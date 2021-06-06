using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// das war noch das alte Skript von dem ersten Tutorial

public class DestroyBalls : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);
    }

}
