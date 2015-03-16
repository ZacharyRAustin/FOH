using UnityEngine;
using System.Collections;

public class Equipment {

	public string name = "Default Equipment Name";
	public int health = 0, mana = 0, strength = 0, agility = 0, intelligence = 0, armor = 0, magicResist = 0, level = 1;

	public string NameGenerator()
	{
		string equipmentType = null;
		string descriptor = "Basic ";
		int greatestStat = 0;
		
		if (this is Bow)
		{
			equipmentType = "Bow";
		}
		else if (this is Sword)
		{
			equipmentType = "Sword";
		}
		else if (this is Wand)
		{
			equipmentType = "Wand";
		}
		else if (this is Armor)
		{
			equipmentType = "Armor";
		}
		
		if (strength > greatestStat)
		{
			descriptor = "Powerful ";
			greatestStat = strength;
		}
		if (agility > greatestStat)
		{
			descriptor = "Agile ";
			greatestStat = agility;
		}
		if (intelligence > greatestStat)
		{
			descriptor = "Wise ";
			greatestStat = intelligence;
		}
		if (health > greatestStat)
		{
			descriptor = "Steadfast ";
			greatestStat = health;
		}
		if (mana > greatestStat)
		{
			descriptor = "Imbued ";
			greatestStat = mana;
		}
		if (armor > greatestStat)
		{
			descriptor = "Defender's ";
			greatestStat = armor;
		}
		if (magicResist > greatestStat)
		{
			descriptor = "Resilient ";
			greatestStat = magicResist;
		}

		string[] values = new string[2]{descriptor, equipmentType};
		return string.Concat (values);
	}

	public void generateStats(int totalPoints)
	{
		for (int ii = totalPoints; ii > 0; ii--)
		{
			int possibleStats = 7;
			int statSelect = Random.Range (0, possibleStats);
			
			if (statSelect == 0)
			{
				health += 3;
			}
			else if (statSelect == 1)
			{
				mana += 3;
			}
			else if (statSelect == 2)
			{
				strength += 1;
			}
			else if (statSelect == 3)
			{
				agility += 1;
			}
			else if (statSelect == 4)
			{
				intelligence += 1;
			}
			else if (statSelect == 5)
			{
				armor += 1;
			}
			else if (statSelect == 6)
			{
				magicResist += 1;
			}
		}
	}

	public void PrintStats()
	{
		// if printing a weapon, call this after the weapon's print function, that includes name, damage, etc
		// if printing armor, just separately print the name and then call PrintStats()
		Debug.Log ("Print stats");

		if (health != 0)
		{
			MyConsole.NewMessage ("Health +" + health);
		}
		if (mana != 0)
		{
			MyConsole.NewMessage ("Mana +" + mana);
		}
		if (strength != 0)
		{
			MyConsole.NewMessage ("Strength +" + strength);
		}
		if (agility != 0)
		{
			MyConsole.NewMessage ("Agility +" + agility);
		}
		if (intelligence != 0)
		{
			MyConsole.NewMessage ("Intelligence +" + intelligence);
		}
		if (armor != 0)
		{
			MyConsole.NewMessage ("Armor +" + armor);
		}
		if (magicResist != 0)
		{
			MyConsole.NewMessage ("Magic Resistance +" + magicResist);
		}
	}
}
