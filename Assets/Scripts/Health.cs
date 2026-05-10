using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (healthSlider != null) healthSlider.value = currentHealth;

        if (anim != null) anim.SetTrigger("TakeHit");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        if (anim != null) anim.SetTrigger("Death");

        if (gameObject.CompareTag("Player"))
            GetComponent<PlayerMovement>().OnDeath();
        else
            Destroy(gameObject, 2f);
    }
}