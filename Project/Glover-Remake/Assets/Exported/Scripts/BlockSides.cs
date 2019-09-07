using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSides : MonoBehaviour
{

    // set player into push state when enter trigger
    // set player out of Aniamtio state when exit trigger

    public enum BlockSide
    {
        Forward,
        Backward,
        Left,
        Right
    }
    public BlockSide side;
    Pushable pushable;

    // Start is called before the first frame update
    void Start()
    {
        pushable = gameObject.GetComponentInParent<Pushable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            pushable.canPush = true;
            pushable.anim.SetBool("Pushing", true);
            if(side == BlockSide.Forward)
            {
                pushable.moveDirection = -Vector3.right;
            }
            if (side == BlockSide.Backward)
            {
                pushable.moveDirection = Vector3.right;
            }

            if (side == BlockSide.Left)
            {
                pushable.moveDirection = -Vector3.forward;
            }
            if (side == BlockSide.Right)
            {
                pushable.moveDirection = Vector3.forward;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pushable.canPush = false;
            pushable.anim.SetBool("Pushing", false);
        }
    }
}
