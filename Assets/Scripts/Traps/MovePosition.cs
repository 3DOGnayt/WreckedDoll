using System.Collections;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    [SerializeField] private float positionX = 0;
    [SerializeField] private float positionY = 0;
    [SerializeField] private float positionZ = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private float timeToStart = 0;
    [SerializeField] private float timeDown = 0;
    [SerializeField] private float timeRepeat = 0;
    [SerializeField] private bool isActive = false;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x + positionX, transform.position.y + positionY, transform.position.z + positionZ);
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (isActive)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (!isActive)
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            
            StartCoroutine(On());
        }
    }

    public IEnumerator On()
    {
        yield return new WaitForSeconds(timeToStart);
        isActive = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(timeDown);
        isActive = false;
        yield return new WaitForSeconds(timeRepeat);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}