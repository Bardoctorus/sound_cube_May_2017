using UnityEngine;
using System.Collections;

public class playerRh1 : MonoBehaviour
{


   
    [Header("Holding Things")]
    public GameObject heldObject;
    public Rigidbody heldObjectRb;
    public LayerMask layerMask = -1;
    public bool holdingNow = false;
    public float grabDistance = 500f;

    float summonSpeed;
    tempNoteCube heldCubeScript;

    public float throwspeed = 40f;
    public ForceMode forcemode;

    //spring stuff
    [Header("Spring Things")]

    GameObject rhref;

    //raycast stuff for button pressing
    [Header("Raycast for button pressing things")]
    private Ray ray;
    private RaycastHit hit;
    public float buttonDistance = 5f;

    //new variables for RHray method
    bool clickedOnCube = false;
   
    //lerp vars
    public float lerpTimeLength;
    bool isLerping = false;
    Vector3 startPos;
    //Vector3 endPos;
    float timeStartedLerping;
    

    void Start()
    {

        rhref = GameObject.Find("rhref");



    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonRay();
        }


        if (Input.GetMouseButtonDown(1))
        {
            // RightMouseButtonRay();
            if (clickedOnCube == false)
            {
                RHray();
            }
            else
            {
              
                isLerping = false;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();
                heldObjectRb.isKinematic = false;
                heldObjectRb.useGravity = true;
                if (heldObject.tag == ("noteCubeRef"))
                {
                    heldObject.GetComponent<tempNoteCube>().GoToMyPlinth();
                }

                heldCubeScript.ChangeHeldState(0);

                heldObject = null;               
                clickedOnCube = false;
                

            }
        }

        if (Input.GetKeyDown(KeyCode.E))  
        {
            if (heldObject != null)
            {
                heldObject.GetComponent<AudioSource>().Play();
                heldObject.GetComponentInChildren<testLightLerp>().Flash();
            }

           
        }

    
       
    
            if (isLerping == true)
            {
              //  Debug.Log("is lerping");
                float timeSinceStarted = Time.time - timeStartedLerping;
                float percentageComplete = timeSinceStarted / lerpTimeLength;
                heldObject.transform.position = Vector3.Lerp(startPos, rhref.transform.position, percentageComplete);
                heldObject.transform.Rotate(Mathf.PingPong(Time.time * 4, 9f), Mathf.PingPong(Time.time * 5, 7f), Mathf.PingPong(Time.time * 6, 8f));
            }


        
    }



   

    public void DropCube()
    {
        if (heldObject != null)
        {
            isLerping = false;
            heldObjectRb = heldObject.GetComponent<Rigidbody>();
            heldObjectRb.isKinematic = false;
            heldObjectRb.useGravity = true;
            if (heldObject.tag == ("noteCubeRef"))
            {
                heldObject.GetComponent<tempNoteCube>().GoToMyPlinth();
            }

            heldCubeScript.ChangeHeldState(0);
            heldObject = null;
            clickedOnCube = false;
        }

    }
 

    public void RHray()
    {
        if (heldObject != null)
        {
            DropCube();
        }
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        if (Physics.Raycast(ray, out hit, grabDistance, layerMask))
        {
            if (hit.collider.gameObject.tag == ("noteCubeRef") || hit.collider.gameObject.tag == ("noteCube"))
            {
                heldObject = hit.collider.gameObject;
                Rigidbody r = heldObject.GetComponent<Rigidbody>();
                heldCubeScript = heldObject.GetComponent<tempNoteCube>();

                heldCubeScript.ChangeHeldState(1);
                clickedOnCube = true;
                r.isKinematic = true;
                r.useGravity = false;
                BeginLerp(heldObject);
            }
        }
    }
    void BeginLerp(GameObject held)
    {
       // Debug.Log("BeginLerpCalled");
        isLerping = true;
        timeStartedLerping = Time.time;
        startPos = held.transform.position +new Vector3(.001f,.001f,.001f);

    }


 


    void LeftMouseButtonRay()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, 
            Screen.height / 2, 0f));
        if (Physics.Raycast(ray, out hit, buttonDistance) 
            && hit.collider.gameObject.CompareTag("worldButton"))
        {

           GameObject g = hit.collider.gameObject;
            g.GetComponent<PlinthNewButton>().ButtonSelected();

           
        }
        if (Physics.Raycast(ray, out hit, buttonDistance) 
            && hit.collider.gameObject.CompareTag("noteCube")
            || Physics.Raycast(ray, out hit, buttonDistance) 
            && hit.collider.gameObject.CompareTag("noteCubeRef"))
        {
            hit.collider.gameObject.GetComponent<AudioSource>().Play();
            hit.collider.gameObject.GetComponentInChildren<testLightLerp>().Flash();
        }

    }


}
