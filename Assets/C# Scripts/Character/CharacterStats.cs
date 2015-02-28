using UnityEngine;
using System.Collections.Generic;

public class CharacterStats {

	private string name;

	//base stats
	private int maxHealth, maxMana, strength, agility, intelligence;

	//progression stats
	private int level, currentExp, maxExp, expYield, unallocatedStatPoints;

	//combat stats
	private int currentHealth, currentMana, armor, magicResist;
	private int attackDamage;
	private int hitRate;
	private int critRate;
	private int dodgeRate;
	private float attackRange = 1.30f, attackRate = 1f, moveSpeed = 1f;
	private bool magicAttack;

	public List<RandomAbility> abilities = new List<RandomAbility>();
	private List<Buff> buffs = new List<Buff>();

	//base stat properties
	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public int MaxHealth
	{
		get { return maxHealth; }
		set { maxHealth = value;}
	}
	
	public int MaxMana
	{
		get { return maxMana; }
		set { maxMana = value; }
	}
	
	public int Strength
	{
		get { return strength; }
		set { strength = value; }
	}
	
	public int Agility
	{
		get { return agility; }
		set { agility = value; }
	}
	
	public int Intelligence
	{
		get { return intelligence; }
		set { intelligence = value; }
	}

	//progression stat properties
	public int Level
	{
		get { return level; }
		set { level = value; }
	}

	public int CurrentExp
	{
		get { return currentExp; }
		set { currentExp = value; }
	}

	public int MaxExp
	{
		get { return maxExp; }
		set { maxExp = value; }
	}

	public int ExpYield
	{
		get { return expYield; }
		set { expYield = value; }
	}

	public int UnallocatedStatPoints
	{
		get { return unallocatedStatPoints; }
		set { unallocatedStatPoints = value; }
	}

	//combat stat properties

	public int CurrentHealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}
	
	public int CurrentMana
	{
		get { return currentMana; }
		set { currentMana = value; }
	}

	public int AttackDamage
	{
		get { return attackDamage; }
		set { attackDamage = value; }
	}

	public int HitRate
	{
		get { return hitRate; }
		set { hitRate = value; }
	}

	public int CritRate
	{
		get { return critRate; }
		set { critRate = value; }
	}

	public int DodgeRate
	{
		get { return dodgeRate; }
		set { dodgeRate = value; }
	}

	public int Armor
	{
		get { return armor; }
		set { armor = value; }
	}

	public int MagicResist
	{
		get { return magicResist; }
		set { magicResist = value; }
	}

	public float AttackRange
	{
		get { return attackRange; }
		set { attackRange = value;}
	}

	public float AttackRate
	{
		get { return attackRate; }
		set { attackRate = value;}
	}

	public float MoveSpeed
	{
		get { return moveSpeed; }
		set { moveSpeed = value;}
	}

	public bool MagicAttack
	{
		get { return magicAttack; }
		set { magicAttack = value;}
	}

	private int calculateAttackDamage ()
	{
		if (magicAttack == true)
		{
			return intelligence;
		}
		else
		{
			return strength;
		}
	}

	private int calculateHitRate ()
	{
		if (60 + agility < 100)
		{
			return 60 + agility;
		}
		else
		{
			return 100;
		}
	}

	private int calculateCritRate ()
	{
		if (agility < 100)
		{
			return agility;
		}
		else
		{
			return 100;
		}
	}

	private int calculateDodgeRate ()
	{
		if (agility < 100)
		{
			return agility;
		}
		else
		{
			return 100;
		}
	}

	public void InitializeBaseStats ()
	{
		maxHealth = 50;
		maxMana = 30;
		strength = 5;
		agility = 5;
		intelligence = 5;
	}

	public void InitializeCombatStats ()
	{
		currentHealth = maxHealth;
		currentMana = maxMana;
		attackDamage = calculateAttackDamage ();
		critRate = calculateCritRate ();
		dodgeRate = calculateDodgeRate ();
		hitRate = calculateHitRate ();
		armor = 3;
		magicResist = 0;
		magicAttack = false;
		attackRange = 1.3f;
		attackRate = 1.0f;
		moveSpeed = 2.0f;
	}

	public void InitializeProgressionStats ()
	{
		level = 1;
		currentExp = 0;
		maxExp = 10;
		unallocatedStatPoints = 0;
	}

	// Ability and Buff list management
	public RandomAbility GetAbility (int index)
	{
		if(-1 < index && index < abilities.Count)
		{
			return abilities.ToArray()[index];
		}
		else
		{
			Debug.Log("Invalid ability index " + index + " for size " + abilities.Count);
			return null;
		}
	}

	public void AddAbility (RandomAbility s)
	{
		if (abilities.Count < 5)
		{
			abilities.Add (s);
		}
		else
		{
			Debug.Log ("This character already has 5 abilities!");
		}
	}

	public void AddBuff (Buff buff)
	{
		if (buffs.Contains(buff)) //buffs cannot stack
		{
			buff.elapsedTime = .01f; //reset duration, but do not set to 0
		}
		else
		{
			buffs.Add (buff);
		}
	}

	public void RemoveBuff (Buff buff)
	{
		buffs.Remove (buff);

	}

	public void ResolveBuffs ()
	{
		foreach (Buff buff in buffs)
		{
			buff.Resolve();
		}
	}

}
