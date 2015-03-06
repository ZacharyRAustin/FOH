using UnityEngine;
using System.Collections;

public class StrengthBuff : Buff {

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
				Debug.Log (target.name + "'s strength is boosted by " + magnitude + "!");
			}
			else if (magnitude < 0)
			{
				Debug.Log (target.name + "'s strength is reduced by " + magnitude + "!");
			}
			target.stats.strMod += (int) magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Strength: " + target.stats.Strength);
		}

		target.stats.strMod += (int) magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s strength returns to normal.");
			Debug.Log ("Strength: " + target.stats.Strength);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
