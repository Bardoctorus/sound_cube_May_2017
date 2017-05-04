using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loopspeedreadout : MonoBehaviour {
    float currentValue;
    GameObject slider;
	// Use this for initialization
	void Start ()
    {
        slider = GameObject.Find("LoopSpeedSlider");
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentValue = slider.GetComponent<Slider>().value;
     
        string stringValue = currentValue.ToString("n1");
        var tex = gameObject.GetComponent<Text>();
        tex.text ="Loop every "+ stringValue+" seconds";
	}


}
