using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movement_x;
    private float movement_y;
    private int count;

    public float speed = 0;
    public TextMeshProUGUI count_text;
    public TextMeshProUGUI win_text_object;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        win_text_object.gameObject.SetActive(false);
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

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);
        rb.AddForce(movement * speed);
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
