using UnityEngine;
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
            enemy.Generate(pos.x, pos.y);
            enemy.name = enemyName;
            int temp = SpawnCharacteristics.generateNewEnemyLevel();
            Debug.Log("Generated Level is: " + temp);
            enemy.stats = new CharacterStats(temp);
            enemy.stats.Name = enemyName;
            enemy.isenemy = true;
            enemy.position_y_health = 10 + 60 * EnemyCollection.NumberOfEnemies();
            enemy.position_y = 40 + 60 * EnemyCollection.NumberOfEnemies();
            enemy.image_name = "troll_image";
            enemy.gameObject.AddComponent("EnemyMovement");
            EnemyCollection.addEnemy(enemy);
            return numEnemies;
        }
        else
        {
            return -1;
        }
    }
}
