using UnityEngine;
using System.Collections.Generic;

public class RandomAbility {

	public string name;
	public Character caster;
	public int targetOption;
	public float range, castTime, coolDown = 10f;
	public int manaCost, damage, healing;
	public Buff buff;
	public float buffMagnitude, buffTime;
	public float remainingCooldownTime = 0f;
	//public GameObject spellProjectilePrefab;

	private bool isInstantCast = false;

	// Use this for initialization
	void Start () {
	
	}

	public void SetAbility (AbilityParameters param)
	{
		targetOption = param.targetOption;
		range = param.range;
		manaCost = Random.Range (param.minManaCost, param.maxManaCost);

		damage = Random.Range (param.minDamage, param.maxDamage);
		healing = Random.Range (param.minHealing, param.maxHealing);
		buff = RandomBuff (param.possibleBuffs);
		SetCastTime (param);
		SetBuffMagnitude (param);
		buffTime = Random.Range (param.minBuffTime, param.maxBuffTime);

		if (buff != null)
		{
			buff.magnitude = buffMagnitude;
			buff.duration = buffTime;
		}

		name = AbilityNameGenerator (buff);
	}

	void SetCastTime (AbilityParameters param)
	{
		if (Random.value < .5) //50% chance for spell to be instant cast and cost more mana
		{
			castTime = 0f;
			manaCost += 5;
			isInstantCast = true;
		}
		else
		{
			castTime = Random.Range (param.minCastTime, param.maxCastTime);
		}
	}

	void SetBuffMagnitude (AbilityParameters param)
	{
		if (buff is StrengthBuff || buff is AgilityBuff || buff is IntelligenceBuff || buff is ArmorBuff || buff is DodgeRateBuff)
		{
			buffMagnitude = (float) Random.Range(param.minBuffAdd, param.maxBuffAdd);
		}
		else if (buff is AttackRateBuff || buff is MoveSpeedBuff)
		{
			buffMagnitude = Random.Range (param.minBuffMagnitude, param.maxBuffMagnitude);
		}

		if (buff != null)
		{
			buff.DebuffSet ();
		}
	}

	public Buff RandomBuff (List<Buff> possibleBuffs)
	{
		bool addBuff = false;
		if (Random.value < .5) //50% chance to have an added buff
		{
			addBuff = true;
		}

		if (addBuff)
		{
			int possibleBuffCount = possibleBuffs.Count;
			int randomBuffIndex = Random.Range(0, possibleBuffCount);
			return possibleBuffs.ToArray () [randomBuffIndex];
		}
		else
		{
			return null;
		}
	}

	string AbilityNameGenerator (Buff buff)
	{
		string name = "Default Ability Name";
		string instantCast = null;
		string actionName = "Default";
		//string damageOrHeal = "Default";
		string descriptor = null;
		string targetdescriptor = null;
		string newline = null;

		if (buff == null)
		{
			if (healing > 0)
			{
				actionName = "Heal";
			}
			else if (damage > 0)
			{
				actionName = "Damage";
			}
		}
		else
		{
			if (buff is StrengthBuff)
			{
				actionName = "Strength";
			}
			else if (buff is AgilityBuff)
			{
				actionName = "Agility";	
			}
			else if (buff is IntelligenceBuff)
			{
				actionName = "Intelligence";	
			}
			else if (buff is AttackRateBuff)
			{
				actionName = "Attack Rate";
			}
			else if (buff is DodgeRateBuff)
			{
				actionName = "Dodge Rate";	
			}
			else if (buff is ArmorBuff)
			{
				actionName = "Armor";	
			}
			else if (buff is MoveSpeedBuff)
			{
				actionName = "Move Speed";	
			}

			if (buffMagnitude > 1)
			{
				descriptor = "Strengthen ";
			}
			else if (buffMagnitude < 1)
			{
				descriptor = "Weaken ";
			}
		}

		if (isInstantCast)
		{
			instantCast = "Instant ";
		}

		if (targetOption == AbilityTargetOption.SELF)
		{
			targetdescriptor = "Self ";
		}
		else if (targetOption == AbilityTargetOption.TARGET_ALLY)
		{
			targetdescriptor = "Ally ";
		}
		else if (targetOption == AbilityTargetOption.TARGET_ENEMY)
		{
			targetdescriptor = "Enemy ";
		}
		else if (targetOption == AbilityTargetOption.TARGET_LOCATION)
		{
			targetdescriptor = "Aimed ";
		}

		string[] values = new string[5]{instantCast, targetdescriptor, newline, descriptor, actionName};

		name = string.Concat (values);

		if (name.Length > 15)
		{
			newline = "\n";
			values = new string[5]{instantCast, targetdescriptor, newline, descriptor, actionName};
			name = string.Concat (values);
		}
		return name;
	}

