using UnityEngine;
using System.Collections;

public class Uibehavior : MonoBehaviour {
    public GameObject canvas;
	// Use this for initialization
	void Start ()
    {
        
        GameObject b = Instantiate(canvas, transform.position, Quaternion.identity) as GameObject;
        b.transform.SetParent(gameObject.transform, false);
        b.GetComponent<Canvas>().enabled = false;
	}
	


  public void EnabledState(bool isIt)

    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("UI");

        foreach (GameObject g in gs)
        {
            g.GetComponent<Canvas>().enabled = isIt;
        }
        GameObject[] bs = GameObject.FindGameObjectsWithTag("Notebuttons");

        foreach (GameObject b in bs)
        {
            Destroy(b);
        }
    }
}
