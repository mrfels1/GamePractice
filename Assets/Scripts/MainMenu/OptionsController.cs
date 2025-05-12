using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider _musicSlider;

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }
}
