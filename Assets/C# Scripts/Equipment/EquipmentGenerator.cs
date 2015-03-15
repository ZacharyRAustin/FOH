using UnityEngine;
using System.Collections;

public class EquipmentGenerator {

	public Sword basicSword = new Sword("Basic Sword", 1, 60, 0, 1f);
	public Bow basicBow = new Bow ("Basic Bow", 1, 60, 0, 1f);
	public Wand basicWand = new Wand ("Basic Wand", 1, 60, 0, 1f);
	public Armor basicArmor = new Armor ("Basic Armor", 1);

	public static Weapon GenerateWeapon(int lvl)
	{
		int r = Random.Range (1, 4);
		if (r == 1)
		{
			return new Sword (lvl);
		}
		else if (r == 2)
		{
			return new Bow (lvl);
		}
		else if (r == 3)
		{
			return new Wand (lvl);
		}
		else
		{
			Debug.Log ("Generate Weapon r is not 1, 2 or 3; something is horribly wrong");
			return null;
		}
	}

	public static Armor GenerateArmor(int lvl)
	{
		return new Armor (lvl);
	}
}
