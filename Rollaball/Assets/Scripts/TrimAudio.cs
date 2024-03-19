using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float startAt = 10.0f; // Start 10 seconds into the clip
    public float duration = 5.0f; // Play for 5 seconds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayPartOfAudioClip();
    }

    void PlayPartOfAudioClip()
    {
        audioSource.time = startAt; // Set the time at which the playback will start
        audioSource.Play();
        Invoke("StopAudio", duration); // Schedule to stop the audio after the duration
    }

    void StopAudio()
    {
        audioSource.Stop();
    }
}
