using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Sound : MonoBehaviour
{
    public AudioSource Wave_SFX;
    public AudioSource Buff_SFX;
    public AudioSource Dead2_SFX;
    public AudioSource Fire_SFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlayWaveSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Wave_SFX.Play();
    }

    IEnumerator PlayFireSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire_SFX.Play();
    }

    IEnumerator PlayBuffSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Buff_SFX.Play();
    }


    IEnumerator PlayBoss2DeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Dead2_SFX.Play();
    }
}
