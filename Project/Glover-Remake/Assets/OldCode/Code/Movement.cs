using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class Movement : MonoBehaviour {
    Rigidbody2D rbody;
    Animator anim;
    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;
    public bool CanMove;


    public float moveSpeed = 4;

    // Use this for initialization
    void Start()
    {

        CanMove = true;

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            CheckInput();

        }
        else
        {
            rbody.velocity = new Vector2(0, 0);

        }
    }

    private void CheckInput()
    {
        // move right 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rbody.velocity = Vector2.right * moveSpeed;
            anim.SetBool("walkingRight", true);

            //disable other anims
            anim.SetBool("walkingUp", false);
            anim.SetBool("walkingLeft", false);
            anim.SetBool("walkingDown", false);
          //  anim.SetBool("idle", false);
            //change direction
            isRight = true;
            isLeft = false;
            isUp = false;
            isDown = false;

            //disable idle anims
            anim.SetBool("idleRight", false);
            anim.SetBool("idleLeft", false);
            anim.SetBool("idleUp", false);
            anim.SetBool("idleDown", false);
        }
        // move left
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rbody.velocity = Vector2.left * moveSpeed;
            anim.SetBool("walkingLeft", true);

            //disable other anims
            anim.SetBool("walkingUp", false);
            anim.SetBool("walkingRight", false);
            anim.SetBool("walkingDown", false);
        //    anim.SetBool("idle", false);

            //change direction
            isRight = false;
            isLeft = true;
            isUp = false;
            isDown = false;

            //disable idle anims
            anim.SetBool("idleRight", false);
            anim.SetBool("idleLeft", false);
            anim.SetBool("idleUp", false);
            anim.SetBool("idleDown", false);

        }
        //move up
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rbody.velocity = Vector2.up * moveSpeed;
            anim.SetBool("walkingUp", true);

            //disable other anims
            anim.SetBool("walkingDown", false);
            anim.SetBool("walkingRight", false);
            anim.SetBool("walkingLeft", false);
       //     anim.SetBool("idle", false);

            //change direction
            isRight = false;
            isLeft = false;
            isUp = true;
            isDown = false;

            //disable idle anims
            anim.SetBool("idleRight", false);
            anim.SetBool("idleLeft", false);
            anim.SetBool("idleUp", false);
            anim.SetBool("idleDown", false);
        }

        //move down
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rbody.velocity = Vector2.down * moveSpeed;
            anim.SetBool("walkingDown", true);

            //disable other anims
            anim.SetBool("walkingUp", false);
            anim.SetBool("walkingRight", false);
            anim.SetBool("walkingLeft", false);
         //   anim.SetBool("idle", false);

            //change direction
            isRight = false;
            isLeft = false;
            isUp = false;
            isDown = true;

            //disable idle anims
            anim.SetBool("idleRight", false);
            anim.SetBool("idleLeft", false);
            anim.SetBool("idleUp", false);
            anim.SetBool("idleDown", false);

        }
        else if(isRight || isLeft || isUp || isDown)
        {
            if (isRight)
            {
                anim.SetBool("idleRight", true);
            }
            else if (isLeft)
            {
                anim.SetBool("idleLeft", true);
            }
            else if (isUp)
            {
                anim.SetBool("idleUp", true);
            }
            else if (isDown)
            {
                anim.SetBool("idleDown", true);
            }

            //anim.SetBool("idle",true);
            anim.SetBool("walkingUp", false);
            anim.SetBool("walkingRight", false);
            anim.SetBool("walkingLeft", false);
            anim.SetBool("walkingDown", false);
            rbody.velocity = Vector2.zero;
        }
    }
}