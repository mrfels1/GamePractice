
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 

public class HealthUIController : MonoBehaviour
{
    public Health playerHealth;      // Ссылка на Health игрока
    public GameObject gameovertext; // UI текст для Game Over
    public Slider healthSlider;      // UI слайдер

    private void Start()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.maxHealth;
        }
    }

    private void Update()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.currentHealth;
        }
        if (playerHealth.currentHealth <= 0)
        {
            healthSlider.gameObject.SetActive(false); // Скрыть слайдер, если здоровье 0
            gameovertext.SetActive(true); // Показать текст Game Over
            RestartScene(); // Перезапустить сцену через заданное время
            Debug.Log("Game Over! Перезапуск сцены...");
            
        }
    }

    public float delay = 5f; // Время до перезапуска

    public void RestartScene()
    {
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
