using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;
using System;

public class tempNoteCube : MonoBehaviour {

 
    //getting things for switching the new Held State enum
    public cubeStates mycubeStates { get; private set; }


    bool lerpBackToPlinth = false;
    Vector3 myPlinthVector3;

    AudioSource myAudioSource;

    float timeStartedLerping;
    float lerpbacktoplinthtime = 0.5f;
    Vector3 startPos;

    //bool for shaking
    bool isShaking = false;

    //trail rend

    TrailRenderer trend;

    public MeshRenderer rend;


    void Start()
    {
        //getting the cubstates script for the enum
        mycubeStates = GetComponent<cubeStates>();
        mycubeStates.currentHoldState = cubeStates.Holdstates.None;


       myAudioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(ConstrainAtStart());

     

        trend = GetComponent<TrailRenderer>();
        
    }

    public void BeginLerpBackToPlinth()
    {
    
        startPos = transform.position;
       // Debug.Log("BeginLerpCalled");
        lerpBackToPlinth = true;
        timeStartedLerping = Time.time;

    }

    void FixedUpdate()
    {
        if(lerpBackToPlinth==true)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / lerpbacktoplinthtime;
            transform.position = Vector3.Lerp(startPos, myPlinthVector3, percentageComplete);
            if(percentageComplete>=1)
            {
                lerpBackToPlinth = false;
            }
           
        }
    }

    void Update()
    {
       if(mycubeStates.currentHoldState== cubeStates.Holdstates.PlinthHeld)
        {
            StartCoroutine(StartTrail());
        }
        else
        {
            trend.enabled = false;
        }
    }
    IEnumerator StartTrail()
    {
        yield return new WaitForSeconds(1f);
        trend.enabled = true;
    }

    public void MyPlinthLocation()
    {
        myPlinthVector3 = transform.position;
       
    }
    public void GoToMyPlinth()
    {
        BeginLerpBackToPlinth();
    }




    public void ChangeHeldState(int i)
    {

        mycubeStates.currentHoldState = (cubeStates.Holdstates)i;
        
    }



    public void AssignNote(string name)
    {

        if (mycubeStates.currentHoldState == cubeStates.Holdstates.PlayerHeld)
        {
            //create the path name
            string pathName = name.Substring(0, 2);

            string fullPath = "Sounds/" + pathName + "/" + name;

           // Debug.Log(fullPath);


            //get the audiosource, and throw an error if it's not there
           
            if (myAudioSource == null)
            {
                Debug.LogError("can't find the audiosource");
            }

            //make sure there isn't a clip there already (probably not needed but hey) and get it to load the path you just built above
            if (myAudioSource.clip != null)
            {
                myAudioSource.clip = null;
            }
            if (myAudioSource.clip == null)
            {
                myAudioSource.clip = Resources.Load(fullPath, typeof(AudioClip)) as AudioClip;
                myAudioSource.Play();
               
            }
        }

    }

 
    //testing a shaking when the cube is over the plinth. Looks shit, needs redoing
    public void ShakeMe()
    {
        if (mycubeStates.currentHoldState== cubeStates.Holdstates.PlayerHeld && isShaking==false)
        {
            isShaking = true;
            StartCoroutine(Shaketime());
           // Debug.Log("Shake");
        }

    }

    IEnumerator Shaketime()
    {
        transform.Translate(.02f, .02f, .02f);
        yield return new WaitForSeconds(.01f);
        transform.Translate(-.02f, -.02f, -.02f);
        yield return new WaitForSeconds(.01f);
        isShaking = false;
    }

 

    IEnumerator ConstrainAtStart()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(5f);

        r.constraints = RigidbodyConstraints.None;
    }




        
}
