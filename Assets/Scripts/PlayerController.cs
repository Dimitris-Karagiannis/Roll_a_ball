using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movement_x;
    private float movement_y;
    private int count;
    private Vector3 tilt;

    private float accelerometer_speed;
    private float gyroscope_speed;

    public TextMeshProUGUI count_text;
    public TextMeshProUGUI win_text_object;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        win_text_object.gameObject.SetActive(false);

        // Load the selected control mode from PlayerPrefs
        int controlMode = PlayerPrefs.GetInt("ControlMode", 0); // Default to Accelerometer

        // Load corresponding speed values from PlayerPrefs
        accelerometer_speed = PlayerPrefs.GetFloat("AccelerometerSpeed", 5f);
        gyroscope_speed = PlayerPrefs.GetFloat("GyroscopeSpeed", 5f);

        // Apply the correct speed based on the selected control mode
        if (controlMode == 0) // Accelerometer
        {
            gyroscope_speed = 0f;  // Disable gyroscope speed
        }
        else if (controlMode == 1) // Gyroscope
        {
            accelerometer_speed = 0f;  // Disable accelerometer speed
        }

        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;
    }

    void OnMove(InputValue movement_value)
    {
        Vector2 movement_vector = movement_value.Get<Vector2>();
        movement_x = movement_vector.x;
        movement_y = movement_vector.y;
    }

    void SetCountText()
    {
        count_text.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            win_text_object.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        Vector3 gyroRotation = SystemInfo.supportsGyroscope ? Input.gyro.rotationRateUnbiased : Vector3.zero;

        // Combine both accelerometer and gyroscope data if both are enabled
        if (accelerometer_speed > 0f)
        {
            tilt = new Vector3(acceleration.x, 0, acceleration.y); // Only accelerometer data
        }
        else if (gyroscope_speed > 0f)
        {
            tilt = new Vector3(gyroRotation.y, 0, -gyroRotation.x); // Only gyroscope data
        }
    }

    void FixedUpdate()
    {
        if (accelerometer_speed > 0f || gyroscope_speed > 0f)
        {
            rb.AddForce(tilt * Mathf.Max(accelerometer_speed, gyroscope_speed)); // Apply the active speed
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            count += 1;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            win_text_object.gameObject.SetActive(true);
            win_text_object.text = "You Lose!!!";
        }
    }
}
