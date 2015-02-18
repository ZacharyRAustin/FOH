﻿using UnityEngine;
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
		allies.addHero (playerCharA);
		allies.addHero (playerCharB);
		allies.addHero (playerCharC);
		enemies.addEnemy (enemy);
		inputManager.Awake ();
		inputManager.Allies = allies;
		inputManager.Enemies = enemies;
		inputManager.Select (playerCharA);
		enemy.actionQueue.ParentChar = enemy;
		enemy.Enqueue (playerCharA);
	}
	
	// Update is called once per frame
	void Update () {
		inputManager.Allies = allies;
		inputManager.Enemies = enemies;

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
		playerCharA.Generate(new Vector3(-2.7f, -5.0f, 0f));
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
		playerCharB.Generate (new Vector3 (-2.7f, 0f, 0f));
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

		Heal heal = new Heal ();
		heal.Start ();
		Frostbolt frostbolt = new Frostbolt ();
		frostbolt.Start ();

		playerCharC = Instantiate (characterPrefab) as Character;
		playerCharC.characterPrefab.name = "Hero C Prefab";
		playerCharC.characterPrefab.SetParentChar(playerCharC);
		playerCharC.Generate (new Vector3 (-2.7f, 5.0f, 0f));
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
		
		enemy = Instantiate(characterPrefab) as Character;
		enemy.characterPrefab.name = "Enemy Prefab";
		enemy.characterPrefab.SetParentChar(enemy);
        enemy.Generate(new Vector3(2.7f, 5.0f, 0f));
        enemy.setMaterial(enemyMaterial);
		enemy.position_y_health = 10;
		enemy.position_y = 40;
		enemy.isenemy = true; 
		enemy.image_name = "enemy_image";
		enemy.name = "Enemy";
		enemy.stats.Name = "Enemy";
		enemy.stats.MaxHealth = 50;
		enemy.stats.MaxMana = 30;
		enemy.stats.Strength = 10;
		enemy.stats.Agility = 5;
		enemy.stats.Intelligence = 5;
		enemy.stats.InitializeCombatStats ();

        playerCharA.setTarget(enemy);
        enemy.setTarget(playerCharA);

        enemies.addEnemy(enemy);
    }

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }


}
