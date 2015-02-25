using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelingTest {
    public static void PerformTest() {
        WriteFile f = new WriteFile(@"C:\users\zachary\desktop\leveling.txt");
        for(int i = 1; i < 100; i++)
        {
            List<int> numGenerated = new List<int>();
            List<string> levels = new List<string>();
            for(int j = 0; j < 10; j++)
            {
                //set character level and calculate options
                int enemiesGenerated = SpawnCharacteristics.testLeveling(i);
                numGenerated.Add(enemiesGenerated);

                f.writeLine("For Level " + i + " iteration " + j + ": Number Enemies Generated = " + enemiesGenerated);

                for(int k = 0; k < enemiesGenerated; k++)
                {
                    int l = SpawnCharacteristics.generateNewEnemyLevel();
                    levels.Add("Enemy " + k + " has level " + l);
                }
                levels.Add("\n\n");
                f.writeAllLines(levels);

                numGenerated.Clear();
                levels.Clear();
            }
        }
    }

    private static void calculateStats() {

    }
}
