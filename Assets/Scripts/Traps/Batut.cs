using UnityEngine;

public class Batut : MonoBehaviour
{
    [SerializeField] private Vector3 vector = new Vector3(0, 1, 1);
    [SerializeField] private float power;
    [Space]
    [SerializeField] private Vector3 vector2 = new Vector3(0, 0, 0);
    [SerializeField] private float power2;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(vector * power, ForceMode.Impulse);
            other.gameObject.GetComponent<Rigidbody>().AddForce(vector2 * power2, ForceMode.Impulse);
        }
    }
}
