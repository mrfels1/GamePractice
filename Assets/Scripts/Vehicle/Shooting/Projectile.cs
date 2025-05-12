using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public GameObject impactEffect;
    public AudioClip impactSound;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Урон
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        // Эффекты
        if (impactEffect)
        {
            Instantiate(impactEffect, collision.contacts[0].point, Quaternion.identity);
        }

        if (impactSound)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }

        Destroy(gameObject);
    }
}
