using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource backgroundMusic; // Reference to the AudioSource for background music

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    public float GetVolume()
    {
        return backgroundMusic.volume;
    }
}
