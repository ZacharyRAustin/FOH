using UnityEngine;
using System.Collections;

public class Weapon : Equipment {
	
	public int damage, hitRate, critRate;
	public float attackRange, attackRate;
	public bool magicAttack;

	public void Print ()
	{
		MyConsole.NewMessage (name);
		MyConsole.NewMessage ("Damage: " + damage);

		if (magicAttack)
		{
			MyConsole.NewMessage ("Attack Type: Magical");
		}
		else
		{
			MyConsole.NewMessage ("Attack Type: Physical");
		}

		MyConsole.NewMessage ("Hit Rate: " + hitRate);
		MyConsole.NewMessage ("Crit Rate: " + critRate);
		MyConsole.NewMessage ("Range: " + attackRange);
		MyConsole.NewMessage ("Attack Time: " + attackRate);
		PrintStats ();
	}




}
