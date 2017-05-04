using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class volumeMaster : MonoBehaviour {

    public AudioMixer masterM;
    
    public void Lev(float lev)
    {
        masterM.SetFloat("lev", lev);
    }


}
