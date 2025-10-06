using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10.0f;
    private float movementX;
    private float movementY;
    public int count;
    public float timeLeft = 60.0f;
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timerText;
    private AudioSource audioSource;

    private int totalPickUps;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        totalPickUps = GameObject.FindGameObjectsWithTag("PickUp").Length;
        SetCountText();
        winText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            count++;
            SetCountText();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Puntos: " + count.ToString() + "/" + totalPickUps;
        if (count >= totalPickUps)
        {
            winText.gameObject.SetActive(true);
        }
    }

    void CountdownTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Tiempo: " + Mathf.CeilToInt(timeLeft).ToString(); //esa funcion la tuve que googlear.
        }
        else
        {
            winText.text = "PERDISTE";
            winText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        CountdownTimer();
    }
}
