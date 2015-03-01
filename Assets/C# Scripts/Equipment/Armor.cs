using UnityEngine;
using System.Collections;

public class Armor : Equipment {

	public Armor (string sName, int sArmor)
	{
		name = sName;
		armor = sArmor;
	}

	public Armor (int lvl)
	{
		level = lvl;
		generateStats (lvl);
		name = NameGenerator ();
	}

	public void Print ()
	{
		Debug.Log (name + ":");
		PrintStats ();
	}
}
