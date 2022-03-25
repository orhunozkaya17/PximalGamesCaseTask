using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] Transform Visual;
    [SerializeField] ParticleSystem BoxTrace;
    HingeJoint joint;
    StackController stackController;
    BoxCollider boxCollider;
    Rigidbody rb;
    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        BoxTrace.Stop();
    }

    public void Init(StackController stackController, Rigidbody target, bool firtBox = false, Vector3 firstboxConnectedPos = new Vector3())
    {
        if (firtBox)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            joint.connectedBody = target;
            joint.connectedAnchor = firstboxConnectedPos;
        }
        else
        {

            transform.rotation = target.rotation;
            transform.position = target.position + target.transform.up * 1;
            joint.connectedBody = target;
            joint.connectedAnchor = new Vector3(0, 0.6f, 0);
        }
        this.stackController = stackController;
    }

    public void SetFinalScene()
    {
        Destroy(joint);
        Destroy(rb);
    }
    public void DeConnected()
    {
        Destroy(joint);
        boxCollider.isTrigger = false;
        rb.useGravity = true;

    }
    public Transform getVisual()
    {
        return Visual;
    }
    public void setPartical(bool on)
    {
        if (on)
        {
            BoxTrace.Play();
        }
        else
        {
            BoxTrace.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SoundManager.instance.PlayBoxBreak(transform.position);
            ParticalEffectManager.instance.BrokenEffect(transform.position);
            ParticalEffectManager.instance.HitEffect(transform.position);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SoundManager.instance.PlayBoxBreak(transform.position);
            ParticalEffectManager.instance.BrokenEffect(transform.position);
            ParticalEffectManager.instance.HitEffect(transform.position);
            stackController.removeStack(this);
            Destroy(this.gameObject);
        }
    }
}

    
  
