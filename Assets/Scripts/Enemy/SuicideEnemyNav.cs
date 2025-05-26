using UnityEngine;
using UnityEngine.AI;

public class SuicideEnemyNav : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float damage = 50f;
    public float explosionForce = 500f;
    public float upwardsModifier = 1f; // Модификатор силы взрыва вверх
    public GameObject explosionEffect; // (опционально) эффект взрыва

    private Transform target;
    private NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var col in colliders)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log($"{rb.name}: AddExplosionForce");
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
            }
        }

        Destroy(gameObject);
    }
}
