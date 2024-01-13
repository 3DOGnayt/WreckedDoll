using UnityEngine;

public class RotatePosition : MonoBehaviour
{
    [SerializeField] private GameObject[] p;
    [SerializeField] private bool isRotate = false;
    [SerializeField] private float angleX = 2f;
    [SerializeField] private float angleY = 2f;
    [SerializeField] private float angleZ = 2f;

    private void FixedUpdate()
    {
        if (isRotate)
        {
            for (int i = 0; i < p.Length; i++)
            {
                p[i].transform.Rotate(angleX, angleY, angleZ);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            isRotate = true;
        }
    }
}