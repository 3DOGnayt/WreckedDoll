using System.Collections;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Camera cam;
    public LayerMask LayerMask;

    public float force;
    public bool firstTouch = true;

    public float force2;
    public bool secondTouch = false;

    public delegate void Ragdoll();
    public static event Ragdoll ragdoll;

    public bool gotHit = false;

    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.blue);
        Debug.DrawRay(transform.position, ray.direction * 20f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20f, LayerMask) && firstTouch == true)
        {
            ragdoll();
            if (Physics.Raycast(ray, out hit))
            { 
                hit.collider.gameObject.GetComponent<Rigidbody>()
                    .AddForce(ray.direction * force, ForceMode.Impulse);
            }
            firstTouch = false;
        }

        if (cam.GetComponent<SlowMo>().SlowValue == 0.05f)
        {
            StartCoroutine(Wait());
        }    

        if (Physics.Raycast(ray, out hit, 20f, LayerMask) && secondTouch == true)
        {
            if (Physics.Raycast(ray, out hit, LayerMask))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>()
                    .AddForce((ray.direction) * force, ForceMode.Impulse);
                gotHit = true;
            }            
        }

        if (gotHit == true)
        {
            StartCoroutine(Repeat());
        }
    }

    IEnumerator Wait()
    {
        secondTouch = true;

        switch (gameObject.GetComponent<Punch>().gotHit)
        {
            case true:
                secondTouch = false;
                yield break;
            case false:
                yield return new WaitForSeconds(0.75f);
                secondTouch = false;
                break;
        }
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSeconds(0.5f);
        gotHit = false;
    }
}