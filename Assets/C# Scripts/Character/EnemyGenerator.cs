﻿using UnityEngine;
using System.Collections;

public class EnemyGenerator {
    private static Character enemyPrefab;
    private static Material enemyMaterial;
    private static bool initialized = false;

    public static void Initialize(Character prefab, Material mat) {
        enemyPrefab = prefab;
        enemyMaterial = mat;
        initialized = true;
    }

    public static int generateEnemy(Vector3 pos) {
        if(initialized)
        {
            int numEnemies = EnemyCollection.NumberOfEnemies();
            string enemyName = "Enemy " + numEnemies;
            Character enemy = MonoBehaviour.Instantiate(enemyPrefab) as Character;
            enemy.characterPrefab.name = "Enemy " + numEnemies + " Prefab";
            enemy.characterPrefab.SetParentChar(enemy);
            enemy.Generate(pos);
            enemy.setMaterial(enemyMaterial);
            enemy.name = enemyName;
            enemy.stats.Name = enemyName;
            enemy.stats.MaxHealth = 50;
            enemy.stats.MaxMana = 30;
            enemy.stats.Agility = 5;
            enemy.stats.Intelligence = 5;
            enemy.stats.InitializeCombatStats();
            EnemyCollection.addEnemy(enemy);
            return numEnemies;
        }
        else
        {
            return -1;
        }
    }
}