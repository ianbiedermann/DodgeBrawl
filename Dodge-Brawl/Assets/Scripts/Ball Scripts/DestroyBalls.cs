using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyBalls : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator.SetBool("isExploding", false);
    }

    void FixedUpdate()
    {
        if ( (rb.velocity.x <= 0.3) && (rb.velocity.x >= -0.3) && (rb.velocity.y <= 0.3) && (rb.velocity.y >= -0.3) )
        {
            StartCoroutine(DestroyTheBall());
        }
    }

IEnumerator DestroyTheBall()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("isExploding");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

}
