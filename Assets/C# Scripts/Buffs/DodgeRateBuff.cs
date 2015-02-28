using UnityEngine;
using System.Collections;

public class DodgeRateBuff : Buff {

	// Use this for initialization
	void Start () {
	
	}

	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Dodge rate: " + target.stats.DodgeRate);
			float floatDodgeRate = magnitude * (float) target.stats.DodgeRate;
			target.stats.DodgeRate = (int) floatDodgeRate;
			Debug.Log ("Dodge rate: " + target.stats.DodgeRate);
		}
		if (elapsedTime >= duration)
		{
			float floatDodgeRate = (float) target.stats.DodgeRate / magnitude; //return speed to normal
			target.stats.DodgeRate = (int) floatDodgeRate;
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
