
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsCollieder;
	[SerializeField] private Transform m_GroundCheckRight;
	[SerializeField] private Transform m_GroundCheckLeft;   // A position marking where to check if the player is grounded.
	[SerializeField] public Transform m_GroundCheckMid;
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;    // A collider that will be disabled when crouching
	[SerializeField] private SpriteRenderer spriteRender;
	public bool WallJump = false;
	private bool linksGround = false;
	private bool rechtsGround = false;
	private bool mitteGround = false;
	private bool rechtsWand = false;
	private bool linksWand = false;
	private bool allGround = false;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .3f; // Radius of the overlap circle to determine if the player can stand up
	public Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
	private int Aircontroltimer;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		WallJump = false;
		allGround = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		if (linksGround || rechtsGround || mitteGround)
        {
			m_Grounded = true;
			if (!wasGrounded)
				OnLandEvent.Invoke();

		}
		linksGround = Collisioncheck(m_GroundCheckLeft, Vector2.down, 0.6f, m_WhatIsCollieder);
		rechtsGround = Collisioncheck(m_GroundCheckRight, Vector2.down, 0.6f, m_WhatIsCollieder);
		mitteGround = Collisioncheck(m_GroundCheckMid, Vector2.down, 0.3f, m_WhatIsCollieder);
		linksWand = Collisioncheck(m_GroundCheckLeft, Vector2.left, 0.3f, m_WhatIsCollieder);
		rechtsWand = Collisioncheck(m_GroundCheckRight, Vector2.right, 0.3f, m_WhatIsCollieder);
		if (linksGround || rechtsGround || mitteGround)
        {
			allGround = true;
			//Debug.Log("Grounded");
        }
	}
	private bool Collisioncheck(Transform Position, Vector2 Direction, float Distance, LayerMask m_Layers)
    {
		Color rayColor;
		RaycastHit2D raycastHit= Physics2D.Raycast(Position.position, Direction, Distance, m_Layers);
		if (raycastHit.collider != null)
        {
			rayColor = Color.green;
			return true;
        }
		rayColor= Color.red;
		Debug.DrawRay(Position.position, Direction * Distance, rayColor);
		return false;
    }


	public void Move(float move, bool crouch, bool jump)
	{
		m_AirControl = true;
		if (Aircontroltimer > 0)
        {
			m_AirControl = false;
			Aircontroltimer = Aircontroltimer - 1;
        }
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsCollieder))
			{
				crouch = true;
			}
		}
		//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
			if (Mathf.Abs( m_Rigidbody2D.velocity.y) > 0.1)
            {
				move *= 0.8f;
            }
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			if (m_Rigidbody2D.velocity.x > 0.2f && spriteRender.flipX)
			{
				// ... flip the player.
				Flip();
				Debug.Log("Drehen1");
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (m_Rigidbody2D.velocity.x < -0.2f && !spriteRender.flipX )
			{
				// ... flip the player.
				Flip();
				Debug.Log("Drehen2");
				Debug.Log(m_Rigidbody2D.velocity.x);
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			Debug.Log("NormalJump");
        }
        else
		{
			if (jump && m_Rigidbody2D.velocity.y <= 0f && rechtsWand)
			{
				m_Rigidbody2D.AddForce(new Vector2(-600, m_JumpForce));
				WallJump = true;
				Flip();
				Aircontroltimer = 500;
			}
			if (jump && m_Rigidbody2D.velocity.y <= 0f && linksWand)
			{
				m_Rigidbody2D.AddForce(new Vector2(600, m_JumpForce));
				WallJump = true;
				Flip();
				Aircontroltimer = 500;
			}
		}

	}


	private void Flip()
	{
		spriteRender.flipX = !spriteRender.flipX; 
		
	}
}
