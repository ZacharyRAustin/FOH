using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public bool isPaused = true;
	public string text;
    public Room roomPrefab;
    private Room roomInstance;

	private InputManager inputManager = new InputManager();

	private Character selected;

    public Character characterPrefab1;
	public Character characterPrefab2;
	public Character characterPrefab3;
	public Character trollPrefab;
	public string stringToEditA = "";
	public string stringToEditB = "";
	public string stringToEditC = "";
	public bool userHasHitReturnA = false;
	public bool userHasHitReturnB = false;
	public bool userHasHitReturnC = false;
    

	private EquipmentGenerator equipmentGenerator = new EquipmentGenerator();

	public int count_1;

	private Character playerCharA;
	private Character playerCharB;
	private Character playerCharC;
	private Character enemy;
	public Image damageimage;

    public Material enemyMaterial;
    public Material heroMaterial;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Color flashColour_1 = new Color(0f, 0f, 0f, 0f);
	public int count;

    private int seed = 123456789;

	GUIStyle style_font = new GUIStyle();

	// Use this for initialization

	void Start () {


		levelchange.start ();
		count_1 = 0;
		style_font.fontSize = 10;
		style_font.fontStyle = FontStyle.Normal;

        Random.seed = seed;
        EnemyGenerator.Initialize(trollPrefab, enemyMaterial);
        SpawnCharacteristics.prepareForSpawn();
        BeginGame();
		CharacterCollection.addHero (playerCharA);
        CharacterCollection.addHero (playerCharB);
        CharacterCollection.addHero (playerCharC);
        roomInstance.SpawnCharacters(DoorPositions.SOUTH);
		inputManager.Awake ();
		inputManager.Select (playerCharA);
        //for(int i = 0; i < EnemyCollection.NumberOfEnemies(); i++)
        //{
        //    EnemyCollection.getEnemy(i).setTarget(playerCharA);
        //    EnemyCollection.getEnemy(i).actionQueue.ParentChar = EnemyCollection.getEnemy(i);
        //    EnemyCollection.getEnemy(i).Enqueue(playerCharA);
        //}
	}
	
	// Update is called once per frame
	void Update () {

		SetHeroIsSelected ();
		count_1 += 1;
		flashred ();

		playerCharA.count_times = 0;
		playerCharB.count_times = 0;
		playerCharC.count_times = 0;

		if (EnemyCollection.NumberOfEnemies() > 0)
		{
        	if(EnemyCollection.getEnemy(0) != null)
        	{
        	    setEnemyTarget ();
        	}
		}

        if (Input.GetButtonDown("Regenerate Map"))
        {
            roomInstance.GenerateNextRoom();

        }
		if (Input.GetButtonDown("Pause"))
		{
			isPaused = !isPaused;
            Utilities.pause(isPaused);
		}

        if(Input.GetButtonDown("Level Test"))
        {
            LevelingTest.PerformTest();
        }

		if (Input.GetButtonDown("Generate Abilities"))
		{
			AbilityParameters param = new AbilityParameters(Random.Range(1,5));
			RandomAbility s = new RandomAbility();
			s.SetAbility(param);
			playerCharA.stats.AddAbility(s);
			Debug.Log ("Hero A got a new ability!");
			MyConsole.NewMessage("Hero A got a new ability!");
			s.Print ();
			s.caster = playerCharA;

			param = new AbilityParameters(Random.Range (1,5));
			s = new RandomAbility();
			s.SetAbility(param);
			playerCharB.stats.AddAbility(s);
			Debug.Log ("Hero B got a new ability!");
			MyConsole.NewMessage("Hero B got a new ability!");
			s.Print ();
			s.caster = playerCharB;

			param = new AbilityParameters(Random.Range (1,5));
			s = new RandomAbility();
			s.SetAbility(param);
			playerCharC.stats.AddAbility(s);
			Debug.Log ("Hero C got a new ability!");
			MyConsole.NewMessage("Hero C got a new ability!");
			s.Print ();
			s.caster = playerCharC;
		}

		if (Input.GetButtonDown ("Generate Equipment"))
	    {
			playerCharA.stats.weapon = equipmentGenerator.GenerateWeapon(3);
			Debug.Log ("Hero A got a " + playerCharA.stats.weapon.name + "!");
			MyConsole.NewMessage("Hero A got a " + playerCharA.stats.weapon.name + "!");
			playerCharA.stats.gear[0] = equipmentGenerator.GenerateArmor(1);
			playerCharA.stats.gear[1] = equipmentGenerator.GenerateArmor(2);
			playerCharA.stats.gear[2] = equipmentGenerator.GenerateArmor(0);

			playerCharB.stats.weapon = equipmentGenerator.GenerateWeapon(3);
			Debug.Log ("Hero B got a " + playerCharB.stats.weapon.name + "!");
			MyConsole.NewMessage("Hero B got a " + playerCharB.stats.weapon.name + "!");
			playerCharB.stats.gear[0] = equipmentGenerator.GenerateArmor(10);
			playerCharB.stats.gear[1] = equipmentGenerator.GenerateArmor(2);
			playerCharB.stats.gear[2] = equipmentGenerator.GenerateArmor(1);

			playerCharC.stats.weapon = equipmentGenerator.GenerateWeapon(3);
			Debug.Log ("Hero C got a " + playerCharC.stats.weapon.name + "!");
			MyConsole.NewMessage("Hero C got a " + playerCharC.stats.weapon.name + "!");
			playerCharC.stats.gear[0] = equipmentGenerator.GenerateArmor(10);
			playerCharC.stats.gear[1] = equipmentGenerator.GenerateArmor(2);
			playerCharC.stats.gear[2] = equipmentGenerator.GenerateArmor(1);
		}

		if (Input.GetButtonDown ("Buff Test"))
		{
			MoveSpeedBuff slow1 = new MoveSpeedBuff();
			DodgeRateBuff evasion1 = new DodgeRateBuff();
			AttackRateBuff attackRate1 = new AttackRateBuff();
			slow1.duration = 5;
			evasion1.duration = 5;
			attackRate1.duration = 10;
			slow1.magnitude = .5f;
			evasion1.magnitude = 2;
			attackRate1.magnitude = .5f;
			slow1.target = playerCharA;
			evasion1.target = playerCharA;
			attackRate1.target = playerCharA;
			playerCharA.stats.AddBuff(slow1);
			playerCharA.stats.AddBuff(evasion1);
			playerCharA.stats.AddBuff(attackRate1);
		}

		if (Input.GetButtonDown ("View Equipment"))
		{
			playerCharA.stats.PrintEquipment();
			playerCharB.stats.PrintEquipment();
			playerCharC.stats.PrintEquipment();
		}

		inputManager.Resolve ();

	}

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();
        playerCharA = Instantiate (characterPrefab1) as Character;
		playerCharA.characterPrefab.SetParentChar(playerCharA);
		playerCharA.characterPrefab.name = "Hero A Prefab";
        playerCharA.Generate();
		playerCharA.actionQueue.ParentChar = playerCharA;
		playerCharA.position_y_health= 10;
		playerCharA.position_y = 40;
		playerCharA.isenemy = false;
		playerCharA.image_name = "Hero1_image";
		playerCharA.tag = "Hero A";
		playerCharA.name = "Hero A";
		playerCharA.stats.Name = "Hero A";
		playerCharA.stats.InitializeBaseStats ();
		playerCharA.stats.InitializeEquipment ();
		playerCharA.stats.CalculateCombatStats ();
		playerCharA.stats.InitializeCombatStats ();

		playerCharB = Instantiate (characterPrefab2) as Character;
		playerCharB.characterPrefab.name = "Hero B Prefab";
		playerCharB.characterPrefab.SetParentChar(playerCharB);
        playerCharB.Generate();
		playerCharB.actionQueue.ParentChar = playerCharB;
		playerCharB.position_y_health = 70;
		playerCharB.position_y = 100;
		playerCharB.isenemy = false;
		playerCharB.image_name = "Hero2_image";
		playerCharB.tag = "Hero B";
		playerCharB.name = "Hero B";
		playerCharB.stats.Name = "Hero B";
		playerCharB.stats.InitializeBaseStats ();
		playerCharB.stats.InitializeEquipment ();
		playerCharB.stats.CalculateCombatStats ();
		playerCharB.stats.InitializeCombatStats ();
		playerCharB.stats.InitializeProgressionStats ();
		//playerCharB.Anim = playerCharB.characterPrefab.GetComponent<Animator> ();
		//playerCharB.Anim.SetBool ("walk 0", true);
		//Debug.Log (playerCharB.Anim);

		playerCharC = Instantiate (characterPrefab3) as Character;
		playerCharC.characterPrefab.name = "Hero C Prefab";
		playerCharC.characterPrefab.SetParentChar(playerCharC);
        playerCharC.Generate();
		playerCharC.actionQueue.ParentChar = playerCharC;
		playerCharC.tag = "Hero C";
		playerCharC.name = "Hero C";
		playerCharC.position_y_health = 130;
		playerCharC.position_y = 160;
		playerCharC.isenemy = false;
		playerCharC.image_name = "Hero3_image";
		playerCharC.stats.Name = "Hero C";
		playerCharC.stats.InitializeBaseStats ();
		playerCharC.stats.InitializeEquipment ();
		playerCharC.stats.CalculateCombatStats ();
		playerCharC.stats.InitializeCombatStats ();
		playerCharC.stats.InitializeProgressionStats ();
  }


    private void setEnemyTarget() {
        for (int i = 0; i < EnemyCollection.NumberOfEnemies(); i++)
        {

            Character closestHero = CharacterCollection.getHero(0);
            float closestDistance = 1000.0f;
            Vector3 enemyPosition = EnemyCollection.getEnemy(i).getCharacterPosition();
            for (int j = 0; j < CharacterCollection.NumberOfHeroes(); j++)
            {
                Vector3 heroPosition = CharacterCollection.getHero(j).getCharacterPosition();
                if (Vector3.Distance(enemyPosition, heroPosition) < closestDistance)
                {
                    closestDistance = Vector3.Distance(enemyPosition, heroPosition);
                    closestHero = CharacterCollection.getHero(j);
                }
            }
            EnemyCollection.getEnemy(i).Target = closestHero;
        }
    }
