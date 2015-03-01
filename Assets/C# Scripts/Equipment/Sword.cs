using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	static public int minDamage = 1, maxDamage = 5, minHit = 40, maxHit = 80, minCrit = 0, maxCrit = 30;
	static public float minRange = 1.3f, maxRange = 1.3f, minRate = .5f, maxRate = 2f;

	public Sword (int lvl)
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

	public Sword (string sName, int sDamage, int sHit, int sCrit, float sRate)
	{
		magicAttack = false;
		attackRange = 1.3f;
		name = sName;
		damage = sDamage;
		hitRate = sHit;
		critRate = sCrit;
		attackRate = sRate;
	}


}
