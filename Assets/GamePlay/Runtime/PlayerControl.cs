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
    // Start is called before the first frame update
    void Start()
    {
        if(!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            MoveStateControl();
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
}
