using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {


    // access variables
    public bool canMove;

    

    public float speed = 4f;
    float animRight = 0f;
    float animUp = 0f;
    bool isIdle = true;
    float animDirection = 2;
    Vector2 Direction;

    Animator anim;
    Rigidbody2D rigBody;

    simpleInventory inventory;
    simpleStats stats;
   // EventManager events;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        rigBody = gameObject.GetComponent<Rigidbody2D>();
        stats = FindObjectOfType<simpleStats>();
        inventory = FindObjectOfType<simpleInventory>();
       // events = gameObject.GetComponent<EventManager>();
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(canMove)
        {
            Move();

        
        }
 
	}


    public void Move()
    {
         
        GetInput();
        transform.Translate(Direction * speed * Time.deltaTime);
     //   rigBody.velocity = Direction * speed * Time.deltaTime;
        
    }


    private void GetInput()
    {
      Direction = Vector2.zero;

       animRight = 0f;
        animUp = 0f;
        isIdle = true;

        if (Input.GetKey(KeyCode.W))
        {
            isIdle = false;
            animUp = 1f;
            Direction += Vector2.up;
            animDirection = 0.1f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            isIdle = false;
            animUp = -1f;
            Direction += Vector2.down;
            animDirection = 0.2f;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            isIdle = false;
            animRight = -1f;
            Direction += Vector2.left;
            animDirection = 0.3f;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            isIdle = false;
            animRight = 1f;
            Direction += Vector2.right;
            animDirection = 0.4f;

        }

        if (isIdle)
        {
            anim.SetBool("isIdle", true);
            anim.SetFloat("direction", animDirection);
        }

        else
        {


            anim.SetBool("isIdle", false);
         //   anim.SetFloat("direction", animDirection);

            anim.SetFloat("Right", animRight);
            anim.SetFloat("Up", animUp);
        }

    }
}
