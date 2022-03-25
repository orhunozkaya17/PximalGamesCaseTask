using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class StackController : MonoBehaviour
{
    public Stack stackPrefab;
    public Transform stackparent;
    public Vector3 firstboxConnectedPos;
    public Transform boxspos;

    private Rigidbody PalletTruck;
    private List<Stack> stacks = new List<Stack>();
    void Start()
    {
        PalletTruck = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            stacks.RemoveAt(0);
            foreach (Stack stack in stacks)
            {
                stack.transform.DOMove(stack.transform.position - Vector3.up * 0.5f, 1f);

            }

        }
    }
    void AddStack()
    {
        if (stacks.Count == 0)
        {
            Stack stack = Instantiate(stackPrefab, boxspos.position, Quaternion.identity);
            stack.Init(this, PalletTruck, true, firstboxConnectedPos);
            stacks.Add(stack);
            stack.transform.SetParent(stackparent);
        }
        else
        {
            Stack stack = Instantiate(stackPrefab, stacks[stacks.Count - 1].transform.position + Vector3.up, Quaternion.identity);
            stack.transform.SetParent(stackparent);
            stack.Init(this, stacks[stacks.Count - 1].GetComponent<Rigidbody>(), false);
            stacks.Add(stack);
        }
        SoundManager.instance.PlayStackCollect(transform.position);
    }
    void FinalSceneSet()
    {
      
        Vector3 pos = stacks.First().transform.position + Vector3.up * 0.3f;
        foreach (Stack stack in stacks)
        {
            pos += Vector3.up * 0.35f;
            stack.transform.DOMove(pos, 1f);
            stack.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1f);
            stack.GetComponent<Stack>().SetFinalScene();
        }
    }
    public Stack getStack()
    {
   
        Stack stackk = stacks.First();
        Vector3 pos = transform.position;
        stacks.RemoveAt(0);
        for (int i = 0; i < stacks.Count; i++)
        {
            
            stacks[i].transform.DOMove(pos, 0.5f);
            pos.y += 0.35f;

        }
       
        return stackk;
    }
    public bool endStack()
    {
        if (stacks.Count - 1 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void removeStack(Stack gam)
    {
        if (stacks.Contains(gam))
        {
            int a = stacks.FindIndex(x => x == gam);
            for (int i = stacks.Count - 1; i >= a; i--)
            {
                stacks[i].GetComponent<Stack>().DeConnected();
            }
            stacks.RemoveRange(a, stacks.Count - a);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
            AddStack();
            Destroy(other.gameObject);
        }
        else if (other.tag == "FinalScene")
        {
            Destroy(other);
            if (stacks.Count == 0)
            {
              
                Events.CallGameOver();
                return;
            }
            FinalSceneSet();
            Events.CallFinalScene();
        }
    }
}
