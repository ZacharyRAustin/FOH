using UnityEngine;
using System.Collections;

/*
 * Static class for utility functions
 * Wrapper for multiple calls that do the same thing
 */

public class Utilities {

    public static void pause(bool paused) {
        EnemyCollection.pause(paused);
        CharacterCollection.pause(paused);
    }

    public static void prepareForGeneration() {
        CharacterCollection.prepareCharactersForSpawn();
        CharacterCollection.nextRoomRegen();
        EnemyCollection.removeAll();
        SpawnCharacteristics.prepareForSpawn();
        DropSystem.GenerateLoot();
        DropSystem.printGenerated();
    }
}
