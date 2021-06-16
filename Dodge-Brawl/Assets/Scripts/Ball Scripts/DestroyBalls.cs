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

        float velocity = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);

        if ( velocity <= 2.0f )
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
