using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public GameObject Guy;
    public Animator AnimatorGuy;

    void Start()
    { 
        GetRagdollBits();
        RagdollMadeOff();

        Punch.ragdoll += RagdollMadeOn;
    }

    private void OnDestroy()
    {
        Punch.ragdoll -= RagdollMadeOn;       
    }

    Collider[] ragDollColliders;
    Rigidbody[] limbsRigidbodies;

    public bool IsGameStared { get; internal set; }

    void GetRagdollBits()
    {
        ragDollColliders = Guy.GetComponentsInChildren<Collider>();
        limbsRigidbodies = Guy.GetComponentsInChildren<Rigidbody>();
    }

    void RagdollMadeOn()
    {
        foreach (Collider collider in ragDollColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rigidbody in limbsRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        BoxCollider.enabled = false;
        AnimatorGuy.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void RagdollMadeOff()
    {
        foreach (Collider collider in ragDollColliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigidbody in limbsRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        BoxCollider.enabled = true;
        AnimatorGuy.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }  
}
