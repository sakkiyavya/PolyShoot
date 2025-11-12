using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveState = PlayerControl.MoveState;

public class PlayerAnima : MonoBehaviour
{
    public Transform head;
    public Transform body;
    public Transform hand;
    public Transform footLeft;
    public Transform footRight;

    public PlayerControl playerControl;

    public float footAnimaSpeed = 1f;
    public float footAnimaXRange = 1f;
    public float footAnimaYRange = 0.6f;

    float moveContinueTime = 0f;

    Vector3 headOriginPos;
    Vector3 bodyOriginPos;
    Vector3 handOriginPos;
    Vector3 footLeftOriginPos;
    Vector3 footRightOriginPos;

    Vector3 footCenterOriginPos;

    bool initialed = false;

    // Start is called before the first frame update
    void Start()
    {
        InitialData();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGamePlaying && initialed)
        {
            FootAnima();
        }
    }

    void InitialData()
    {
        if (!head)
            head = transform.Find("Head");
        if (!body)
            body = transform.Find("Body");
        if (!hand)
            hand = transform.Find("Hand");
        if (!footLeft)
            footLeft = transform.Find("FootLeft");
        if (!footRight)
            footRight = transform.Find("FootRight");
        if(!playerControl)
            playerControl = GetComponent<PlayerControl>();

        if(hand)
            handOriginPos = hand.localPosition;
        if(body)
            bodyOriginPos = body.localPosition;
        if (hand)
            handOriginPos = hand.localPosition;
        if(footLeft)
            footLeftOriginPos = footLeft.localPosition;
        if(footRight)
            footRightOriginPos = footRight.localPosition;

        initialed = hand && body && hand && footLeft && footLeft && playerControl;
        if (initialed)
            footCenterOriginPos = (footLeftOriginPos + footRightOriginPos) / 2;
        else
            Debug.Log("Œ¥≥ı ºªØPlayerAnima");
    }

    void FootAnima()
    {
        switch(playerControl.moveState)
        {
            case MoveState.right:
                moveContinueTime -= Time.deltaTime;
                footLeft.localPosition = new Vector3( - Mathf.Cos(moveContinueTime * footAnimaSpeed) * footAnimaXRange, - Mathf.Sin(moveContinueTime * footAnimaSpeed) * footAnimaYRange, 0) + footCenterOriginPos;
                footRight.localPosition = new Vector3(Mathf.Cos(moveContinueTime * footAnimaSpeed) * footAnimaXRange, Mathf.Sin(moveContinueTime * footAnimaSpeed) * footAnimaYRange, 0) + footCenterOriginPos;
                break;
            case MoveState.left:
                moveContinueTime += Time.deltaTime;
                footLeft.localPosition = new Vector3( - Mathf.Cos(moveContinueTime * footAnimaSpeed) * footAnimaXRange, - Mathf.Sin(moveContinueTime * footAnimaSpeed) * footAnimaYRange, 0) + footCenterOriginPos;
                footRight.localPosition = new Vector3(Mathf.Cos(moveContinueTime * footAnimaSpeed) * footAnimaXRange, Mathf.Sin(moveContinueTime * footAnimaSpeed) * footAnimaYRange, 0) + footCenterOriginPos;
                break;
            case MoveState.idle:
                moveContinueTime = 0;
                footLeft.localPosition = footLeftOriginPos;
                footRight.localPosition = footRightOriginPos;
                break;
            case MoveState.jump:
                moveContinueTime = 0;

                break;
            default:
                break;
        }
    }    
}
