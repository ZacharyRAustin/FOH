using UnityEngine;
using System.Collections;
using System;

public class SpawnCharacteristics{

    private static bool nearDoor;
    private static int doorNum;
    private static int averageEnemyLevel;
    private static int spawnChance = 2;
    private static int doorsEntered = 1;
    private static int maxEnemies = 2;
    private static int averageCharacterLevel;
    private static int diffPerEnemy;
    private static int testCounter = 0;
    private static int maxEnemyLevel;

    public static bool canLeaveRoom() {
        return EnemyCollection.allEnemiesDead() && nearDoor && doorNum > -1;
    }

    public static void setNearDoor(bool near, int pos) {
        if(pos > 0)
        {
            if(pos == DoorPositions.NORTH)
            {
                doorNum = DoorPositions.SOUTH;
            }
            else if(pos == DoorPositions.SOUTH)
            {
                doorNum = DoorPositions.NORTH;
            }
            else if(pos == DoorPositions.WEST)
            {
                doorNum = DoorPositions.EAST;
            }
            else if(pos == DoorPositions.EAST)
            {
                doorNum = DoorPositions.WEST;
            }
            else
            {
                doorNum = -1;
            }
        }
        nearDoor = near;
    }

    public static int getDoorPosition() {
        return doorNum;
    }

    public static void setSpawnChance(int c) {
        if (c > 0 && c < 25)
        {
            spawnChance = c;
        }
    }

    public static int getSpawnChance() {
        if(EnemyCollection.NumberOfEnemies() >= maxEnemies)
        {
            return 0;
        }
        return spawnChance;
    }

    public static int getAvgEnemyLevel() {
        return averageEnemyLevel;
    }

    public static void setAvgEnemyLevel(int l) {
        if(l > 0)
        {
            averageEnemyLevel = l;
        }
    }

    public static int getAvgCharacterLevel() {
        return averageCharacterLevel;
    }

    public static void setAvgCharacterLevel(int l) {
        averageCharacterLevel = l;
    }

    public static void increaseDoorsEntered() {
        doorsEntered++;
    }

    public static int getDoorsEntered() {
        return doorsEntered;
    }

    public static bool shouldSpawnEnemy() {
        return UnityEngine.Random.Range(0f, 200f) < getSpawnChance();
    }

    private static void calculateMaxEnemies() {
        maxEnemies = UnityEngine.Random.Range(1, 6); 
    }

    private static void calculateEnemyLevel() {
        if(testCounter % 3 == 0)
        {
            averageCharacterLevel++;
            Debug.Log("Increasing character level to " + averageCharacterLevel);
        }
        averageEnemyLevel = averageCharacterLevel;

        int totalHeroLevel = averageCharacterLevel * CharacterCollection.NumberOfHeroes();
        int totalEnemyLevel = averageEnemyLevel * maxEnemies;
        int levelDifference = totalHeroLevel - totalEnemyLevel;
        diffPerEnemy = levelDifference / maxEnemies;
        maxEnemyLevel = averageCharacterLevel + (int) ((double) averageCharacterLevel * .3);
        testCounter++;
    }

    public static int generateNewEnemyLevel() {
        int bound = 0;
        if(maxEnemies < CharacterCollection.NumberOfHeroes())
        {
            bound = diffPerEnemy + UnityEngine.Random.Range(0, CharacterCollection.NumberOfHeroes());
        }
        else if(maxEnemies == CharacterCollection.NumberOfHeroes())
        {
            bound = diffPerEnemy + UnityEngine.Random.Range(-1, 1);
        }
        else
        {
            bound = diffPerEnemy + UnityEngine.Random.Range(-CharacterCollection.NumberOfHeroes(), 1); 
        }

        if(bound < 0)
        {
            int temp = Math.Max(averageEnemyLevel + UnityEngine.Random.Range(bound, 0), 1);
            return Math.Min(temp, maxEnemyLevel);
        }
        else
        {
            int temp = Math.Max(averageEnemyLevel + UnityEngine.Random.Range(0, bound), 1);
            return Math.Min(temp, maxEnemyLevel);
        }

    }

    public static void prepareForSpawn() {
        increaseDoorsEntered();
        calculateMaxEnemies();
        calculateEnemyLevel();
    }

    public static int testLeveling(int heroLevel) {
        averageCharacterLevel = heroLevel;
        calculateMaxEnemies();
        calculateEnemyLevel();
        return maxEnemies;
    }
}
