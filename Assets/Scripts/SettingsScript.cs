using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        // Load stored values
        accelerometer_speed_value = PlayerPrefs.GetFloat("AccelerometerSpeed", 5f);
        gyroscope_speed_value = PlayerPrefs.GetFloat("GyroscopeSpeed", 5f);

        accelerometer_slider.value = accelerometer_speed_value;
        gyroscope_slider.value = gyroscope_speed_value;

        // Ensure a valid option is selected at start
        OnDropdownValueChanged(controlDropdown.value);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Assume dropdown options are: 0 = Accelerometer, 1 = Gyroscope
        if (index == 0)
        {
            ActivateAccelerometerOnly();
            PlayerPrefs.SetInt("ControlMode", 0); // Save accelerometer control mode
        }
        else if (index == 1)
        {
            ActivateGyroscopeOnly();
            PlayerPrefs.SetInt("ControlMode", 1); // Save gyroscope control mode
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

        // Save to PlayerPrefs
        if (speedText == accelerometer_speed_value_text)
            PlayerPrefs.SetFloat("AccelerometerSpeed", value);
        else if (speedText == gyroscope_speed_value_text)
            PlayerPrefs.SetFloat("GyroscopeSpeed", value);

    }

    public void SaveChanges()
    {
        PlayerPrefs.Save(); // Make sure changes are saved
    }
}
