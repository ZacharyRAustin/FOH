using UnityEngine;
using System.Collections;

public class Bow : Weapon {

	static public int minDamage = 1, maxDamage = 5, minHit = 50, maxHit = 80, minCrit = 0, maxCrit = 20;
	static public float minRange = 3f, maxRange = 10f, minRate = .5f, maxRate = 2f;

	public Bow (int lvl)
	{
		level = lvl;
		magicAttack = false;
		attackRange = Random.Range (minRange, maxRange);
		attackRate = Random.Range (minRate, maxRate);
		damage = Random.Range (minDamage + lvl, maxDamage + lvl);
		hitRate = Random.Range (minHit, maxHit);
		critRate = Random.Range (minCrit, maxCrit);
		generateStats (lvl);
		name = NameGenerator ();
	}

	public Bow (string sName, int sDamage, int sHit, int sCrit, float sRate)
	{
		magicAttack = false;
		attackRange = 5.0f;
		name = sName;
		damage = sDamage;
		hitRate = sHit;
		critRate = sCrit;
		attackRate = sRate;
	}

}
