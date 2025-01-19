using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;  // Reference to the volume slider
    private AudioManager audioManager; // Reference to the AudioManager

    void Start()
    {
        // Find the AudioManager GameObject and get its AudioManager component
        audioManager = FindObjectOfType<AudioManager>();

        // Initialize the volume slider value to the current volume of the background music
        volumeSlider.value = audioManager.GetVolume();

        // Add listener to handle changes in slider value
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeSliderValueChanged(); });

        // Play background music if not already playing
        if (!audioManager.backgroundMusic.isPlaying)
        {
            audioManager.backgroundMusic.Play();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnVolumeSliderValueChanged()
    {
        // Set the volume level based on the slider value
        audioManager.SetVolume(volumeSlider.value);
    }
}
