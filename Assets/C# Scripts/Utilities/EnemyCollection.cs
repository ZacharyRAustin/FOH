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
            return enemies.ToArray()[index];
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

    public static void removeAndDestroyEnemy(Character c) {
        if(c != null)
        {
            enemies.Remove(c);
            Object.Destroy(c.gameObject);

        }
    }

	public static void removeEnemy(Character enemy)
	{
		enemies.Remove (enemy);
	}

    public static void removeAll() {
        List<Character> destroy = new List<Character>(enemies);
        enemies.Clear();
        foreach(Character c in destroy)
        {
            if(c != null && c.gameObject != null)
            {
                Object.Destroy(c.gameObject);
            }
        }
    }

    public static void print() {
        foreach (Character c in enemies)
        {
            Debug.Log(c.name + " num spells " + c.stats.abilities.Count);
        }
    }

    public static void pause(bool paused) {
        foreach (Character c in enemies)
        {
            c.isPaused = paused;
        }
    }

    public static bool allEnemiesDead() {
        bool ret = true;
        foreach(Character c in enemies)
        {
            ret = ret && c.isDead;
        }
        return ret;
    }

}
