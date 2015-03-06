using UnityEngine;
using System.Collections;

public class Buff {

	public string name;
	public Character target;
	public float magnitude, duration, elapsedTime = 0f;
	public bool debuff = false; // if false, treated as "buff" (beneficial), if true, "debuff" (negative) - may matter for other spells
						// ex. a Purge spell that removes debuffs from an ally or buffs from an enemy

	public Buff CopyBuff (Buff b)
	{
		if (b is AgilityBuff)
		{
			AgilityBuff b2 = new AgilityBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;

			b2.elapsedTime = 0;

			return b2;
		}
		else if (b is StrengthBuff)
		{
			StrengthBuff b2 = new StrengthBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is StrengthBuff)
		{
			StrengthBuff b2 = new StrengthBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is IntelligenceBuff)
		{
			IntelligenceBuff b2 = new IntelligenceBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is DodgeRateBuff)
		{
			DodgeRateBuff b2 = new DodgeRateBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is ArmorBuff)
		{
			ArmorBuff b2 = new ArmorBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is AttackRateBuff)
		{
			AttackRateBuff b2 = new AttackRateBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else if (b is MoveSpeedBuff)
		{
			MoveSpeedBuff b2 = new MoveSpeedBuff();
			b2.debuff = b.debuff;
			b2.duration = b.duration;
			b2.magnitude = b.magnitude;
			b2.name = b.name;
			
			b2.elapsedTime = 0;
			
			return b2;
		}
		else
		{
			return null;
		}

	}

	// Resolve function handles application of effects - damage, stat changes, etc.
	public virtual void Resolve () {
		Debug.Log ("Buff virtual resolve - you should not see this");
	}

	public virtual void DebuffSet()
	{

	}


}
