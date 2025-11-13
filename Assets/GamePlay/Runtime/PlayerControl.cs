using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum MoveState
    {
        idle = 0,
        right = 1,
        left = 2,
        jump = 3,
    }
    public MoveState moveState = 0;
    public Rigidbody2D rigidBody;

    public float jumpForce = 1f;
    public float maxJumpKeyTime = 0.3f;

    float jumpKeyTime = 0;
    void Awake()
    {
        if(!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            MoveStateControl();
            MoveLogicControl();
        }
    }

    void MoveStateControl()
    {
        bool isMove = false;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveState = MoveState.left;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightAlt))
        {
            moveState = MoveState.right;
            isMove = true;
        }
        
        if(rigidBody.velocity.y != 0)
        {
            moveState = MoveState.jump;
            isMove = true;
        }

        if(!isMove)
            moveState = MoveState.idle;
    }
    void MoveLogicControl()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightAlt))
        {

        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if(jumpKeyTime > 0)
            {
                jumpKeyTime -= Time.deltaTime;
                rigidBody.AddForce(jumpForce * Vector3.up * jumpKeyTime * jumpKeyTime);
            }
        }


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyTime = 0;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            jumpKeyTime = maxJumpKeyTime;
        }
    }
}
