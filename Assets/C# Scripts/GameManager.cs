using UnityEngine;
using System.Collections;

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

	private CharacterCollection allies = new CharacterCollection();
	private EnemyCollection enemies = new EnemyCollection();

    public Material enemyMaterial;
    public Material heroMaterial;

    private int seed = 123456789;

	// Use this for initialization
	void Start () {
        Random.seed = seed;
		BeginGame();
		inputManager.Awake ();
		inputManager.Allies = allies;
		inputManager.Enemies = enemies;
		inputManager.Select (playerCharA);
		enemy.actionQueue.ParentChar = enemy;
		//enemy.Enqueue (playerCharA);
	}
	
	// Update is called once per frame
	void Update () {
		inputManager.Allies = allies;
		inputManager.Enemies = enemies;

		setEnemyTarget (enemy, allies);


        if (Input.GetButtonDown("Regenerate Map"))
        {
            roomInstance.GenerateNextRoom();
        }
		if (Input.GetButtonDown("Pause"))
		{
			isPaused = !isPaused;
		}

		playerCharA.isPaused = isPaused;
		playerCharB.isPaused = isPaused;
		playerCharC.isPaused = isPaused;
		enemy.isPaused = isPaused;

		inputManager.Resolve ();

	}

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();

        playerCharA = Instantiate (characterPrefab) as Character;
		playerCharA.characterPrefab.SetParentChar(playerCharA);
		playerCharA.characterPrefab.name = "Hero A Prefab";
		playerCharA.Generate(2.7f, -5.0f);
		playerCharA.actionQueue.ParentChar = playerCharA;
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
		playerCharB.Generate (2.7f, 0f);
		playerCharB.actionQueue.ParentChar = playerCharB;
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

		Heal heal = new Heal ();
		heal.Start ();
		Frostbolt frostbolt = new Frostbolt ();
		frostbolt.Start ();

		playerCharC = Instantiate (characterPrefab) as Character;
		playerCharC.characterPrefab.name = "Hero C Prefab";
		playerCharC.characterPrefab.SetParentChar(playerCharC);
		playerCharC.Generate (-2.7f, 5.0f);
		playerCharC.actionQueue.ParentChar = playerCharC;
		playerCharC.tag = "Hero C";
		playerCharC.name = "Hero C";
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

		// initialize enemy
		enemy = Instantiate(characterPrefab) as Character;
		enemy.characterPrefab.name = "Enemy Prefab";
		enemy.characterPrefab.SetParentChar(enemy);
        enemy.Generate(2.7f, 5.0f);
        enemy.setMaterial(enemyMaterial);
		enemy.name = "Enemy";
		enemy.stats.Name = "Enemy";
		enemy.stats.MaxHealth = 50;
		enemy.stats.MaxMana = 30;
		enemy.stats.Strength = 10;
		enemy.stats.Agility = 5;
		enemy.stats.Intelligence = 5;
		enemy.stats.InitializeCombatStats ();

		allies.addHero (playerCharA);
		allies.addHero (playerCharB);
		allies.addHero (playerCharC);
		enemies.addEnemy (enemy);
		
		setEnemyTarget (enemy, allies);

		// add enemy movement to enemy
		enemy.gameObject.AddComponent ("EnemyMovement");



	}

	private void setEnemyTarget(Character enemy, CharacterCollection heroesCollection) {
		Character closestHero = heroesCollection.getHero(0);
		float closestDistance = 1000.0f;
		Vector3 enemyPosition = enemy.getCharacterPosition ();
		for (int i = 0; i < heroesCollection.Heroes.Count; i++) {
			Vector3 heroPosition = heroesCollection.getHero(i).getCharacterPosition();
			if (Vector3.Distance (enemyPosition, heroPosition) < closestDistance) {
				closestDistance = Vector3.Distance (enemyPosition, heroPosition);
				closestHero = heroesCollection.getHero(i);
			}
		}
		enemy.Target = closestHero;
	}

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }


}
