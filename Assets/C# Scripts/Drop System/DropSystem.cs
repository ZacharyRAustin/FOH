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
        s.SetAbility(p);
        abilities.Add(s);
    }

    private static void generateRandomEquipment() {
        if (Random.value <= 0.5)
        {
            equipment.Add(EquipmentGenerator.GenerateWeapon(getNextLevel()));
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
            if(Random.Range(0, 1) < 0.45)
            {
                if(Random.Range(0, 1) > 0.5)
                {
                    generateRandomAbility();
                }
                else
                {
                    generateRandomEquipment();
                }
            }
        }
    }

    public static void printGenerated() {
        foreach(Equipment e in equipment)
        {
            Debug.Log("Generated " + e.name + " with level " + e.level);
        }

        foreach(RandomAbility r in abilities)
        {
            Debug.Log("Genereated " + r.name);
        }
    }

    public static List<Equipment> getGeneratedEquipment() {
        return equipment;
    }

    public static List<RandomAbility> getGeneratedAbilities() {
        return abilities;
    }
}
