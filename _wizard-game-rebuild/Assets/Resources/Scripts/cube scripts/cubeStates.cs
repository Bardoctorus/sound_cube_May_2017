using UnityEngine;
using System.Collections;

public class cubeStates : MonoBehaviour {

    public enum Holdstates
    {
        None, // 0
        PlayerHeld, // 1
        PlinthHeld // 2         if you are every dumb enough to need that annotation, enjoy licking your keyboard.
    }

    public Holdstates currentHoldState;

	
}
