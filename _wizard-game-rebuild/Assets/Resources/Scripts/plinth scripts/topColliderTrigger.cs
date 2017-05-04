using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class topColliderTrigger : MonoBehaviour {

    public List<GameObject> list; 
    [Header("Loop Time Values")]
    public float loopTime = 4f;
    public float chordSpreadLimit = .3f;

    [Header("Dodgy Loop Logic")]
 
    bool canLoop;
    bool isLooping = false;

    CheckWinScript checkWinScript;
   
    playerRh1 playerHand;

  public  Dictionary<string,string> topColDict;

    GameObject namesake;
  Dictionary<string, string> namesakeDict;
    bool dictsMatch = false;
    public bool plinthCOmplete;
    public bool plinthLocked;
    float lightIntensity = 4f;
    float howLongWaitAtStart = 2f;
    teleportToMemory tele;

    PlinthNewButton plinthButton;


    void Awake()
    {
        canLoop = true;
    }
    
  

    void Start()
    {
        plinthButton = GetComponentInChildren<PlinthNewButton>();
              
        list= new List<GameObject>();
       
        checkWinScript = Component.FindObjectOfType<CheckWinScript>();
        playerHand = Component.FindObjectOfType<playerRh1>();
        topColDict = new Dictionary<string, string>();
        namesake = GameObject.Find(transform.parent.name);
       // myBox = GetComponent<BoxCollider>();
     
        StartCoroutine(WaitForLevelSetUp(howLongWaitAtStart));
        tele = Component.FindObjectOfType<teleportToMemory>();
        

    }

    IEnumerator WaitForLevelSetUp(float t)
    {
        yield return new WaitForSeconds(t);
        LateLevelSetup();
    }

    void LateLevelSetup()
    {
        if (transform.parent.tag == "Reference")
        {
            plinthButton.ButtonSelected();
        }
        if (transform.parent.tag=="Reference" && topColDict.Count==0)
        {
           // Debug.Log(topColDict.Count);
           checkWinScript.RemoveEmptyPlinthsFromWinCondition();
            LightOn(false);  
        }

        foreach (GameObject g in list)
        {
            if (g.GetComponent<tempNoteCube>() == null)
            {
                Debug.LogError("Cannot find the Note Cube Script to report Plinth location");
            }
            else
            {
                g.GetComponent<tempNoteCube>().MyPlinthLocation();
            }
        }

        tele.enabled = true;

    }

    void OnTriggerEnter(Collider col)   
    {              
        if(col.gameObject.CompareTag("noteCube") && plinthCOmplete == false || col.gameObject.CompareTag("noteCubeRef")&&plinthCOmplete==false)
        {
            col.gameObject.GetComponent<AudioSource>().Play();
            if(col.gameObject.GetComponent<AudioSource>().clip==null)
            {
                return;
            }
            string currentNoteName = col.gameObject.GetComponent<AudioSource>().clip.ToString();
            //Debug.Log(currentNoteName);

           

            //add to list of notes playing
            list.Add(col.gameObject);

            //Dictionary stuff
            if (transform.parent.tag=="Reference")
            {
                col.gameObject.tag = ("noteCubeRef");
                
                topColDict[currentNoteName] = transform.parent.name;
            }
            if (transform.parent.tag == "Attempt")
            {
                topColDict[currentNoteName] = transform.parent.name;
               // Debug.Log("enter check");
                Check();
            }

            //internal light
            col.gameObject.GetComponentInChildren<testLightLerp>().Flash();
        }
      
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("noteCube") && plinthCOmplete == false || col.gameObject.CompareTag("noteCubeRef") && plinthCOmplete == false)
        {
            //Tell the cube to break its bond with its orbiting point and reset the availability of that point
            col.gameObject.GetComponent<attachCubeToSpinPoint>().Disconnect();

            if (col.gameObject.GetComponent<AudioSource>().clip == null)
            {
                return;
            }
            string currentNoteName = col.gameObject.GetComponent<AudioSource>().clip.ToString();

            if (transform.parent.tag == "Reference")
            {
                topColDict.Remove(currentNoteName);
            }


            if (transform.parent.tag == "Attempt")
            {
                topColDict.Remove(currentNoteName);
                Check();
              //  Debug.Log("exit check");
            }
            
            //remove from list of notes playing
            list.Remove(col.gameObject);

           

        }
        
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("noteCube") && plinthCOmplete == false || col.gameObject.CompareTag("noteCubeRef") && plinthCOmplete == false)
        {

            
            tempNoteCube t = col.gameObject.GetComponent<tempNoteCube>();
            t.ShakeMe();
            if (t.mycubeStates.currentHoldState == cubeStates.Holdstates.None)
            {
                t.ChangeHeldState(2);
            }
        }

       

    }

    void Check()
    {

   if(topColDict.Count==0)
        {
           // Debug.Log("Checking Empty - Returning");
                return;

        }
        namesakeDict = namesake.GetComponentInChildren<topColliderTrigger>().topColDict;
     
        if (namesakeDict.Count == topColDict.Count) // Require equal count.
        {
            
            dictsMatch = true;
            foreach (var pair in namesakeDict)
            {
                string value;
                if (topColDict.TryGetValue(pair.Key, out value))
                {
                    // Require value be equal.
                    if (value != pair.Value)
                    {
                        dictsMatch = false;
                     //   Debug.Log("Value not Equal");
                        break;
                    }
                }
                else
                {
                    // Require key be present.
                    dictsMatch = false;
                  //  Debug.Log("Key not Present");
                    break;
                }
            }
            
           // Debug.Log("Value Equal, Key Present");
        }

      if(dictsMatch)
        {
            if (plinthCOmplete == false)
            {
                //GetComponent<CapsuleCollider>().enabled = false;
               
                if (canLoop == true)
                {
                   
                    plinthButton.ButtonSelected();
                }
                LightOn(true);
                // myBox.enabled = false;
                playerHand.DropCube();

                checkWinScript.NumberCorrect(1);

                foreach (GameObject g in list)
                {
                    //change enum state and make them none player interactable 
                    tempNoteCube t = g.gameObject.GetComponent<tempNoteCube>();
                    g.layer = 0;


                    if (t.mycubeStates.currentHoldState == cubeStates.Holdstates.None)
                    {
                        t.ChangeHeldState(2);
                    }
                    Rigidbody r = g.GetComponent<Rigidbody>();
                    r.useGravity = false;
                }

                plinthCOmplete = true;
                StartCoroutine(PlinthLocker());

            }

        }
        else
        {
            LightOn(false);
            

        }
        dictsMatch = false;

    }

    IEnumerator PlinthLocker()
    {
        yield return new WaitForSeconds(0.5f);

        plinthLocked = true;
        

    }

   public void Loop()
    {

            foreach (GameObject g in list)
            {
                g.GetComponent<audioController>().PlaySingleNoteNow(chordSpreadLimit);
            }
      
    }



    public void ButtonPressed()
    {
        
        if(canLoop==true)
        {
            canLoop = false;
            InvokeRepeating("Loop", 0, loopTime);
        }
        else if (canLoop == false)
        {
            if (IsInvoking("Loop")) CancelInvoke("Loop");
            canLoop = true;
        }
       // canLoop = !canLoop;
   
    }





    IEnumerator Looper(float f)
    {
      
            yield return new WaitForSeconds(f);
        canLoop = true;
        Loop();
        

    }

    void LightOn(bool light)
    {
        Light l = GetComponentInChildren<Light>();
        if (light)
        {
            
            l.intensity = lightIntensity;
        }
        else
        {
            l.intensity = 0;
        }
    }

  
}
