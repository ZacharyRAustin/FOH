using UnityEngine;
using System.Collections.Generic;

/*
 * Static class to holds enemies
 * 
 * Removes the need to pass a enemy collection to each 
 * function and class that needs access to it
 */

public class EnemyCollection {
    private static List<Character> enemies = new List<Character>();

    public static void addEnemy(Character enemy) {
        enemies.Add(enemy);
    }

	public static int NumberOfEnemies ()
	{
		return enemies.Count;
	}

    public static Character getEnemy(int index) {
        if(index > -1 && index < enemies.Count)
        {
            return enemies.ToArray()[0];
        }
        else
        {
            Debug.Log("Invalid enemy index " + index + " for enemies list size " + enemies.Count);
            return null;
        }
    }

    public static void removeEnemy(int index) {
        if (-1 < index && index < enemies.Count)
        {
            enemies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Invalid enemy index " + index + " for enemies list size " + enemies.Count);
        }
    }

    public static void removeAll() {
        enemies.Clear();
    }

    public static void print() {
        foreach (Character c in enemies)
        {
            Debug.Log(c.name + " num spells " + c.stats.abilities.Count);
        }
    }

}
