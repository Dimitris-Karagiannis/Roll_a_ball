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

    public float speed = 10f;
    public TextMeshProUGUI count_text;
    public TextMeshProUGUI win_text_object;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        win_text_object.gameObject.SetActive(false);

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

        // Combine both accelerometer and gyroscope data
        // tilt = new Vector3(acceleration.x + gyroRotation.x, 0, acceleration.y + gyroRotation.y);

        // Only gyroscope data
        // tilt = new Vector3(gyroRotation.y, 0, -gyroRotation.x);

        // Only accelerometer data
        tilt = new Vector3(acceleration.x, 0, acceleration.y);
    }

    void FixedUpdate()
    {
        rb.AddForce(tilt * speed);
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
