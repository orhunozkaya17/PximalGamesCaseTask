using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPrefab : MonoBehaviour
{
    [SerializeField] AudioSource source;

    public void Init(AudioClip clip,float volume,bool ispitch=true,bool loop=false)
    {
        if (loop)
        {
            source.loop = loop;
        }
        else
        {
            StopCoroutine(Disable(clip.length));
            StartCoroutine(Disable(clip.length));
        }
        if (ispitch)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
        }
        else
        {
            source.pitch = 1;
        }
        source.volume = volume;
        
        source.clip = clip;
        source.Play();
     
    }

    IEnumerator Disable(float length)
    {
        yield return new WaitForSeconds(length);
        gameObject.SetActive(false);
    }
}
