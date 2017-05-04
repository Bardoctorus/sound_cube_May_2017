using UnityEngine;
using System.Collections;

public class PlinthNewButton : MonoBehaviour {
    topColliderTrigger myTopcoltrigger;

    Animator anim;
    int onHash = Animator.StringToHash("TurnOn");
    int offHash = Animator.StringToHash("TurnOff");
    bool turningOn;

    Renderer rend;
    Material onMat;
    Material offMat;
    Light l;

    AudioSource aud;
    AudioClip[] clips;
    void Awake()
    {
        aud = GetComponent<AudioSource>();
        clips = new AudioClip[2];
        clips[0] = Resources.Load("sounds/plinthButtons/on") as AudioClip;
        clips[1] = Resources.Load("sounds/plinthButtons/off") as AudioClip;
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        myTopcoltrigger = GetComponentInParent<topColliderTrigger>();
        onMat = Resources.Load("Materials/PlinthButton/buttonOn") as Material;
        offMat = Resources.Load("Materials/PlinthButton/buttonOff") as Material;
        l = GetComponentInChildren<Light>() as Light;
        

    }
        
    void Start ()
    {
        



    }

    public void ChangeMat(int i)
    {
        if (i==1)
        {
            rend.material = onMat;
        }
        else if (i==0)
        {
            rend.material = offMat;
        }
    }

    public void ButtonSelected()
    {
        myTopcoltrigger.ButtonPressed();

        turningOn = !turningOn;
        if(turningOn)
        {
            anim.SetTrigger(onHash);
            aud.clip = clips[0];
            aud.Play();
           
        }
        else if(!turningOn)
        {
            anim.SetTrigger(offHash);
            aud.clip = clips[1];
            aud.Play();
          
        }

       
      
    }

    public void Light(int i)
    {
        if (i == 1)
        {
            l.enabled = true;
        }
        else l.enabled = false;

    }

  

   
}
