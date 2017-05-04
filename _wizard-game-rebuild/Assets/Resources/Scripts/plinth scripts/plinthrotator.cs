using UnityEngine;
using System.Collections;

public class plinthrotator : MonoBehaviour {

    public float xspeed = 5f;
    public float yspeed = 5f;
    public float zspeed = 5f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(xspeed,yspeed,zspeed,Space.Self);

    }
}
