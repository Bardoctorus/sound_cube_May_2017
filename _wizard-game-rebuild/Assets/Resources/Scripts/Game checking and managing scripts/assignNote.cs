using UnityEngine;
using System.Collections;

public class assignNote : MonoBehaviour {

    string[] noteFilePaths;
    int[] cFifths;
    TestLevelGen testlev;
    int previousRandomnote;

    void Awake()
    {
        testlev = GetComponent<TestLevelGen>();
        noteFilePaths = new string[]
      {
            "Sounds/ol/ol ol-",
            "Sounds/ps/ps ps-",
            "Sounds/bp/bp bp-"
      };

        //this array is the note numbers of C and G. From this we can get All the notes we need - +2 for 2nds and 6ths of C - or +5 for F and C      
        cFifths = new int[]
        {
                0,7,12,19,24,31,36,43,48
        };
      
    }
	
    public void noteAssign(GameObject refCube, GameObject atCube)
    {
        int randomNote = cFifths[Random.Range(0, 9)];
        //randomNote = ExtensionMethods.RandomPercentage(randomNote, 30);
        if (randomNote == previousRandomnote || randomNote > 48)
        {
            randomNote = cFifths[Random.Range(0, 9)];

            if (randomNote > 24)
            {
                randomNote = ExtensionMethods.RandomPercentage(randomNote, 30);
            }
        }
        if (testlev.endLevelTune == TestLevelGen.EndLevelTune.sillyfloop135)
        {
            randomNote += 5;
            if(randomNote>48)
            {
                randomNote = 48;
            }
        }
        //stops a plinth having the same note on it... ONLY WORKS FOR PLINTHS OF 2 SO FAR!!!!!!!!!!!!!!!!
        previousRandomnote = randomNote;
      
        int r = Random.Range(0, 3);
        string s = noteFilePaths[r];

 
               refCube.GetComponent<AudioSource>().clip = Resources.Load(s+ randomNote) as AudioClip;
   

        atCube.GetComponent<AudioSource>().clip = Resources.Load(s+ randomNote) as AudioClip;


    }
}
