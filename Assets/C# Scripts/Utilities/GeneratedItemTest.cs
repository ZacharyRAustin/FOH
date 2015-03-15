using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratedItemTest{
    public static void PerformTest() {
        WriteFile f = new WriteFile(@"C:\users\zachary\desktop\items.txt");
        for(int i = 1; i < 100; i++)
        {
            List<int> numGenerated = new List<int>();
            List<string> items = new List<string>();

            for(int j = 0; j < 10; j++)
            {
                SpawnCharacteristics.testLeveling(i);
            }
        }
    }
}
