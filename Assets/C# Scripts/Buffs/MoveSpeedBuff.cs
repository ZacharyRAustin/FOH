using UnityEngine;
using System.Collections;

public class MoveSpeedBuff : Buff {

	public override void DebuffSet()
	{
		if (magnitude > 1)
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
				Debug.Log (target.name + "'s move speed is multiplied by " + magnitude + "!");
			}
			else if (magnitude < 1)
			{
				Debug.Log (target.name + "'s move speed is multiplied by " + magnitude + "!");
			}
			target.stats.moveSpeedMod *= magnitude;
			target.stats.CalculateCombatStats();
			Debug.Log ("Move speed: " + target.stats.MoveSpeed);
		}

		target.stats.moveSpeedMod *= magnitude;
		target.stats.CalculateCombatStats();

		if (elapsedTime >= duration)
		{
			target.stats.CalculateCombatStats();
			Debug.Log (target.name + "'s movement speed returns to normal.");
			Debug.Log ("Move speed: " + target.stats.MoveSpeed);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;

	}
	

}
