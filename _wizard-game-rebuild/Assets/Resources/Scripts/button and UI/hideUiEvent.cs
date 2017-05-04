using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class hideUiEvent : MonoBehaviour {

    [System.Serializable]
    public class HideUiOnKeyPress : UnityEvent<bool>
    
    {

    }

    public HideUiOnKeyPress hideui;
  
	void Update () {
	if (Input.GetKeyDown(KeyCode.F))
        {
            //hideui calls the ui behavior script
            hideui.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            //hideui calls the ui behavior script

            hideui.Invoke(false);
        }
    }
}
