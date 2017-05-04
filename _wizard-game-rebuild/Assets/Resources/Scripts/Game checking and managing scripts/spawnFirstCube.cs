using UnityEngine;
using System.Collections;

public class spawnFirstCube : MonoBehaviour {

    public GameObject cube;
    public int heightoffset = 4;
    TestLevelGen testLevelGen;
    calculateCubesPerPlinth cubeCalc;
    assignNote assNote;

    int spawnedhere = 0;

    int numberToSpawnOnthisPlinth;
    void Awake()
    {
        testLevelGen = GetComponent<TestLevelGen>();
        cubeCalc = GetComponent<calculateCubesPerPlinth>();
        assNote = GetComponent<assignNote>();
    }

    public void FirstCubeSpawn(Vector3 refPlinthLocation)
    {
        
        
        cubeCalc.NewRandom();

        //test to see if we have enough cubes for a regular plinth
        if (testLevelGen.howManyCubePairs - testLevelGen.howManyCubePairsSpawned >= cubeCalc.randomcubesPerPlinth)
            {
            //number to spawn is -1 as we already spawns one up there
           
                numberToSpawnOnthisPlinth = (int)cubeCalc.randomcubesPerPlinth-1;
                for (int i = 0; i < numberToSpawnOnthisPlinth; i++)
                {
                    cubeSpawn(refPlinthLocation);
                    spawnedhere += 1;
       
                }
                
            }

        if(testLevelGen.howManyPlinthPairs==testLevelGen.howManyPlinthPairsSpawned&&testLevelGen.howManyCubePairs-testLevelGen.howManyCubePairsSpawned>0)
        {
            LeftoverCubes();
        }
      
      }

    public void LeftoverCubes()
    {
       // Debug.Log("leftover cubes = " + (testLevelGen.howManyCubePairs - testLevelGen.howManyCubePairsSpawned));
        int leftoverCubes = testLevelGen.howManyCubePairs - testLevelGen.howManyCubePairsSpawned;
        for (int i = 0; i < leftoverCubes; i++)
        {
            GameObject g = testLevelGen.spawnedPlinths[Random.Range(0,testLevelGen.spawnedPlinths.Count)];
            cubeSpawn(g.transform.position);
        }
    }

    public void cubeSpawn(Vector3 refPlinthLocation)
    {
        //incrementing the height offset so they don't collide
        heightoffset += 1;

        //offsetting the cubes so they fall in the right places
        Vector3 refCubeSpawnPoint = refPlinthLocation + new Vector3(0, heightoffset, 0);
        Vector3 atCubeSpawnPoint = new Vector3(Random.Range(-testLevelGen.spawnLimits.x, testLevelGen.spawnLimits.x), 20, Random.Range(-testLevelGen.spawnLimits.z, testLevelGen.spawnLimits.z)) +testLevelGen.GameAreaOffset;
        //instantiate them
        GameObject refCube = Instantiate(cube, refCubeSpawnPoint, Quaternion.identity)as GameObject;
        GameObject atCube = Instantiate(cube, atCubeSpawnPoint, Quaternion.identity) as GameObject;

        testLevelGen.howManyCubePairsSpawned += 1;

        assNote.noteAssign(refCube, atCube);


    }
}
