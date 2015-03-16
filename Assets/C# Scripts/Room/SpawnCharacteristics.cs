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
    private static int averageCharacterLevel = 1;
    private static int diffPerEnemy;
    private static int testCounter = 0;
    private static int maxEnemyLevel;
    private static int enemyCumulativeLevel = 0;
    private static bool isBoss = false;

    public static bool canLeaveRoom() {
        print();
        if(doorNum > -1)
        {
            nearDoor = true;
        }
        return EnemyCollection.allEnemiesDead() && nearDoor && doorNum > -1;
    }

    public static void print() {
        Debug.Log("All enemies dead: " + EnemyCollection.allEnemiesDead() + " and near door " + nearDoor + " with number " + doorNum);
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
		else
		{
			averageEnemyLevel = 1;
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
        averageCharacterLevel = CharacterCollection.getAverageLevel();
        averageEnemyLevel = averageCharacterLevel;

        int totalHeroLevel = averageCharacterLevel * CharacterCollection.NumberOfHeroes();
        int totalEnemyLevel = averageEnemyLevel * maxEnemies;
        int levelDifference = totalHeroLevel - totalEnemyLevel;
        diffPerEnemy = levelDifference / maxEnemies;
        maxEnemyLevel = (Math.Max ((averageCharacterLevel + (int) ((double) averageCharacterLevel * .3)), 1));
        Debug.Log("Max Enemy Level is " + maxEnemyLevel);
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
            Debug.Log("Temp is " + temp);
            int ret = Math.Min(temp, maxEnemyLevel);
            enemyCumulativeLevel += ret;
            return ret;
        }
        else
        {
            int temp = Math.Max(averageEnemyLevel + UnityEngine.Random.Range(0, bound), 1);
            Debug.Log("Temp is " + temp);
            int ret = Math.Min(temp, maxEnemyLevel);
            enemyCumulativeLevel += ret;
            return ret;
        }

    }

    public static void prepareForSpawn() {
        isBoss = false;
        if(UnityEngine.Random.Range(0, 100) < 9)
        {
            isBoss = true;
            maxEnemies = 1;
        }
        else
        {
            calculateMaxEnemies();
        }
        increaseDoorsEntered();
        calculateEnemyLevel();
        enemyCumulativeLevel = 0;
    }

    public static int testLeveling(int heroLevel) {
        averageCharacterLevel = heroLevel;
        calculateMaxEnemies();
        calculateEnemyLevel();
        return maxEnemies;
    }

    public static int getCumulativeLevelDifference() {
        return enemyCumulativeLevel - CharacterCollection.getCumulativeLevel();
    }

    public static int getMaxEnemies() {
        return maxEnemies;
    }

    public static bool isBossFight() {
        return isBoss;
    }
}
