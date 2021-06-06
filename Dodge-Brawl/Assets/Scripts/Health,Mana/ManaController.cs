using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{

    public float maxMana = 100;
    public float currentMana;

    public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        currentMana += 0.001f;

        manaBar.SetMana(currentMana);

    }

    void TakeDamage(int damage)
    {
        if(currentMana >= 10)
        {
            currentMana -= damage;
        }  
    }

}
