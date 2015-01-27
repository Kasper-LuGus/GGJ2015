using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class SimpleCharacterController : MonoBehaviour 
{
	public float walkSpeed = 5.0f;
	public float turnSpeed = 5.0f;
	public float jumpVelocity = 5.0f;


	protected Rigidbody rbody = null;
	protected bool grounded = false;


	public void SetupLocal()
	{
		if (gameObject.CacheComponent<Rigidbody>(ref rbody))
		{
			rbody.freezeRotation = true;
		}
	}
	
	public void SetupGlobal()
	{
	}
	
	protected void Awake()
	{
		SetupLocal();
	}

	protected void Start() 
	{
		SetupGlobal();
	}
	
	protected void Update() 
	{
		CheckGrounded();


		if (PlayerStateManager.use.state != PlayerStateManager.PlayerState.Free)
			return;

		CheckMovement();
		CheckJump();
	}

	protected void CheckGrounded()
	{
		grounded = Physics.Raycast(transform.position , -Vector3.up, 0.5f);
	}
	
	protected void CheckMovement()
	{
		if (!grounded)		// if we're preparing to levitate, stop moving
			return;

		float input = Input.GetAxis("Vertical");

		rbody.velocity = (transform.forward * input * walkSpeed).y(rbody.velocity. y);
	
    }

	protected void CheckJump()
	{
		if (rbody.useGravity == false && Input.GetButtonDown("Jump"))
		{
			rbody.useGravity = true;
		}

		if (!grounded)
			return;

		if (Input.GetButtonDown("Jump"))
		{
			rbody.velocity = rbody.velocity.y(jumpVelocity);
		}
	}


}
