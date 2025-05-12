using UnityEngine;

[System.Serializable]
public class WeaponModeRaycast
{
    [Header("Fire Settings")]
    public string name;
    public Transform firePoint;
    public float projectileSpeed = 50f;
    public float fireRate = 10f; // выстрелов в секунду
    public float damage = 10f;
    public float spreadAngle = 1.0f; // в градусах
    public float bulletDrop = 9.81f; // м/с²
    public float maxDistance = 1000f;
    public float tracerDuration = 0.05f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public LineRenderer tracerPrefab;
    public AudioClip fireSound;
    public GameObject impactEffect;
}


public class WeaponController_Raycast : MonoBehaviour
{
    public WeaponModeRaycast[] weaponModes;
    public AudioSource audioSource;
    private int currentMode = 0;
    private float nextFireTime = 0f;

    private WeaponModeRaycast mode;
    void Start(){
        mode = weaponModes[currentMode];
    }
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
    
    /*
    void Fire()
    {
        
        
        nextFireTime = Time.time + (1f / mode.fireRate);

        if (mode.muzzleFlash) mode.muzzleFlash.Play();
        if (audioSource && mode.fireSound) audioSource.PlayOneShot(mode.fireSound);

        Vector3 direction = mode.firePoint.forward;
        direction = Quaternion.Euler(
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            0
        ) * direction;

        // Баллистический луч по шагам
        Vector3 origin = mode.firePoint.position;
        Vector3 velocity = direction * 300f; // начальная скорость
        float timestep = 0.02f;
        Vector3 prevPos = origin;
        bool hitDetected = false;

        for (float t = 0f; t < mode.maxDistance / velocity.magnitude; t += timestep)
        {
            Vector3 currentPos = origin + velocity * t + 0.5f * Vector3.down * mode.bulletDrop * t * t;
            if (Physics.Raycast(prevPos, currentPos - prevPos, out RaycastHit hit, Vector3.Distance(prevPos, currentPos)))
            {
                HandleHit(hit);
                DrawTracer(mode.firePoint.position, hit.point);
                hitDetected = true;
                break;
            }
            prevPos = currentPos;
        }

        if (!hitDetected)
        {
            Vector3 endPos = origin + direction * mode.maxDistance;
            DrawTracer(mode.firePoint.position, endPos);
        }
    }
    */

    void Fire()
    {
        nextFireTime = Time.time + (1f / mode.fireRate);

        if (mode.muzzleFlash) mode.muzzleFlash.Play();
        if (audioSource && mode.fireSound) audioSource.PlayOneShot(mode.fireSound);

        Vector3 direction = mode.firePoint.forward;
        direction = Quaternion.Euler(
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            Random.Range(-mode.spreadAngle, mode.spreadAngle),
            0
        ) * direction;

        // Создание визуального "снаряда" (tracer с баллистикой)
        GameObject tracerObject = new GameObject("Tracer");
        tracerObject.transform.position = mode.firePoint.position;
        tracerObject.transform.rotation = Quaternion.LookRotation(direction);

        var tracer = tracerObject.AddComponent<ProjectileTracerRaycast>();
        LineRenderer line = null;

        if (mode.tracerPrefab)
        {
            line = Instantiate(mode.tracerPrefab, tracerObject.transform);
            line.SetPosition(0, tracerObject.transform.position);
            line.SetPosition(1, tracerObject.transform.position + direction * 1f);
        }

        tracer.tracer = line;
        tracer.Initialize(direction, mode.projectileSpeed, mode.bulletDrop, mode.damage, mode.impactEffect);
    }
}
