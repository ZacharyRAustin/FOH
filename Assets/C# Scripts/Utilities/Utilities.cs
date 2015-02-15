using UnityEngine;
using System.Collections;

/*
 * Static class for utility functions
 * Wrapper for multiple calls that do the same thing
 */

public class Utilities {

    private static bool nearDoor;
    private static int doorNum;

    public static void pause(bool paused) {
        EnemyCollection.pause(paused);
        CharacterCollection.pause(paused);
    }

    public static void prepareForGeneration() {
        CharacterCollection.prepareCharactersForSpawn();
        EnemyCollection.removeAll();
    }

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
}
