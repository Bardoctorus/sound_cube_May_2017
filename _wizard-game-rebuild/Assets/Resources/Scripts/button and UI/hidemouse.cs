using UnityEngine;
//using UnityEngine.Events;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class hidemouse : MonoBehaviour {


   // public UnityEvent hideUI;
   // bool eventFired = false;

    GameObject player;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    eventFired = true;
        //    hideUI.Invoke();
        //}

        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    eventFired = false;
        //    hideUI.Invoke();
        //}

        if (Input.GetKey(KeyCode.F))
        {
           player.GetComponent<FirstPersonController>().enabled = false;
            UnityEngine.Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;           
        }

        else
        {
            player.GetComponent<FirstPersonController>().enabled = true;

            UnityEngine.Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


            if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

  

}
