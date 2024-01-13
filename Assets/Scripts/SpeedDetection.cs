using UnityEngine;

namespace DefaultNamespace
{
    public class SpeedDetection : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float stayDelay;
        [SerializeField] private Finish finish;
        private RagdollOnOff _ragdollOnOff;
        private float _curentTime;
        private bool isLevelEnded = false;

        private void Start()
        {
            _ragdollOnOff = GetComponent<RagdollOnOff>();
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude > 1)
            {
                _curentTime = Time.time;
            }
            if (Time.time - _curentTime >= stayDelay && !isLevelEnded && _ragdollOnOff.IsGameStared && _curentTime > 0)
            {
                finish.LevelEnd();
                isLevelEnded = true;
            }
        }
    }
}