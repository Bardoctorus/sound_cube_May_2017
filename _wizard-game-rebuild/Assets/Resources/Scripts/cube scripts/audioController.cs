using UnityEngine;
using System.Collections;

public class audioController : MonoBehaviour
{

    AudioSource myAudioSource;
    testLightLerp lerp;

	
	void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();
        lerp = GetComponentInChildren<testLightLerp>();
	}
	
	

    public void PlaySingleNoteNow(float spreadchordValue)
    {

        StartCoroutine(PlayNowRoutine(spreadchordValue));

    }

    IEnumerator PlayNowRoutine(float spreadAmount)
    {
        yield return new WaitForSeconds(Random.Range(0.0f, spreadAmount));
        myAudioSource.Play();
        lerp.Flash();

        yield return null;

    }
}
