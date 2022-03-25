using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBox : MonoBehaviour
{
    [SerializeField] float time = 2f;
    List<Vector3> postion = new List<Vector3>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            postion.Add(child.localPosition);
        }
    }
    private void OnEnable()
    {
       
        int childd = 0;   
        foreach (Transform child in transform)
        {
            child.localPosition = postion[childd];
            childd++;
        }
        StopCoroutine(Disable());
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(time);
        transform.gameObject.SetActive(false);
    }
}
