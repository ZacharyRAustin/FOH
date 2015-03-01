using UnityEngine;
using System.Collections;

public class StrengthBuff : Buff {

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
			Debug.Log ("Strength: " + target.stats.Strength);
		}
		if (elapsedTime >= duration)
		{
			target.stats.strMod -= (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s strength returns to normal.");
			Debug.Log ("Strength: " + target.stats.Strength);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
