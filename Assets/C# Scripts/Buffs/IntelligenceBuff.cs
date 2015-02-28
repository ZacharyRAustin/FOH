using UnityEngine;
using System.Collections;

public class IntelligenceBuff : Buff {
	
	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Intelligence: " + target.stats.Intelligence);
			target.stats.Intelligence = (int) magnitude + target.stats.Intelligence;
			Debug.Log ("Intelligence: " + target.stats.Intelligence);
		}
		if (elapsedTime >= duration)
		{
			target.stats.Intelligence = target.stats.Intelligence - (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s intelligence returns to normal.");
			Debug.Log ("Intelligence: " + target.stats.Intelligence);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
