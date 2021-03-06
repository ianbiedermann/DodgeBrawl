using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Playermovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D Controller;
    [SerializeField] private Animator Anim;
    [SerializeField] private HealthController Health;
    [SerializeField] private Joystick joystick;

    public float runSpeed = 40f;

    private float horizontalmove = 0f;
    private float virticalspeed = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Player_gettingDamage"))
        {
            horizontalmove = 0;
            Anim.SetBool("IsJumping", false);


        }
        else 
        {
            if (joystick.Horizontal != 0)
            {
                horizontalmove = joystick.Horizontal * runSpeed;
            }
            else 
            {
                horizontalmove = Input.GetAxis("Horizontal") * runSpeed;
            }
            virticalspeed = Controller.m_Rigidbody2D.velocity.y;
            Anim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalmove));
            Anim.SetFloat("VerticalSpeed", virticalspeed);
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                Anim.SetBool("IsJumping", true);
                Debug.Log("Sprung");
            }
            if (Controller.WallJump)
            {
                Anim.SetBool("WallJumping", true);
                Debug.Log("WallJump");
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
         }
        
    }

    private void FixedUpdate()
    {
        Damage();
        //Chrakterbewegung 
        Controller.Move(horizontalmove * Time.fixedDeltaTime , crouch, jump);
        jump = false;

    }

    public void OnLanding()
    {
       
       

            if(Controller.m_Rigidbody2D.velocity.y < 0.01f)
            {
               Anim.SetBool("IsJumping", false);
               Anim.SetBool("WallJumping", false);
               Debug.Log("Landung");
            }
            

    }
    private void Damage()
    {
        Debug.Log(Health.damageTimer);
        if (Health.damageTimer > 0)
        {
            Health.damageTimer = Health.damageTimer - 1;
            Anim.SetBool("GettingDamage", true);
        }
        else
        {
            Anim.SetBool("GettingDamage", false);
        }
    }
    public void Jump()
    {
        jump = true;
    }
}
