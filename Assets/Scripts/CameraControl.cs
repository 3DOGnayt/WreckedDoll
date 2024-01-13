using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothSpeed;
    [SerializeField] private float clipingDis;   

    private Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Start()
    {       
        var position = transform.position;
        GetComponent<Camera>().nearClipPlane = Vector3.Distance(transform.position, target.position) - clipingDis;
        offset += new Vector3(position.x - target.position.x,
            position.y - target.position.y, position.z - target.position.z);
    }
    
    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        transform.LookAt(target);
        Vector3 smothedPosition =
            Vector3.Lerp(Position, desiredPosition, smoothSpeed * Time.deltaTime);
        Position = smothedPosition;
    }
}