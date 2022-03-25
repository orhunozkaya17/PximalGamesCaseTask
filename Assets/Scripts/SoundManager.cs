using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] GameObject soundPrefab;
    [Header("Sound Settings")]
    [Range(0, 1)] 
    [SerializeField] float soundVolume = 1f;
    [Range(0, 1)]
    [SerializeField] float musicVolume = 1f;
    [Header("Music Loop Game")]
    [SerializeField] AudioClip musicLoopClip;
    AudioSource musicSource;
    [Header("Stack Sound")]
    [SerializeField] AudioClip stackCollectClip;
    [Header("Box Break")]
    [SerializeField] AudioClip BreakBoxClip;
    [Header("Box Pauch")]
    [SerializeField] AudioClip[] BoxPauchClips;
    [Header("Box In Truck")]
    [SerializeField] AudioClip boxInTruckClip;
    [Header("Truck loop")]
    [SerializeField] AudioClip TruckLoopClip; 
    [Header("Truck Door")]
    [SerializeField] AudioClip TruckDoorClip;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        musicSource = GetComponent<AudioSource>();
        musicSource.loop = true;
        SetMusicLoop();
       
    }

    private void SetMusicLoop()
    {
        musicSource.volume = musicVolume;
        musicSource.clip = musicLoopClip;
    }

    public void PlayStackCollect(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(stackCollectClip, soundVolume);
    }
    public void PlayBoxBreak(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(BreakBoxClip, soundVolume,false);
    }
    public void PlayBoxPauch(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(BoxPauchClips[Random.Range(0, BoxPauchClips.Length)], soundVolume);
    }
    public void PlayBoxInTruck(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(boxInTruckClip, soundVolume);
    }
    public void PlayTruckSound(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(TruckLoopClip, 0.1f, false,true);
    }
    public void PlayTruckDoor(Vector3 pos)
    {
        GameObject sound = PoolManager.instance.ReuseObject(soundPrefab, pos, Quaternion.identity);
        sound.SetActive(true);
        sound.GetComponent<SoundPrefab>().Init(TruckDoorClip, soundVolume);
    }
}
