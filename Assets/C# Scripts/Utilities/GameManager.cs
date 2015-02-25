using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public bool isPaused = true;
	
    public Room roomPrefab;
    private Room roomInstance;

	private InputManager inputManager = new InputManager();

	private Character selected;
    public Character characterPrefab;
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

	// Use this for initialization

	void Start () {
		//damageimage.color = flashColour;



        Random.seed = seed;
        EnemyGenerator.Initialize(characterPrefab, enemyMaterial);
        BeginGame();
		CharacterCollection.addHero (playerCharA);
        CharacterCollection.addHero(playerCharB);
        CharacterCollection.addHero(playerCharC);
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

//=======
//        inputManager.Allies = allies;
//        inputManager.Enemies = enemies;
		flashred ();
        if(EnemyCollection.getEnemy(0) != null)
        {
            setEnemyTarget (EnemyCollection.getEnemy(0));
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

		inputManager.Resolve ();

	}

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();
        playerCharA = Instantiate (characterPrefab) as Character;
		playerCharA.characterPrefab.SetParentChar(playerCharA);
		playerCharA.characterPrefab.name = "Hero A Prefab";
        playerCharA.Generate();
		playerCharA.actionQueue.ParentChar = playerCharA;
		playerCharA.position_y_health= 10;
		playerCharA.position_y = 40;
		playerCharA.isenemy = false;
		playerCharA.image_name = "hero_image";
		playerCharA.tag = "Hero A";
		playerCharA.name = "Hero A";
		playerCharA.stats.Name = "Hero A";
		playerCharA.stats.MaxHealth = 50;
		playerCharA.stats.MaxMana = 30;
		playerCharA.stats.Strength = 7;
		playerCharA.stats.Agility = 5;
		playerCharA.stats.Intelligence = 5;
		playerCharA.stats.InitializeCombatStats ();


		playerCharB = Instantiate (characterPrefab) as Character;
		playerCharB.characterPrefab.name = "Hero B Prefab";
		playerCharB.characterPrefab.SetParentChar(playerCharB);
        playerCharB.Generate();
		playerCharB.actionQueue.ParentChar = playerCharB;
		playerCharB.position_y_health = 70;
		playerCharB.position_y = 100;
		playerCharB.isenemy = false;
		playerCharB.image_name = "hero_image";
		playerCharB.tag = "Hero B";
		playerCharB.name = "Hero B";
		playerCharB.stats.Name = "Hero B";
		playerCharB.stats.MaxHealth = 40;
		playerCharB.stats.MaxMana = 30;
		playerCharB.stats.Strength = 5;
		playerCharB.stats.Agility = 8;
		playerCharB.stats.Intelligence = 5;
		playerCharB.stats.InitializeCombatStats ();
		playerCharB.stats.AttackRange = 5.0f;
		//playerCharB.Anim = playerCharB.characterPrefab.GetComponent<Animator> ();
		//playerCharB.Anim.SetBool ("walk 0", true);
		//Debug.Log (playerCharB.Anim);

		Heal heal = new Heal ();
		heal.Start ();
		Frostbolt frostbolt = new Frostbolt ();
		frostbolt.Start ();

		playerCharC = Instantiate (characterPrefab) as Character;
		playerCharC.characterPrefab.name = "Hero C Prefab";
		playerCharC.characterPrefab.SetParentChar(playerCharC);
        playerCharC.Generate();
		playerCharC.actionQueue.ParentChar = playerCharC;
		playerCharC.tag = "Hero C";
		playerCharC.name = "Hero C";
		playerCharC.position_y_health = 130;
		playerCharC.position_y = 160;
		playerCharC.isenemy = false;
		playerCharC.image_name = "hero_image";
		playerCharC.stats.Name = "Hero C";
		playerCharC.stats.MaxHealth = 30;
		playerCharC.stats.MaxMana = 50;
		playerCharC.stats.Strength = 3;
		playerCharC.stats.Agility = 5;
		playerCharC.stats.Intelligence = 8;
		playerCharC.stats.InitializeCombatStats ();
		playerCharC.stats.AttackRange = 4.0f;
		playerCharC.stats.MagicAttack = true;
		playerCharC.stats.abilities.Add (heal);
		playerCharC.stats.abilities.Add (frostbolt);
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
		MyConsole.DrawConsole(new Rect(Screen.width-200,Screen.height-100,400,400));
		if (Event.current.type == EventType.Repaint) {
				
		}

		}
	private void setEnemyTarget(Character enemy) {
		Character closestHero = CharacterCollection.getHero(0);
		float closestDistance = 1000.0f;
		Vector3 enemyPosition = enemy.getCharacterPosition ();
		for (int i = 0; i < CharacterCollection.NumberOfHeroes(); i++) {
			Vector3 heroPosition = CharacterCollection.getHero(i).getCharacterPosition();
			if (Vector3.Distance (enemyPosition, heroPosition) < closestDistance) {
				closestDistance = Vector3.Distance (enemyPosition, heroPosition);
				closestHero = CharacterCollection.getHero(i);
			}
		}
		enemy.Target = closestHero;
	}

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }

	public void flashred(){
		if (playerCharA.count_times == 0 && playerCharB.count_times == 0 && playerCharC.count_times == 0) {
			damageimage.color = flashColour_1;
		} else {
			damageimage.color = flashColour;
		}
		
		
	}

}
