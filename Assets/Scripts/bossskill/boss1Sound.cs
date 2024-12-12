using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class boss1Sound : MonoBehaviour
{
    public AudioSource Spike_SFX;
    public AudioSource Boast_SFX;
    public AudioSource Dead_SFX;
    public AudioSource Fire_SFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlaySpikeSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spike_SFX.Play();
    }

    IEnumerator PlayFireSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire_SFX.Play();
    }

    IEnumerator PlayBoastSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Boast_SFX.Play();
    }


    IEnumerator PlayBossDeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Dead_SFX.Play();
    }
}
