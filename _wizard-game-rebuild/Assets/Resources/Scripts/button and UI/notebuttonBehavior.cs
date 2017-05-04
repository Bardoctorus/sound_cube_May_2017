using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System.Collections;
using System;

public class notebuttonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public class LoadSampleEvent : UnityEvent<string> { };

    public LoadSampleEvent sampleLoad;

    


   public string me;
    public tempNoteCube g;

    void Awake()
    {
       
       
    }

    void Start()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("noteCube");

        foreach (GameObject g in gs)
        {
            tempNoteCube t = g.GetComponent<tempNoteCube>();
            if (sampleLoad == null)
            {
                sampleLoad = new LoadSampleEvent();
            }
            sampleLoad.AddListener(t.AssignNote);
        }


        me = gameObject.name;

    }

    public void SnedStringToCube()

    {
        if (me != null)
        {
            sampleLoad.Invoke(me);
        }

        destroyButtonsOnClick d = GetComponentInParent<destroyButtonsOnClick>();
        d.DestroyChildren();
      
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        string pathName = me.Substring(0, 2);

        string fullPath = "Sounds/" + pathName + "/" + me;

       // Debug.Log(fullPath);
        AudioSource myAudioSource = gameObject.GetComponent<AudioSource>();
        if (myAudioSource == null)
        {
            Debug.Log("can't find the audiosource");
        }

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

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
      
        AudioSource myAudioSource = gameObject.GetComponent<AudioSource>();
        if (myAudioSource == null)
        {
            Debug.Log("can't find the audiosource");
        }

        //if (myAudioSource.clip != null)
        //{
        //    myAudioSource.clip = null;
        //}
     
    }
}  
