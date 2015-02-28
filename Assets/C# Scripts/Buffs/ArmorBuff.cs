using UnityEngine;
using System.Collections;

public class ArmorBuff : Buff {
	
	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Armor: " + target.stats.Armor);
			target.stats.Armor = (int) magnitude + target.stats.Armor;
			Debug.Log ("Armor: " + target.stats.Armor);
		}
		if (elapsedTime >= duration)
		{
			target.stats.Armor = target.stats.Armor - (int) magnitude; //return agility to normal
			Debug.Log (target.name + "'s armor returns to normal.");
			Debug.Log ("Armor: " + target.stats.Armor);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
		
	}

}
