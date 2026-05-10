using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float attackDamage = 20f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Rigidbody2D rb;
    private float moveInput;
    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        moveInput = Input.GetAxisRaw("Horizontal");

        anim.SetBool("isRunning", moveInput != 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        
        // Play attack sound
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) audio.Play();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>() != null)
            {
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(3, 3, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-3, 3, 1);
    }

    public void OnDeath()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
    }
}