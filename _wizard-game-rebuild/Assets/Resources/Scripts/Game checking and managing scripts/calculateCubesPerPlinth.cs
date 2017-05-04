using UnityEngine;
using System.Collections;

public class calculateCubesPerPlinth : MonoBehaviour {

    TestLevelGen testlevelgen;
   public float cubesPerPlinth { get; protected set; }
   public float remainder { get; protected set; }

    public float randomcubesPerPlinth { get; protected set; }


    // Use this for initialization
    void Awake ()
    {
        testlevelgen = GetComponent<TestLevelGen>();

        cubesPerPlinth = ExtensionMethods.NumberPerPlinthsMinus1(testlevelgen.howManyPlinthPairs, testlevelgen.howManyCubePairs);
        remainder = ExtensionMethods.RemainderForLastPlinth(testlevelgen.howManyPlinthPairs, testlevelgen.howManyCubePairs, cubesPerPlinth);

        randomcubesPerPlinth = ExtensionMethods.RoundWithRandom(testlevelgen.howManyPlinthPairs, testlevelgen.howManyCubePairs);

    }

    public void NewRandom()
    {
        randomcubesPerPlinth = ExtensionMethods.RoundWithRandom(testlevelgen.howManyPlinthPairs, testlevelgen.howManyCubePairs);
    }
	
	
}
