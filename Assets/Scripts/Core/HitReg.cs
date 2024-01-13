using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class HitReg : MonoBehaviour
{
    [SerializeField] private BodyPart part;
    [SerializeField] private MeshRenderer showHit;

    public event Action<BodyPart, int> OnHit;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator FadeOutHitPart()
    {
        while (showHit.material.color.a > 0)
        {
            yield return new WaitForSeconds(0.1f);
            
        }
    }
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
            return;

        if (collisionInfo.gameObject.CompareTag("Finish"))
        {
            FindObjectOfType<Finish>().LevelEnd();
        }

        if (collisionInfo.gameObject.GetComponent<HitReg>() != null)
            return;

        int multiply = 1;
        var m = collisionInfo.gameObject.GetComponent<ObjectScoreMultiply>();

        if (m != null)
            multiply = m.Multiply;
        else if (collisionInfo.gameObject.transform.parent != null &&
                 collisionInfo.gameObject.transform.parent.gameObject.GetComponent<ObjectScoreMultiply>() != null)
        {
            multiply = collisionInfo.gameObject.transform.parent.gameObject.GetComponent<ObjectScoreMultiply>()
                .Multiply;
        }

        if (showHit !=  null)
        {
            showHit.material.DOKill();
            var colorHit = showHit.material.color;
            colorHit.a += 0.5f;
            showHit.material.color = colorHit;
            colorHit.a = 0;
            showHit.material.DOColor(colorHit, 1f);

        }

        OnHit?.Invoke(part, multiply);

        foreach (var t in collisionInfo.contacts)
        {
            Color color;
            switch (part)
            {
                case BodyPart.LLeg:
                    color = Color.blue;
                    break;
                case BodyPart.RLeg:
                    color = Color.green;
                    break;
                case BodyPart.LArm:
                    color = Color.red;
                    break;
                case BodyPart.RArm:
                    color = Color.yellow;
                    break;
                case BodyPart.Head:
                    color = Color.cyan;
                    break;
                case BodyPart.Chest:
                    color = Color.black;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#if UNITY_EDITOR


            Debug.DrawLine(t.point - new Vector3(0, 0.5f), t.point + new Vector3(0, 0.5f), color, 10f);
            Debug.DrawLine(t.point - new Vector3(0.5f, 0), t.point + new Vector3(0.5f, 0), color, 10f);
#endif
        }

        _rigidbody.AddForce(collisionInfo.impulse * 100f);
    }
}