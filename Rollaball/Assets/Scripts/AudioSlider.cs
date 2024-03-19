using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the slider value and listeners
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
