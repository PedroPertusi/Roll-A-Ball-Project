using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this if you are using TextMeshPro

public class FXButtonToggle : MonoBehaviour
{
    private bool isFXOn = true; // Default state
    private TextMeshProUGUI buttonText;

    void Start()
    {
        // Get the TextMeshProUGUI component from the child GameObject
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Load the saved state from PlayerPrefs if it exists, default to true (1)
        isFXOn = PlayerPrefs.GetInt("fxaudio", 1) == 1;
        UpdateText();
    }

    public void ToggleFX()
    {
        // Toggle the state
        isFXOn = !isFXOn;
        
        // Update the text based on the new state
        UpdateText();
        
        // Save the state to PlayerPrefs
        PlayerPrefs.SetInt("fxaudio", isFXOn ? 1 : 0);
        PlayerPrefs.Save();
        
        // Here you would also add your logic to actually toggle the FX audio
        // For example, setting a volume parameter, or enabling/disabling an audio component
    }

    private void UpdateText()
    {
        // Set the button text based on the state of isFXOn
        buttonText.text = isFXOn ? "FX Audio\nOn" : "FX Audio\nOff";
    }
}
