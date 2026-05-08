using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(3, 3, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-3, 3, 1);
    }
}