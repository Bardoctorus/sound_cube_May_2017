using UnityEngine;
using System.Collections;

public class destroyButtonsOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyChildren()
    {
        //GameObject[] gs = GameObject.FindGameObjectsWithTag("Notebuttons");

        //foreach (GameObject g in gs)
        //{
        //    Destroy(g);
        //}

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
       
}
