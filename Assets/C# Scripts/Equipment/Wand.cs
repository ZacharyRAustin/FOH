using UnityEngine;
using System.Collections;

public class Wand : Weapon {

	static public int minDamage = 1, maxDamage = 3, minHit = 50, maxHit = 70, minCrit = 0, maxCrit = 20;
	static public float minRange = 3f, maxRange = 6f, minRate = .5f, maxRate = 1.5f;

	public Wand (int lvl)
	{
		level = lvl;
		magicAttack = true;
		attackRange = Random.Range (minRange, maxRange);
		attackRate = Random.Range (minRate, maxRate);
		damage = Random.Range (minDamage, maxDamage + lvl);
		hitRate = Random.Range (minHit, maxHit);
		critRate = Random.Range (minCrit, maxCrit);
		generateStats (lvl);
		name = NameGenerator ();
	}

	public Wand (string sName, int sDamage, int sHit, int sCrit, float sRate)
	{
		magicAttack = true;
		attackRange = 5.0f;
		name = sName;
		damage = sDamage;
		hitRate = sHit;
		critRate = sCrit;
		attackRate = sRate;
	}
}
