using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<float> OnMove;

    private Vector3 _startPosition = Vector3.zero;
    private float _direction;

    private void Update()
    {
        OnMove?.Invoke(Input.GetAxis("Horizontal"));
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //case TouchPhase.Began:
                //    break;
                case TouchPhase.Moved:
                    _direction = touch.position.x > _startPosition.x ? 1 : -1;
                    _direction = touch.position.y > _startPosition.y ? 1 : -1;
                    break;
                //case TouchPhase.Stationary:
                //    break;
                //case TouchPhase.Ended:
                //    break;
                //case TouchPhase.Canceled:
                //    break;
                default:
                    _startPosition = touch.position;
                    _direction = 0f;
                    break;
            }
            OnMove?.Invoke(_direction);
        }
    }
}