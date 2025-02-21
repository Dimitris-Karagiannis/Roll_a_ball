using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro for TMP_Dropdown

public class SettingsScript : MonoBehaviour
{
    public Slider accelerometer_slider;
    public Slider gyroscope_slider;

    public TMP_Dropdown controlDropdown; // Dropdown for selecting control mode

    public TextMeshProUGUI accelerometer_speed_value_text;
    public TextMeshProUGUI gyroscope_speed_value_text;

    public float accelerometer_speed_value;
    public float gyroscope_speed_value;

    void Start()
    {
        // Add listeners to update speed values
        accelerometer_slider.onValueChanged.AddListener(value => UpdateSpeedValue(value, ref accelerometer_speed_value, accelerometer_speed_value_text));
        gyroscope_slider.onValueChanged.AddListener(value => UpdateSpeedValue(value, ref gyroscope_speed_value, gyroscope_speed_value_text));

        // Add listener for dropdown selection
        controlDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Ensure a valid option is selected at start
        OnDropdownValueChanged(controlDropdown.value);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Assume dropdown options are: 0 = Accelerometer, 1 = Gyroscope
        if (index == 0)
        {
            ActivateAccelerometerOnly();
        }
        else if (index == 1)
        {
            ActivateGyroscopeOnly();
        }
    }

    public void ActivateGyroscopeOnly()
    {
        gyroscope_slider.gameObject.SetActive(true);
        accelerometer_slider.gameObject.SetActive(false);
    }

    public void ActivateAccelerometerOnly()
    {
        gyroscope_slider.gameObject.SetActive(false);
        accelerometer_slider.gameObject.SetActive(true);
    }

    public void UpdateSpeedValue(float value, ref float speedValue, TextMeshProUGUI speedText)
    {
        speedValue = value; // Store the speed value
        speedText.text = value.ToString("0.00"); // Update UI text
    }
}

