using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlinthEventManager : MonoBehaviour {
    public bool OneShot;


    public UnityEvent triggerNow;

    void Awake()
    {
        if(triggerNow== null)
        {
            triggerNow = new UnityEvent();
        }
    }

    void Update()
    {
        if(OneShot==true)
        {
            triggerNow.Invoke();
            OneShot = false;
       
        }
    }



    


}
