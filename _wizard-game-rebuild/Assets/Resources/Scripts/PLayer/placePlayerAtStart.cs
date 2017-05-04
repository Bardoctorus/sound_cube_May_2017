using UnityEngine;
using System.Collections;

public class placePlayerAtStart : MonoBehaviour {

    TestLevelGen testLevelGen;
	// Use this for initialization
	void Start () {
        testLevelGen = Component.FindObjectOfType<TestLevelGen>();
        transform.position = new Vector3(Random.Range(-testLevelGen.spawnLimits.x, testLevelGen.spawnLimits.x), 5, Random.Range(-testLevelGen.spawnLimits.z, testLevelGen.spawnLimits.z)) + testLevelGen.GameAreaOffset;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
