using UnityEngine;
using System.Collections.Generic;

public class CharacterStats {

	private string name;

	//base stats
	private int baseMaxHealth, baseMaxMana, baseStrength, baseAgility, baseIntelligence;
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
	public Weapon weapon;
	public Armor[] gear = new Armor[3];
	private EquipmentGenerator equipmentGenerator = new EquipmentGenerator();

	//modifiers
	public int healthMod = 0, manaMod = 0, strMod = 0, agiMod = 0, intMod = 0, damageMod = 0, armorMod = 0, magResMod = 0;
	public int hitMod = 0, critMod = 0, dodgeMod = 0;
	public float attackRateMod = 1f, moveSpeedMod = 1f;

    public CharacterStats() {

    }

    public CharacterStats(int level) {
        baseMaxHealth = 60 * level;
        baseMaxMana = 30 * level;
        baseStrength = 5 * level;
        baseAgility = 5 * level;
        baseIntelligence = 3 * level;
        expYield = 4 * level;
        InitializeEquipment();
        CalculateCombatStats();
        InitializeCombatStats();
    }


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
			return intelligence + weapon.damage + damageMod;
		}
		else
		{
			return strength + weapon.damage + damageMod;
		}
	}

	private int calculateHitRate ()
	{
		if (weapon.hitRate + agility + hitMod < 100)
		{
			return weapon.hitRate + agility + hitMod;
		}
		else
		{
			return 100;
		}
	}

	private int calculateCritRate ()
	{
		if (agility + weapon.critRate + critMod < 100)
		{
			return agility + weapon.critRate + critMod;
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

	private int calculateArmor()
	{
		return weapon.armor + gear[0].armor + gear[1].armor + gear[2].armor + armorMod;
	}

	private int calculateMagicResist()
	{
		return weapon.magicResist + gear[0].magicResist + gear[1].magicResist + gear[2].magicResist + magResMod;
	}

	private float calculateAttackRate ()
	{
		float agiAttackSpeedBonus = 0f;
		return (weapon.attackRate - agiAttackSpeedBonus) * attackRateMod;
	}

	public void InitializeBaseStats ()
	{
		baseMaxHealth = 50;
		baseMaxMana = 30;
		baseStrength = 5;
		baseAgility = 5;
		baseIntelligence = 5;
	}

	public void InitializeTrollBaseStats()
	{
		baseMaxHealth = 60;
		baseMaxMana = 30;
		baseStrength = 5;
		baseAgility = 5;
		baseIntelligence = 3;
		expYield = 4;
	}

	public void InitializeEquipment ()
	{
		weapon = equipmentGenerator.basicSword;
		gear [0] = equipmentGenerator.basicArmor;
		gear [1] = equipmentGenerator.basicArmor;
		gear [2] = equipmentGenerator.basicArmor;
	}

	public void InitializeCombatStats ()
	{
		currentHealth = maxHealth;
		currentMana = maxMana;
	}

	public void CalculateCombatStats ()
	{
		maxHealth = baseMaxHealth + weapon.health + gear[0].health + gear[1].health + gear[2].health + healthMod;
		maxMana = baseMaxMana + weapon.mana + gear[0].mana + gear[1].mana + gear[2].mana + manaMod;
		strength = baseStrength + weapon.strength + gear[0].strength + gear[1].strength + gear[2].strength + strMod;
		agility = baseAgility + weapon.agility + gear[0].agility + gear[1].agility + gear[2].agility + agiMod;
		intelligence = baseIntelligence + weapon.intelligence + gear[0].intelligence + gear[1].intelligence + gear[2].intelligence + intMod;
		attackDamage = calculateAttackDamage();
		armor = calculateArmor ();
		magicResist = calculateMagicResist ();
		hitRate = calculateHitRate();
		critRate = calculateCritRate();
		dodgeRate = calculateDodgeRate ();
		attackRate = calculateAttackRate();
		attackRange = weapon.attackRange;
		moveSpeed = 2.0f * moveSpeedMod;
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

	public void PrintEquipment ()
	{
		Debug.Log (name + "'s equipment: ");
		weapon.Print ();
		gear [0].Print ();
		gear [1].Print ();
		gear [2].Print ();
	}

	public void LevelHealth()
	{
		baseMaxHealth += 5;
	}

	public void LevelMana()
	{
		baseMaxMana += 5;
	}

	public void LevelStrength()
	{
		baseStrength += 1;
	}

	public void LevelAgility()
	{
		baseAgility += 1;
	}

	public void LevelIntelligence()
	{
		baseIntelligence += 1;
	}

}
