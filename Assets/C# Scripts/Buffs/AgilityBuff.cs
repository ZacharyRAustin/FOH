using UnityEngine;
using System.Collections;

public class AgilityBuff : Buff {

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
			Debug.Log ("Agility: " + target.stats.Agility);
		}
		if (elapsedTime >= duration)
		{
			target.stats.agiMod -= (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s agility returns to normal.");
			Debug.Log ("Agility: " + target.stats.Agility);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}
}
