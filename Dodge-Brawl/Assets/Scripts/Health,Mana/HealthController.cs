using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            currentHealth = currentHealth - 10;

            if (currentHealth <= 0)
            {
                Destroy(GameObject.FindWithTag("Player"));
            }

            healthBar.SetHealth(currentHealth);

        }

    }

}
