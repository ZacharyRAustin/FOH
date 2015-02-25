using UnityEngine;
using System.Collections;

public class SpawnCharacteristics{

    private static bool nearDoor;
    private static int doorNum;
    private static int level;
    private static int spawnChance = 2;
    private static int doorsEntered = 1;
    private static int maxEnemies = 2;

    public static bool canLeaveRoom() {
        return EnemyCollection.allEnemiesDead() && nearDoor && doorNum > -1;
    }

    public static void setNearDoor(bool near, int pos) {
        nearDoor = near;
        doorNum = pos;
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

    public static int getLevel() {
        return level;
    }

    public static void setLevel(int l) {
        if(l > 0)
        {
            level = l;
        }
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
}
