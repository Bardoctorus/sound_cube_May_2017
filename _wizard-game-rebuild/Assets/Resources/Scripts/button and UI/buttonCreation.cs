using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonCreation : MonoBehaviour {

    public GameObject buttonPrefab;
    public int numberOfNotes = 49;

	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MakeButtonsNow(string me)
    {
        if(transform.childCount !=0)
        {
           foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
       // Debug.Log(me);
        int i = 0;
        while(i<numberOfNotes)
        {
           GameObject bu = Instantiate(buttonPrefab)as GameObject;
            bu.transform.SetParent(gameObject.transform, false);
            bu.gameObject.name = me + i;

            Button b = bu.GetComponent<Button>();
            b.GetComponentInChildren<Text>().text = me + i;

            i++;
           

        }
    }
}
