using UnityEngine;
using System.Collections.Generic;

public class AbilityParameters {

	public int targetOption;
	public float range = 0, minCastTime = 0, maxCastTime = 3, minCooldown = 3, maxCooldown = 10;
	public int minManaCost = 0, maxManaCost = 0, minDamage = 0, maxDamage = 0, minHealing = 0, maxHealing = 0;
	public float minBuffMagnitude = .5f, maxBuffMagnitude = 2f, minBuffTime = 2f, maxBuffTime = 10f;
	public int minBuffAdd = -10, maxBuffAdd = 10;
	public List<Buff> possibleBuffs = new List<Buff>();

	AttackRateBuff attackRateBuff = new AttackRateBuff();
	DodgeRateBuff dodgeRateBuff = new DodgeRateBuff();
	MoveSpeedBuff moveSpeedBuff = new MoveSpeedBuff();
	AgilityBuff agilityBuff = new AgilityBuff ();
	ArmorBuff armorBuff = new ArmorBuff();
	IntelligenceBuff intelligenceBuff = new IntelligenceBuff();
	StrengthBuff strengthBuff = new StrengthBuff();

	void InitializePossibleBuffs()
	{
		possibleBuffs.Add (attackRateBuff);
		possibleBuffs.Add (dodgeRateBuff);
		possibleBuffs.Add (moveSpeedBuff);
		possibleBuffs.Add (agilityBuff);
		possibleBuffs.Add (armorBuff);
		possibleBuffs.Add (intelligenceBuff);
		possibleBuffs.Add (strengthBuff);
	}

	public AbilityParameters (int x)
	{
		targetOption = x;
		range = 5;
		minManaCost = 5;
		maxManaCost = 10;
		InitializePossibleBuffs ();

		if (x == AbilityTargetOption.TARGET_ENEMY)
		{
			minDamage = 5;
			maxDamage = 15;
		}
		else if (x == AbilityTargetOption.TARGET_ALLY)
		{
			minHealing = 5;
			maxHealing = 15;
		}
		else if (x == AbilityTargetOption.SELF)
		{
			minHealing = 10;
			maxHealing = 15;
			maxManaCost = 6;
		}
		else
		{

		}
	}

}
