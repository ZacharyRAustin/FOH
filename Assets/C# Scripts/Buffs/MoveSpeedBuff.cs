using UnityEngine;
using System.Collections;

public class MoveSpeedBuff : Buff {

	public override void Resolve () {
		if (elapsedTime == 0) //upon activation
		{
			Debug.Log ("Move speed: " + target.stats.MoveSpeed);
			Debug.Log (target.name + " is slowed! Move speed multiplied by " + magnitude);
			target.stats.MoveSpeed = magnitude * target.stats.MoveSpeed;
			Debug.Log ("Move speed: " + target.stats.MoveSpeed);
		}
		if (elapsedTime >= duration)
		{
			target.stats.MoveSpeed = target.stats.MoveSpeed / magnitude; //return speed to normal
			Debug.Log (target.name + "'s movement speed returns to normal.");
			Debug.Log ("Move speed: " + target.stats.MoveSpeed);
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;

	}
	

}
