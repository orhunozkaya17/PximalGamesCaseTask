using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour

{

    public static PoolManager instance;
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();
    [SerializeField] private Pool[] pool=null;
    [SerializeField] private Transform objectPoolTransform=null;

    [System.Serializable]
    public struct Pool
    {
        public int poolsize;
        public GameObject prefab;
    }
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < pool.Length; i++)
        {
            createPool(pool[i].prefab, pool[i].poolsize);
        }
    }
    
    private void Start()
    {
       
    }

    private void createPool(GameObject prefab, int poolsize)
    {
        int poolKey = prefab.GetInstanceID();
        string prefabname = prefab.name;
        GameObject parentGameObject = new GameObject(prefabname+"Anchor");
        parentGameObject.transform.SetParent(objectPoolTransform);
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey,new Queue<GameObject>());
            for (int i = 0; i < poolsize; i++)
            {
                GameObject newObject = Instantiate(prefab,parentGameObject.transform)as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }

        }
    }
    public GameObject ReuseObject(GameObject prefab,Vector3 postion,Quaternion rotation)
    {
        int poolkey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolkey))
        {
            GameObject objectToreuse = getObjectFromPool(poolkey);
            ResetObject(postion,rotation,objectToreuse,prefab);
            return objectToreuse;
        }
        else
        {
            return null;
        }
    }

    private void ResetObject(Vector3 postion, Quaternion rotation, GameObject objectToreuse,GameObject prefab)
    {
        objectToreuse.transform.position = postion;
        objectToreuse.transform.rotation = rotation;
        objectToreuse.transform.localScale = prefab.transform.localScale;
    }

    private GameObject getObjectFromPool(int poolkey)
    {
        GameObject objecToReUse = poolDictionary[poolkey].Dequeue();
        poolDictionary[poolkey].Enqueue(objecToReUse);
        if (objecToReUse.activeSelf==true)
        {
            objecToReUse.SetActive(false);
        }
        return objecToReUse;
    }
}
