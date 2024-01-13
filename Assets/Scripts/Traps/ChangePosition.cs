using System.Collections;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    [SerializeField] private GameObject point = null;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Quaternion angleTarget = Quaternion.Euler(0, 0, 0);
    [SerializeField] private Quaternion angleStart = Quaternion.Euler(0, 0, 0);
    [SerializeField] private float timeToStart = 0;
    [SerializeField] private float timeDown = 0;
    [SerializeField] private float timeRepeat = 0;

    private bool isActive = false;

    private void FixedUpdate()
    {
        if (isActive)
            point.transform.rotation = Quaternion.Lerp(transform.rotation, angleTarget, Time.deltaTime * speed);
        if(!isActive)
            point.transform.rotation = Quaternion.Lerp(transform.rotation, angleStart, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(On());
        }
    }

    public IEnumerator On()
    {
        yield return new WaitForSeconds(timeToStart);
        isActive = true;
        yield return new WaitForSeconds(timeDown);
        isActive = false;
        yield return new WaitForSeconds(timeRepeat);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}