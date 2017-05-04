using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{

    public static float NumberPerPlinthsMinus1(float plinths, float cubes)
    {
        float cubesDivPlinths = cubes / plinths;
      
        float numberForAllButLastPlinth = Mathf.Round(cubesDivPlinths);
    

        return numberForAllButLastPlinth;
    }

    public static float RemainderForLastPlinth(float plinths, float cubes, float numberForAllButLastPlinth)
    {
        float remainderForLastPlinth = cubes - ((plinths - 1) * numberForAllButLastPlinth);
        if(remainderForLastPlinth<0)
        {
            remainderForLastPlinth = 0.0f;
        }
        return remainderForLastPlinth;
    }

    public static float RoundWithRandom(float plinths, float cubes)
    {
        float roundWithRandom = Mathf.Round(cubes / plinths);
        float result = Random.Range(0, 100);
        if(result<33f)
        {
            roundWithRandom += 1;
        }
        else if(result>33f)
        {
            roundWithRandom -= 1;
        }

        return roundWithRandom;
    }

    public static float RandomPlinthSpeed(float speed)
    {
        float result = Random.Range(0, 100);
        if(result<50)
        {
            speed = Random.Range(-2f, -.2f);
        }
        else
        {
            speed = Random.Range(.2f, 2f);
        }

    return speed;
    }


    public static int RandomPercentage (int randomNote, int likelyhood)
    {
        float result = Random.Range(0, 100);

        if(result<likelyhood)
        {
            randomNote += 2;
        }



        return randomNote;
    }

    public static Vector3 SpawnSpace(Vector3 quadSize)
    {
        Vector3 spawnAreaLimit;

        spawnAreaLimit = new Vector3((quadSize.x * 15) - 1, 0, (quadSize.z * 15) - 1);
        return spawnAreaLimit;


    }

}
