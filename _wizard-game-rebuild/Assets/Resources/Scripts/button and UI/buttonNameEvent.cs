using UnityEngine;
using System.Collections;

public class buttonNameEvent : MonoBehaviour {
    public string me;
    public GameObject containerNotebuttons;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void NameToParent()
    {
        GameObject cnb = containerNotebuttons;

      
       
       me = gameObject.name;
            switch (me)
            {
                case "1 - Organ":
                    me = "ol ol-";
                    cnb.GetComponent<buttonCreation>().MakeButtonsNow(me);
                    break;

                case "2 - Pizzicato":
                    me = "ps ps-";
                cnb.GetComponent<buttonCreation>().MakeButtonsNow(me);
                    break;

                default:
                    me = "ol ol-";
                cnb.GetComponent<buttonCreation>().MakeButtonsNow(me);
                    break;
            }

        }


}
