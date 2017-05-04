using UnityEngine;
using System.Collections;

public class texoffset : MonoBehaviour {
    public float invokeNum=.5f;
    float offsetNum = 0;
   public float offsetSpeed = 0.2f;
	// Use this for initialization
	void Start () {
        InvokeRepeating("IncNum", invokeNum,invokeNum);
	}
	
	// Update is called once per frame
	void IncNum()
    {
        if(offsetNum<1)
        {
            offsetNum += offsetSpeed;
            
        }
       
    }

    void Update()
    {
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0,offsetNum));
        if (offsetNum >= 1)
        {
            offsetNum = 0;
        }
    }
	
}
