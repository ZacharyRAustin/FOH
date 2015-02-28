using UnityEngine;
using System.Collections;

public class StrengthBuff : Buff {

	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Strength: " + target.stats.Strength);
			target.stats.Strength = (int) magnitude + target.stats.Strength;
			Debug.Log ("Strength: " + target.stats.Strength);
		}
		if (elapsedTime >= duration)
		{
			target.stats.Strength = target.stats.Strength - (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s strength returns to normal.");
			Debug.Log ("Strength: " + target.stats.Strength);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