//=======
//        //playerCharC.Anim = playerCharC.characterPrefab.GetComponent<Animator> ();

//        // initialize enemy
//        enemy = Instantiate(characterPrefab) as Character;
//        enemy.characterPrefab.name = "Enemy Prefab";
//        enemy.characterPrefab.SetParentChar(enemy);
//        enemy.Generate(2.7f, 5.0f);
//        enemy.setMaterial(enemyMaterial);
//        enemy.name = "Enemy";
//        enemy.stats.Name = "Enemy";
//        enemy.stats.MaxHealth = 50;
//        enemy.stats.MaxMana = 30;
//        enemy.stats.Strength = 10;
//        enemy.stats.Agility = 5;
//        enemy.stats.Intelligence = 5;
//        enemy.stats.InitializeCombatStats ();

//        allies.addHero (playerCharA);
//        allies.addHero (playerCharB);
//        allies.addHero (playerCharC);
//        enemies.addEnemy (enemy);
		
//        setEnemyTarget (enemy, allies);

//        // add enemy movement to enemy
//        enemy.gameObject.AddComponent ("EnemyMovement");



//    }
	void OnGUI(){
				
				
				MyConsole.DrawConsole ();
				levelchange.Drawlayout ();
		
		
		Event e = Event.current;
		if (e.keyCode == KeyCode.Return) {
						userHasHitReturnA = true;
			playerCharA.name = stringToEditA;
			playerCharB.name = stringToEditB;
			playerCharC.name = stringToEditC;
				}
				else if (false == userHasHitReturnA) {
			GUI.BeginGroup(new Rect(Screen.width/2 - 40, Screen.height/2 - 30,300, 30));
			GUI.Label(new Rect(0,0,300,30),"Name your players and Press Enter");
			GUI.EndGroup();

			stringToEditA = GUI.TextField (new Rect (Screen.width/2, Screen.height/2, 100, 20), stringToEditA, 25);
			stringToEditB = GUI.TextField (new Rect (Screen.width/2, Screen.height/2 + 20, 100, 20), stringToEditB, 25);
			stringToEditC = GUI.TextField (new Rect (Screen.width/2, Screen.height/2 + 40, 100, 20), stringToEditC, 25);
				}
		        

	
	 
}


    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }

	public void flashred(){
		if (playerCharA.count_times == 0 && playerCharB.count_times == 0 && playerCharC.count_times == 0) {
			damageimage.color = flashColour_1;


		} else if((playerCharA.stats.CurrentHealth*100/playerCharA.stats.MaxHealth <= 40)&& (playerCharA.count_times >= 0) ||
		          (playerCharB.stats.CurrentHealth*100/playerCharB.stats.MaxHealth <= 40)&& (playerCharB.count_times >= 0) ||
		          (playerCharC.stats.CurrentHealth*100/playerCharC.stats.MaxHealth <= 40)&& (playerCharC.count_times >= 0)){
			damageimage.color = flashColour;
//			levelchange.push_abilities("power up");
//			levelchange.showlayout();
		}
		
		
	}

	private void RemoveHero(Character hero)
	{
		CharacterCollection.removeHero (hero);
	}

	private void RemoveEnemy(Character enemy)
	{
		EnemyCollection.removeEnemy (enemy);
	}

	private void SetHeroIsSelected ()
	{
		playerCharA.is_selected = false;
		playerCharB.is_selected = false;
		playerCharC.is_selected = false;
		if (playerCharA.isclick == true) {
			inputManager.selected = playerCharA;
				}
		if (playerCharB.isclick == true) {
			inputManager.selected = playerCharB;	
		}
		if (playerCharC.isclick == true) {
			//levelchange.clear();
			inputManager.selected = playerCharC;
		}
		if (playerCharA == inputManager.selected)
		{
			playerCharA.is_selected = true;
		}
		else if (playerCharB == inputManager.selected)
		{
			playerCharB.is_selected = true;
		}
		else if (playerCharC == inputManager.selected)
		{
			playerCharC.is_selected = true;
		}
	}
}
