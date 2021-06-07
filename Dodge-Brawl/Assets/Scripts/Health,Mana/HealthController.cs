using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    public float maxHealth = 100;
    public float currentHealth;
    public bool Damage;
    public HealthBar healthBar;
    AnimatorClipInfo[] m_CurrentClipInfo;
    public int damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage = false;
        m_CurrentClipInfo = this.Anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(m_CurrentClipInfo[0].clip.name);
        if (collision.gameObject.tag == "Ball" && damageTimer == 0 )
        {
            currentHealth = currentHealth - 10;
            Damage = true;
            Anim.enabled = false;
            Anim.enabled = true;
            Anim.SetTrigger("Damage");
            damageTimer = 10;

            if (currentHealth <= 0)
            {
                Destroy(GameObject.FindWithTag("Player"));
            }

            healthBar.SetHealth(currentHealth);

        }

    }

}
