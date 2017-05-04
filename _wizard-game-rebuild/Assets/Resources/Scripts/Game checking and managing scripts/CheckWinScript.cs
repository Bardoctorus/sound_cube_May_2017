using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckWinScript : MonoBehaviour {
    int currentCorrect = 0;
    [SerializeField]
    float numberToWin;
    TestLevelGen testLevGen;
    topColliderTrigger[] topcolarray;


    void Start()
    {
        testLevGen = GetComponent < TestLevelGen > ();
       
        numberToWin = testLevGen.howManyPlinthPairs;

        StartCoroutine(GetAllTopCols());
    }
    IEnumerator GetAllTopCols()
    {
        yield return new WaitForSeconds(0.2f);
         topcolarray= FindObjectsOfType<topColliderTrigger>();
    }


    public void NumberCorrect(int i)
    {
        currentCorrect = currentCorrect + i;
        //Debug.Log("currently correct = "+currentCorrect);
        CheckWin();
    }


    public void RemoveEmptyPlinthsFromWinCondition()
    {
        numberToWin -= 1;
        //  Debug.Log("number to win = "+numberToWin);
    }
    void CheckWin()
    {
        if (currentCorrect >= numberToWin)
        {
            AudioSource a = GetComponent<AudioSource>();
            a.Play();

            foreach (topColliderTrigger tc in topcolarray)
            {
                tc.ButtonPressed();
                tc.ButtonPressed();
            }
         
        }
    }

  
}
