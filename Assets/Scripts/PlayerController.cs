using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int score;
    private int jumps;
    public float speed = 0;
    public TextMeshProUGUI scoreText;
    public GameObject winText;
    public int jumpsCount = 2;
    public int jumpVelocity = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        jumps = 0;
        SetScoreText();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (rb.velocity.y == 0.0f)
        {
            jumps = jumpsCount;
        }
    }
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump ()
    {
        if (jumps > 0)
        {
            if (rb.velocity.y != 0.0f)
            {
                jumps--;
            }
            //rb.AddForce(new Vector3(0.0f, jumpVelocity, 0.0f));
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
            if (score >= 10)
            {
                winText.SetActive(true);
            }
        }
    }
}
