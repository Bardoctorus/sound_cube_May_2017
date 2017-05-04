using UnityEngine;
using System.Collections;

public class testLightLerp : MonoBehaviour {

    Light l;
    float number;
    bool flash;
	// Use this for initialization
	void Start () {
        l = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

       

        number += Time.deltaTime * 2;
        
    if(flash)
        {
            l.intensity = Mathf.PingPong(number, 1f);
        }


    }

    public void Flash()
    {
        StopAllCoroutines();
        number = 0;
        StartCoroutine(Flasher(1f));
    }

    IEnumerator Flasher(float t)
    {
        flash = true;
        
        yield return new WaitForSeconds(t);
        flash = false;
        l.intensity = 0;
    }    

}
