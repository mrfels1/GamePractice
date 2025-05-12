using UnityEngine;

[System.Serializable]
public class WeaponMode
{
    public string name;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;
    public float fireRate = 10f; // выстрелов в секунду
    public float spreadAngle = 1.0f; // в градусах
    public ParticleSystem muzzleFlash;
    public AudioClip fireSound;
}

public class WeaponController : MonoBehaviour
{
    public WeaponMode[] weaponModes;
    public AudioSource audioSource;

    private int currentMode = 0;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchMode();
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();
        }
    }

    void SwitchMode()
    {
        currentMode = (currentMode + 1) % weaponModes.Length;
        Debug.Log("Switched to: " + weaponModes[currentMode].name);
    }

    void Fire()
    {
        WeaponMode mode = weaponModes[currentMode];

        // Таймер для скорострельности
        nextFireTime = Time.time + (1f / mode.fireRate);

        // Разброс
        Vector3 direction = mode.firePoint.forward;
        direction = Quaternion.Euler(
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            0
        ) * direction;

        // Создание снаряда
        GameObject projectile = Instantiate(
            mode.projectilePrefab,
            mode.firePoint.position,
            Quaternion.LookRotation(direction)
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * mode.projectileSpeed;
        }

        // Эффекты
        if (mode.muzzleFlash) mode.muzzleFlash.Play();
        if (mode.fireSound) audioSource.PlayOneShot(mode.fireSound);
    }
}
