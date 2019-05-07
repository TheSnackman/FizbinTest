﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Animator animator;
	Rigidbody2D rigbod2d;
	SpriteRenderer spriteRenderer;
	[SerializeField] Transform groundChecker;
	bool isGrounded;
	bool isRunning;

	[SerializeField] float speedModifier;
	[SerializeField] float jumpForce;
	[SerializeField] float runningModifier;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigbod2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	//Debug.Log("Velocity: " + rigbod2d.velocity);
    	//GroundCheck();
    	CheckForInput();
    }

    private void CheckForInput() {
    	if(isRunning && isGrounded) {
    		rigbod2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speedModifier * runningModifier, rigbod2d.velocity.y);
    		Debug.Log("Use Run Speed");
    	}
    	else {
    		if(isGrounded) {
    			rigbod2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speedModifier, rigbod2d.velocity.y);
    			Debug.Log("Use Walk Speed");
    		}
    	}

        if(Input.GetButtonDown("Jump") && isGrounded) {
        	//Debug.Log("Jump around");
        	rigbod2d.velocity = new Vector2(rigbod2d.velocity.x, jumpForce);
        }

        if(Input.GetButton("RunModifier")) {
        	//Debug.Log("Is Running");
        	isRunning = true;
        }
        else {
        	//Debug.Log("Isnt Running");
        	isRunning = false;
    	}
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
        	isGrounded = true;
        }
    }

    void OnCollisionStay2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
        	isGrounded = true;
        }
    }

    void OnCollisionExit2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
        	isGrounded = false;
        }
    }

    private void GroundCheck () {
    	if(Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"))) {
    		isGrounded = true;
    		Debug.Log("IsGrounded");
    	}
    	else {
    		isGrounded = false;
    		Debug.Log("IsntGrounded");
    	}
    }
}
