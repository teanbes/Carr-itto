using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private int damage;
    
    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            CinemachineShake.Instance.ShakeCamera(1f, 0.05f);
        }
    }
}
