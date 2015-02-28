using UnityEngine;
using System.Collections;

public class AgilityBuff : Buff {

	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Agility: " + target.stats.Agility);
			target.stats.Agility = (int) magnitude + target.stats.Agility;
			Debug.Log ("Agility: " + target.stats.Agility);
		}
		if (elapsedTime >= duration)
		{
			target.stats.Agility = target.stats.Agility - (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s agility returns to normal.");
			Debug.Log ("Agility: " + target.stats.Agility);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}
}
