using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    private Rigidbody rb;
    public GameObject deatheffect; 


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} получил урон: {amount}. Текущее здоровье: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} уничтожен.");
        rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false; // Отключаем NavMeshAgent
            Debug.Log($"{gameObject.name} отключил NavMeshAgent из-за смерти.");
        }
        if (rb != null)
        {
            rb.useGravity = true; // Останавливаем физику
            Debug.Log($"{gameObject.name} упал из-за смерти.");
        }
        if (deatheffect != null)
        {
            Instantiate(deatheffect, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name} создал эффект смерти.");
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}

