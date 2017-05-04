using UnityEngine;
using UnityStandardAssets.ImageEffects;

using System.Collections;


public class teleportToMemory : MonoBehaviour {

    bool inMemory = false;
    public AudioSource bloop;
    playerRh1 playerHand;
    TestLevelGen testlevgen;
    ColorCorrectionCurves col;
    MotionBlur mo;
    Camera cam;
    Light flashLight;
    
    
	// Use this for initialization
	void Start ()
    {
        
        playerHand = GetComponent<playerRh1>();
        testlevgen = Component.FindObjectOfType<TestLevelGen>();
        col = GetComponentInChildren<ColorCorrectionCurves>();
        flashLight = GetComponentInChildren<Light>();
        mo = GetComponentInChildren<MotionBlur>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
	if(Input.GetKeyDown(KeyCode.Q))
        {
            bloop.Play();
            Teleport();
        }
	}

    void Teleport()
    {
        
        if (inMemory == false)
        {
            playerHand.DropCube();

            inMemory = true;
            transform.position = new Vector3(Random.Range(-testlevgen.spawnLimits.x, 
                testlevgen.spawnLimits.x), 3, Random.Range(-testlevgen.spawnLimits.z, 
                testlevgen.spawnLimits.z));
            col.enabled = true;
            flashLight.enabled = true;
            mo.enabled = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
            
            
        }
        else
        {
            playerHand.DropCube();
            inMemory = false;
            transform.position = new Vector3(Random.Range(-testlevgen.spawnLimits.x, 
                testlevgen.spawnLimits.x), 3, Random.Range(-testlevgen.spawnLimits.z, 
                testlevgen.spawnLimits.z))+testlevgen.GameAreaOffset;
            col.enabled = false;
            flashLight.enabled = false;
                mo.enabled = false;
            cam.clearFlags = CameraClearFlags.Skybox;

        }

    }
}
