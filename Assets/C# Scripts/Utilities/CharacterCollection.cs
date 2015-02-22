using UnityEngine;
using System.Collections.Generic;

/*
 * Static class to hold heroes
 * 
 * Removes the need to pass a character instance to a bunch of different 
 * classes and functions
 */ 
public class CharacterCollection {
    private static List<Character> heroes = new List<Character>();

    public static void addHero(Character hero) {
        heroes.Add(hero);
    }

	public static void removeHero(Character hero)
	{
		heroes.Remove (hero);
	}

    public static int NumberOfHeroes() {
        return heroes.Count;
    }

    public static Character getHero(int index) {
        if(-1 < index && index < heroes.Count)
        {
            return heroes.ToArray()[index];
        }
        else
        {
            Debug.Log("Invalid hero index " + index + " for size " + heroes.Count);
            return null;
        }
    }

    public static void removeHero(int index) {
        if (-1 < index && index < heroes.Count)
        {
            heroes.RemoveAt(index);
        }
        else
        {
            Debug.Log("Invalid enemy index " + index + " for enemies list size " + heroes.Count);
        }
    }

    public static void removeAll() {
        heroes.Clear();
    }

    public static void print() {
        foreach(Character c in heroes){
            Debug.Log(c.name + " Num Spells " + c.stats.abilities.Count);
        }
    }

    public static void pause(bool paused) {
        foreach (Character c in heroes)
        {
            c.isPaused = paused;
        }
    }

    public static void setCharacterPosition(int index, Vector3 pos) {
        if (-1 < index && index < heroes.Count && pos != null)
        {
            heroes.ToArray()[index].setCharacterPosition(pos);
        }
        else
        {
            Debug.Log("Unable to set character position for index " + index
                + " at position " + pos);
        }
    }

    public static void prepareCharactersForSpawn() {
        List<Character> destroy = new List<Character>();
        foreach(Character c in heroes)
        {
            if(c.isDead)
            {
                destroy.Add(c);
            }
            else
            {
                c.removeVelocities();
                c.clearActionQueue();
            }
        }

        foreach(Character c in destroy)
        {
            heroes.Remove(c);
            Object.Destroy(c.gameObject);
        }
    }

	public static void heroExpGain(int exp)
	{
		foreach(Character c in heroes)
		{
			c.GainExp(exp);
		}
	}
}
