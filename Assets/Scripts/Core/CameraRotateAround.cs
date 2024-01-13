using UnityEngine;

public class CameraRotateAround : MonoBehaviour 
{
	[SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private float sensitivity = 3;
    [SerializeField] private float limit = 80;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float X, Y;
    [SerializeField] private float angle1;
    [SerializeField] private float angle2;
    [SerializeField] private float clipingDis;

    void Start()
    {
        var position = transform.position;
        GetComponent<Camera>().nearClipPlane = Vector3.Distance(transform.position, target.position) - clipingDis;
        offset2 += new Vector3(position.x - target.position.x, position.y - target.position.y, position.z - target.position.z);

        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }

	void Update ()
    {
        CameraRotate();
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void CameraRotate()
    {
        if (Input.GetMouseButton(0))
        {
            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            Y += Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp(Y, angle1, angle2);

        }
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
    }

    private void LookAtTarget()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset2;
        transform.LookAt(target);
    }
}