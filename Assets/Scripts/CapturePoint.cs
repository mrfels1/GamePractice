using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{
    public float captureTime = 5f; // Время захвата в секундах
    private float currentProgress = 0f;

    public Slider progressBar; // UI-слайдер захвата
    private bool playerInZone = false;
    private bool captured = false;
    public EnemySpawner spawner;

    private void Start()
    {
        if (progressBar != null)
            progressBar.value = 0;
    }

    private void Update()
    {
        if (captured) return;

        if (playerInZone)
        {
            currentProgress += Time.deltaTime;
            currentProgress = Mathf.Clamp(currentProgress, 0, captureTime);
        }
        else
        {
            currentProgress -= Time.deltaTime;
            currentProgress = Mathf.Clamp(currentProgress, 0, captureTime);
        }

        if (progressBar != null)
            progressBar.value = currentProgress / captureTime;

        if (currentProgress >= captureTime)
        {
            CaptureComplete();
        }
    }

    private void CaptureComplete()
    {
        captured = true;
        Debug.Log("Флаг захвачен!");
        spawner.StopSpawning();
        // Можно: сменить цвет, вызвать событие, засчитать победу и т.д.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Игрок вошел в зону захвата");
        playerInZone = true;

        spawner.StartSpawning();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Игрок покинул зону захвата");
        playerInZone = false;

        spawner.StopSpawning();
    }
}
