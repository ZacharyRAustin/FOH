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
			instantCast = "Flash ";
		}

		string[] values = new string[3]{instantCast, descriptor, actionName};

		name = string.Concat (values);

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
		targetChar.stats.CurrentHealth -= (damage + caster.stats.Intelligence);
		targetChar.stats.CurrentHealth += (healing + caster.stats.Intelligence);
		if (buff != null)
		{
			targetChar.stats.AddBuff(buff);
			buff.target = targetChar;
			buff.elapsedTime = 0f;
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
}
