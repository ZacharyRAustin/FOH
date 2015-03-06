using UnityEngine;
using System.Collections;

public class IntelligenceBuff : Buff {

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
				Debug.Log (target.name + "'s intelligence is boosted by " + magnitude + "!");
			}
			else if (magnitude < 0)
			{
				Debug.Log (target.name + "'s intelligence is reduced by " + magnitude + "!");
			}
			target.stats.intMod += (int) magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Intelligence: " + target.stats.Intelligence);
		}

		target.stats.intMod += (int) magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s intelligence returns to normal.");
			Debug.Log ("Intelligence: " + target.stats.Intelligence);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
