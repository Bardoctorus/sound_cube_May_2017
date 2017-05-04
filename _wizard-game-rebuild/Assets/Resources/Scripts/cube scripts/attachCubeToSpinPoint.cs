using UnityEngine;
using System.Collections;

public class attachCubeToSpinPoint : MonoBehaviour {

    //Relavant things for the point to attach to
   


    //lerp things
    bool lerpToPoint = false;
    float timeStartedLerping;
    float followSpeed = .1f;

    bool inTrigger = false;
    bool madeSpinPoint = false;

    Vector3 startPos;

    public GameObject spinner;
     Vector3 spinPoint;
    Transform plinth;
    public cubeStates mycubeStates { get; private set; }
    Rigidbody r;
    BoxCollider b;
    GameObject g;
    topColliderTrigger topcol;
    

    // Use this for initialization
    void Start () {
        mycubeStates = GetComponent<cubeStates>();
        r = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if (lerpToPoint == true && mycubeStates.currentHoldState == cubeStates.Holdstates.PlinthHeld)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / followSpeed;
            //if (gameObject.tag == "notecube")
            //{
            //    Debug.Log(spinPoint);
            //}
            transform.position = Vector3.Lerp(startPos, spinPoint, percentageComplete);         
        }

        if (inTrigger)
        {
            if(madeSpinPoint==true)
            {
                spinPoint = b.transform.position;
            }
            if (madeSpinPoint == false &&topcol.plinthLocked==false)
            {
                madeSpinPoint = true;
                MakePointAndAttach();
            }

        }

        

       
    }

    void OnTriggerStay(Collider col)
    {
        if (mycubeStates.currentHoldState == cubeStates.Holdstates.PlinthHeld&&col.gameObject.tag=="playArea")
        {
            plinth = col.gameObject.GetComponentInParent<Transform>();
            topcol = col.gameObject.GetComponent<topColliderTrigger>();
            inTrigger = true;
            
           
        }

    }    

 
    void MakePointAndAttach()
    {
        Vector3 center = plinth.position + new Vector3(0, 0.15f, 0);
         g = Instantiate(spinner, center, Quaternion.Euler(Random.Range(1, 350), 0, 0))as GameObject;
        float speed = ExtensionMethods.RandomPlinthSpeed(1f);
       
        g.GetComponent<plinthrotator>().yspeed = speed;
         b = g.GetComponentInChildren<BoxCollider>();
        spinPoint = b.transform.position;
        startPos = transform.position;
       timeStartedLerping = Time.time;
       // r.isKinematic = true;
        r.useGravity = false;
        lerpToPoint = true;

    

    }
   
       

    public void Disconnect()
    {
        inTrigger = false;
        lerpToPoint = false;
        Destroy(g);
        madeSpinPoint = false;
    }
       
}
