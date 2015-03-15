using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratedItemTest{
    public static void PerformTest() {
        WriteFile f = new WriteFile(@"C:\users\zachary\desktop\items.txt");
        for(int i = 1; i < 100; i++)
        {
            List<string> items = new List<string>();

            for(int j = 0; j < 10; j++)
            {
                SpawnCharacteristics.setAvgEnemyLevel(i);
                DropSystem.GenerateLoot();
                
                List<RandomAbility> abilities = DropSystem.getGeneratedAbilities();
                for(int k = 0; k < abilities.Count; k++)
                {
                    items.Add("Ability: " + abilities[k].name);
                }
                List<Equipment> equip = DropSystem.getGeneratedEquipment();
                for (int h = 0; h < equip.Count; h++)
                {
                    items.Add("Item: " + equip[h].name + " with level " + equip[h].level);
                }
                if(items.Count != 0)
                {
                    f.writeLine("For Level: " + i + " iteration " + j);
                    f.writeLine("Average Enemy Level: " + SpawnCharacteristics.getAvgEnemyLevel());
                    items.Add("\n\n");
                    f.writeAllLines(items);
                }

                items.Clear();
            }
        }
    }
}
