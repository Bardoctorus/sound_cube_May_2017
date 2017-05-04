using UnityEngine;
using System.Collections;

public class moreCubesPlease : MonoBehaviour {
    public GameObject cube;
public void MoreCubes()
    {
        int n = 10;
        while (n > 0)
        {
            Instantiate(cube, new Vector3(Random.Range(-486, -513), 5, Random.Range(-13, 13)), Quaternion.identity);
            n--;
        }
    }
}
