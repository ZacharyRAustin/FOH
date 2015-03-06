using UnityEngine;
using System.Collections;

public class AttackRateBuff : Buff {

	public override void DebuffSet()
	{
		if (magnitude < 1)
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
			if (magnitude > 1)
			{
				Debug.Log (target.name + "'s attack time is slowed by " + magnitude + "!");
			}
			else if (magnitude < 1)
			{
				Debug.Log (target.name + "'s attack time is sped up by " + magnitude + "!");
			}
			target.stats.attackRateMod *= magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Attack rate: " + target.stats.AttackRate);
		}

		target.stats.attackRateMod *= magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s attack rate returns to normal.");
			Debug.Log ("Attack rate: " + target.stats.AttackRate);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
