using UnityEngine;
using UnityEngine.UI; // Import UI for Toggle component
using TMPro;

public class SettingsScript : MonoBehaviour
{

    public Slider accelerometer_slider;
    public Slider gyroscope_slider;

    public Toggle accelerometer_toggle;
    public Toggle gyroscope_toggle;

    public TextMeshProUGUI accelerometer_speed_value_text;
    public TextMeshProUGUI gyroscope_speed_value_text;

    void Start()
    {
        // Add listeners to detect value change
        accelerometer_slider.onValueChanged.AddListener(value => UpdateValueText(value, accelerometer_speed_value_text));
        gyroscope_slider.onValueChanged.AddListener(value => UpdateValueText(value, gyroscope_speed_value_text));

        // Add listeners for toggles to switch modes
        accelerometer_toggle.onValueChanged.AddListener(isOn => { if (isOn) ActivateAccelerometerOnly(); });
        gyroscope_toggle.onValueChanged.AddListener(isOn => { if (isOn) ActivateGyroscopeOnly(); });
    }

    public void ActivateGyroscopeOnly()
    {
        gyroscope_slider.gameObject.SetActive(true);
        accelerometer_slider.gameObject.SetActive(false);
        accelerometer_toggle.isOn = false;

        UpdateValueText(gyroscope_slider.value, gyroscope_speed_value_text);

    }

    public void ActivateAccelerometerOnly()
    {
        gyroscope_slider.gameObject.SetActive(false);
        accelerometer_slider.gameObject.SetActive(true);
        gyroscope_toggle.isOn = false;

        UpdateValueText(accelerometer_slider.value, accelerometer_speed_value_text);
    }

    public void UpdateValueText(float value, TextMeshProUGUI speed_text)
    {
        // Update the text to display the slider's value
        speed_text.text = value.ToString("0.00");
    }
}
