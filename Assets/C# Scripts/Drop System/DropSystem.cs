using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropSystem{

    private static List<Equipment> equipment = new List<Equipment>();
    private static List<RandomAbility> abilities = new List<RandomAbility>();
    private static int averageLevel = 1;
    private static int maxSpawn;

    private static void generateRandomAbility() {
        AbilityParameters p = new AbilityParameters(Random.Range(1, 5));
        RandomAbility s = new RandomAbility();
		//levelchange.push_abilities (s.name);
        s.SetAbility(p);
        abilities.Add(s);
    }

    private static void generateRandomEquipment() {
        if (Random.value <= 0.5)
        {
            Equipment a = (EquipmentGenerator.GenerateWeapon(getNextLevel()));
			MyConsole.NewMessage(a.name);
			equipment.Add(a);
        }
        else
        {
            equipment.Add(EquipmentGenerator.GenerateArmor(getNextLevel()));
        }
    }

    private static int getNextLevel() {
        return System.Math.Max(1, (Random.Range(-3, 3) + averageLevel));
    }

    private static void calcAverageLevel() {
        averageLevel = System.Math.Max(1, (Random.Range(-3, 4) + SpawnCharacteristics.getAvgEnemyLevel()));
    }

    public static void GenerateLoot() {
        equipment.Clear();
        abilities.Clear();
        calcAverageLevel();
		levelchange.showlayout ();

        float v = Random.value;
        if(v <= 0.25)
        {
            return;
        }
        else if(v > 0.95)
        {
            averageLevel += Random.Range(0, 3);    
        }

        maxSpawn = Random.Range(1, SpawnCharacteristics.getMaxEnemies()+1);

        addLoot();
    }

    private static void addLoot() {

        for(int i = 0; i < maxSpawn; i++)
        {
            if(Random.Range(0, 101) < 45)
            {
                if(Random.Range(0, 101) > 50)
                {
					generateRandomAbility();
					MyConsole.NewMessage("Generated ability");

                }
                else
                {
					generateRandomEquipment();
                }
            }
        }
    }

    public static void printGenerated() {
		MyConsole.NewMessage ("printGenerated");

        foreach(Equipment e in equipment)
        {
			levelchange.push_equipment(e);
			MyConsole.NewMessage("Generated " + e.name + " with level " + e.level);
        }

        foreach(RandomAbility r in abilities)
        {
			levelchange.push_abilities(r);
            MyConsole.NewMessage("Genereated " + r.name);
        }

    }



    public static List<Equipment> getGeneratedEquipment() {
        return equipment;
    }

    public static List<RandomAbility> getGeneratedAbilities() {
        return abilities;
    }
}
