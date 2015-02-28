using UnityEngine;
using System.Collections;

public class AttackRateBuff : Buff {

	// Use this for initialization
	void Start () {
	
	}

	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Attack rate: " + target.stats.AttackRate);
			target.stats.AttackRate = magnitude * target.stats.AttackRate;
			Debug.Log ("Attack rate: " + target.stats.AttackRate);
		}
		if (elapsedTime >= duration)
		{
			target.stats.AttackRate = target.stats.AttackRate / magnitude;
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