	public void Print ()
	{
		Debug.Log ("Ability name: " + name);

		if (targetOption == AbilityTargetOption.TARGET_ALLY)
		{
			Debug.Log ("Target option: Target ally");
		}
		else if (targetOption == AbilityTargetOption.TARGET_ENEMY)
		{
			Debug.Log ("Target option: Target enemy");
		}
		else if (targetOption == AbilityTargetOption.SELF)
		{
			Debug.Log ("Target option: Self");
		}
		else if (targetOption == AbilityTargetOption.TARGET_LOCATION)
		{
			Debug.Log ("Target option: Target Location");
		}

		Debug.Log ("Range: " + range);
		Debug.Log ("Cast time: " + castTime);
		Debug.Log ("Mana cost: " + manaCost);
		Debug.Log ("Damage: " + damage);
		Debug.Log ("Healing: " + healing);

		if (buff == null)
		{
			Debug.Log ("No buff");
		}
		else
		{
			Debug.Log ("Buff: " + buff.name);
			Debug.Log ("Buff duration: " + buffTime);
			Debug.Log ("Buff magnitude: " + buffMagnitude);
		}
	}

	public void Resolve (Character targetChar, Vector3 targetLocation)
	{
		Debug.Log ("RandomAbility.Resolve");
		caster.stats.CurrentMana -= manaCost;
		remainingCooldownTime = coolDown;

		if (targetOption == AbilityTargetOption.TARGET_ALLY || targetOption == AbilityTargetOption.TARGET_ENEMY || targetOption == AbilityTargetOption.SELF)
		{
			targetChar.stats.CurrentHealth -= (damage + caster.stats.Intelligence);
			targetChar.stats.CurrentHealth += (healing + caster.stats.Intelligence);

			if (buff != null)
			{
				Buff appliedBuff = new Buff();
				appliedBuff = buff;
				appliedBuff.target = targetChar;
				appliedBuff.elapsedTime = 0f;
				targetChar.stats.AddBuff(appliedBuff);
			}
		}
		else if (targetOption == AbilityTargetOption.TARGET_LOCATION)
		{
			Vector3 spellProjectileDirection = targetLocation - caster.transform.localPosition;
			spellProjectileDirection.Normalize();
			GameObject spellProjectile = (GameObject) Object.Instantiate(Resources.Load("ErekiBall2"), caster.getCharacterPosition(), Quaternion.identity);
			SpellProjectile projectileScript = spellProjectile.GetComponent<SpellProjectile>();
			projectileScript.velocity = 10;
			projectileScript.targetLocation = targetLocation;
			projectileScript.spell = this;
		
		}
	}

	// Update is called once per frame
	public void UpdateRemainingCooldownTime () {
		if (remainingCooldownTime > 0)
		{
			remainingCooldownTime -= Time.deltaTime;
		}
		else
		{
			remainingCooldownTime = 0;
		}
	}

	public void CollisionResolve(Character hitChar)
	{
		if (hitChar.isenemy)
		{
			hitChar.stats.CurrentHealth -= (damage + caster.stats.Intelligence);
			if (buff != null)
			{
				if (buff.debuff)
				{
					Buff appliedBuff = new Buff();
					appliedBuff = buff;
					appliedBuff.target = hitChar;
					appliedBuff.elapsedTime = 0f;
					hitChar.stats.AddBuff(appliedBuff);
				}
			}
		}
		else
		{
			hitChar.stats.CurrentHealth += (healing + caster.stats.Intelligence);
			if (buff != null)
			{
				if (!buff.debuff)
				{
					Buff appliedBuff = buff.CopyBuff(buff);
					appliedBuff.target = hitChar;
					appliedBuff.elapsedTime = 0f;
					hitChar.stats.AddBuff(appliedBuff);
					//Debug.Log ("Buff target is " + buff.target.name);
					Debug.Log ("Applied Buff target is " + appliedBuff.target.name);
					Debug.Log ("Buff duration: " + appliedBuff.duration);
				}
			}
		}
	}

	public void TargetPositionResolve()
	{

	}
}
