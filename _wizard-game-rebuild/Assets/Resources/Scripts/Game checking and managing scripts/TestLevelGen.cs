using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TestLevelGen : MonoBehaviour
{
    
    public enum EndLevelTune
    {//These must match the filenames exactly or they won't work.
        NiceCLoop,//120bpm
        sillyfloop135//135bpm
    }
    [Header("End Level Music")]
    public EndLevelTune endLevelTune;


    [Header("Number of Plinths and Cubes")]
    public int howManyPlinthPairs;
    
    public int howManyCubePairs;
   
    [SerializeField]
    int spawnFails = 0;

    [SerializeField]
    public int howManyPlinthPairsSpawned = 0;
    [SerializeField]
    public int howManyCubePairsSpawned = 0;


    //  vars for instantiation
    spawnFirstCube spawn1stcube;
    //plinth things
    [Header("Plinths prefabs and other stuff")]
    public GameObject MemoryPrefab;
    public GameObject RealityPrefab;
    public GameObject RefPlParent;
    public GameObject AttemptPlParent;
    public GameObject Plinth;
    public Vector3 spawnLimits { get; protected set; }

    public List<GameObject> spawnedPlinths;

   public LayerMask layerMask;


    //offset for how far away the game area is from the memory area
    public Vector3 GameAreaSize =new Vector3( 1,1,1);
    public Vector3 GameAreaOffset = new Vector3(-500, 0, 0);

    //spawn point vector 3s
    Vector3 refRandom;
    Vector3 attemptRandom;

    AudioSource myAudioSource;
   
    

    
          
   

    void Awake()
    {
        //load whichever tune you have in the enum
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = Resources.Load("Sounds/" + endLevelTune, typeof(AudioClip)) as AudioClip;

        //getting the scripts for generation
        spawn1stcube = GetComponent<spawnFirstCube>();
        spawnedPlinths = new List<GameObject>();


    }

    void Start()
    {
       GameObject Memory = Instantiate(MemoryPrefab, Vector3.zero, Quaternion.identity) as GameObject;
       GameObject Reality = Instantiate(RealityPrefab, GameAreaOffset, Quaternion.identity) as GameObject;

     foreach(Transform child in Memory.transform)
        {
            if(child.gameObject.tag=="PlinthParent")
            {
                RefPlParent = child.gameObject;
            }
        }
        foreach (Transform child in Reality.transform)
        {
            if (child.gameObject.tag == "PlinthParent")
            {
                AttemptPlParent = child.gameObject;
            }
        }

        Memory.transform.localScale =  GameAreaSize;
        Reality.transform.localScale = GameAreaSize;

        //this needs to now make a vector 3 rather than int
        spawnLimits = ExtensionMethods.SpawnSpace(GameAreaSize);
       // Debug.Log(spawnLimits);

        SpawnPlinthPair();
    }

    public void SpawnPlinthPair()
    {
        //Spawn position checking and failsafe if level isn't viable
       
        if (spawnFails >= 10000)
        {
            Debug.LogError("Spawn failed 10000 times, terminating");
            return;
        }
        refRandom = new Vector3(Random.Range(-spawnLimits.x, spawnLimits.x), spawnLimits.y, Random.Range(-spawnLimits.z, spawnLimits.z));

        Collider[] cols = Physics.OverlapSphere(refRandom, 1.5f,layerMask,QueryTriggerInteraction.Collide);
        foreach (Collider col in cols)
        {
            //how can this work when the object doesn't get it's tag until further down....EDIT your dumb. The first one always finds a place.
            //worth bearing in mind when you have other terrain though, need to check against that too.
            if (col.gameObject.tag == "Reference")
            {
               
                //Debug.Log("Spawn failed at: " + refRandom);
                spawnFails += 1;
                StartCoroutine(PauseAfterSpawnFail());
                return;
            }
        }
      

        //spawning reference plinth
        GameObject a = Instantiate (Plinth, refRandom, Quaternion.Euler(new Vector3(0, Random.Range(30,300), 0))) as GameObject;
        a.tag = ("Reference");
        a.name = "P" + howManyPlinthPairsSpawned;
       // a.transform.SetParent(RefPlParent.transform);
        //add to spawned plinths list for deistrubing leftover cubes.
        spawnedPlinths.Add(a);



        attemptRandom = refRandom + GameAreaOffset;
        GameObject b = Instantiate(Plinth, attemptRandom, Quaternion.Euler(new Vector3(0, Random.Range(30, 300), 0))) as GameObject;
        b.name = "P" + howManyPlinthPairsSpawned;
        b.tag = ("Attempt");
        //b.transform.SetParent(AttemptPlParent.transform);
        

        howManyPlinthPairsSpawned += 1;
    

        if(howManyCubePairsSpawned<howManyCubePairs)
        {
            spawn1stcube.FirstCubeSpawn(refRandom);
        }
        if (howManyPlinthPairsSpawned < howManyPlinthPairs)
        {
            SpawnPlinthPair();
        }

    }
    IEnumerator PauseAfterSpawnFail()
    {
        yield return new WaitForEndOfFrame();
        SpawnPlinthPair();
    }

   



}
