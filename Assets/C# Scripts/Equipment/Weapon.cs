using UnityEngine;
using System.Collections;

public class Weapon : Equipment {
	
	public int damage, hitRate, critRate;
	public float attackRange, attackRate;
	public bool magicAttack;

	public void Print ()
	{
		Debug.Log (name);
		Debug.Log ("Damage: " + damage);

		if (magicAttack)
		{
			Debug.Log ("Attack Type: Magical");
		}
		else
		{
			Debug.Log ("Attack Type: Physical");
		}

		Debug.Log ("Hit Rate: " + hitRate);
		Debug.Log ("Crit Rate: " + critRate);
		Debug.Log ("Range: " + attackRange);
		Debug.Log ("Attack Time: " + attackRate);
		PrintStats ();
	}




}
