using UnityEngine;
using System.Collections;

public class emissionPulse : MonoBehaviour
{
    public Renderer rend;
    float baseEmisAmount;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();

    }
}

