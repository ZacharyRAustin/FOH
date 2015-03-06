using UnityEngine;
using System.Collections;

public class DodgeRateBuff : Buff {

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
				Debug.Log (target.name + "'s dodge chance is boosted by " + magnitude + "!");
			}
			else if (magnitude < 0)
			{
				Debug.Log (target.name + "'s dodge chance is reduced by " + magnitude + "!");
			}
			target.stats.dodgeMod += (int) magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Dodge rate: " + target.stats.DodgeRate);
		}

		target.stats.dodgeMod += (int) magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s dodge rate returns to normal.");
			Debug.Log ("Dodge rate: " + target.stats.DodgeRate);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
