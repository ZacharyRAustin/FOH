using UnityEngine;
using System.Collections;

public class Buff {

	public string name;
	public Character target;
	public float magnitude, duration, elapsedTime = 0f;
	public bool debuff = false; // if false, treated as "buff" (beneficial), if true, "debuff" (negative) - may matter for other spells
						// ex. a Purge spell that removes debuffs from an ally or buffs from an enemy



	// Resolve function handles application of effects - damage, stat changes, etc.
	public virtual void Resolve () {
		Debug.Log ("Buff virtual resolve - you should not see this");
	}


}
