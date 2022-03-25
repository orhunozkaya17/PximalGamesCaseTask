using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalEffectManager : MonoBehaviour
{
    public static ParticalEffectManager instance;
    [Header("Hit Effect")]
    [SerializeField] GameObject hitEffect;

    [SerializeField] GameObject BrokenBox;
    [SerializeField] GameObject puffEffect;
    [Header("Game Over Effect")]
    [SerializeField] GameObject trucksmoke;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
    }
    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
    }
    private void Events_GameOver()
    {
        trucksmoke.SetActive(true);
    }

    public void HitEffect(Vector3 position)
    {
        GameObject hit = PoolManager.instance.ReuseObject(hitEffect, position, Quaternion.identity);
        hit.SetActive(true);
    }
    public void BrokenEffect(Vector3 position)
    {
        GameObject brokenBox = PoolManager.instance.ReuseObject(BrokenBox, position, Quaternion.identity);
        brokenBox.SetActive(false);
        brokenBox.SetActive(true);
    }
    public void pufEffect(Vector3 position)
    {
        GameObject effect = PoolManager.instance.ReuseObject(puffEffect, position, Quaternion.Euler(-90, 0, 0));
        effect.SetActive(true);
    }
}
