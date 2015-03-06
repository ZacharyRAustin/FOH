using UnityEngine;
using System.Collections;

public class AgilityBuff : Buff {

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
				Debug.Log (target.name + "'s agility is boosted by " + magnitude + "!");
			}
			else if (magnitude < 0)
			{
				Debug.Log (target.name + "'s agility is reduced by " + magnitude + "!");
			}
			target.stats.agiMod += (int) magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Agility: " + target.stats.Agility);
		}
		else if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s agility returns to normal.");
			Debug.Log ("Agility: " + target.stats.Agility);
			target.stats.RemoveBuff(this);
		}
		else
		{
			target.stats.agiMod += (int) magnitude;
			target.stats.CalculateCombatStats();
		}
		//Debug.Log ("Elapsed buff time: " + elapsedTime);


		
		elapsedTime += Time.deltaTime;
		
	}
}
