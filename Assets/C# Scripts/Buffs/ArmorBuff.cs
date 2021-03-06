﻿using UnityEngine;
using System.Collections;

public class ArmorBuff : Buff {

	public override void DebuffSet()
	{
		if (magnitude >= 0)
		{
			debuff = false;
		}
		else
		{
			debuff = true;
		}
	}
	
	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			if (magnitude > 0)
			{
				Debug.Log (target.name + "'s armor is boosted by " + magnitude + "!");
			}
			else if (magnitude < 0)
			{
				Debug.Log (target.name + "'s armor is reduced by " + magnitude + "!");
			}
			target.stats.armorMod += (int) magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Armor: " + target.stats.Armor);
		}

		target.stats.armorMod += (int) magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s armor returns to normal.");
			Debug.Log ("Armor: " + target.stats.Armor);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
