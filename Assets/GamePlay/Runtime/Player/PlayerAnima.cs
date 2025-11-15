using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MoveState = PlayerControl.MoveState;

[RequireComponent(typeof(PlayerControl))]
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
    public float footAnimaYRange = 1f;

    public float bodyAnimaBreathSpeed = 1f;
    public float bodyAnimaBreathIntensity = 1f;

    public float handLength = 1f;

    public float headHittedOffset = 1f;

    public float deadAnimaTime = 1f;

    float moveContinueTime = 0f;

    Vector3 headOriginPos;
    Vector3 bodyOriginPos;
    Vector3 bodyOriginScale;
    Vector3 handOriginPos;
    Vector3 footLeftOriginPos;
    Vector3 footRightOriginPos;
    Vector3 footCenterOriginPos;
    Vector3 mousePos;

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
            BodyAnima();
            HandAnima();
            HeadAnima();
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

        if(head)
            headOriginPos = head.localPosition;
        if(body)
        {
            bodyOriginPos = body.localPosition;
            bodyOriginScale = body.localScale;
        }
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
        footAnimaSpeed = playerControl.runSpeed;
        footAnimaXRange = Mathf.Log10(playerControl.runSpeed) + 1;
        footAnimaYRange = Mathf.Log10(playerControl.runSpeed) + 1;
        switch (playerControl.moveState)
        {
            case MoveState.right:
                moveContinueTime -= Time.deltaTime;
                footLeft.localPosition = new Vector3( - Mathf.Cos(moveContinueTime * 5 * 1.5f * footAnimaSpeed) * 0.2f * footAnimaXRange, Mathf.Max( - Mathf.Sin(moveContinueTime * 5 * 1.5f * footAnimaSpeed), 0) * 0.1f * footAnimaYRange, 0) + footCenterOriginPos;
                footRight.localPosition = new Vector3(Mathf.Cos(moveContinueTime * 5 * 1.5f * footAnimaSpeed) * 0.2f * footAnimaXRange, Mathf.Max(Mathf.Sin(moveContinueTime * 5 * 1.5f * footAnimaSpeed), 0) * 0.1f * footAnimaYRange, 0) + footCenterOriginPos;
                break;
            case MoveState.left:
                moveContinueTime += Time.deltaTime;
                footLeft.localPosition = new Vector3( - Mathf.Cos(moveContinueTime * 5 * 1.5f * footAnimaSpeed) * 0.2f * footAnimaXRange, Mathf.Max( - Mathf.Sin(moveContinueTime * 5 * 1.5f * footAnimaSpeed), 0) * 0.1f * footAnimaYRange, 0) + footCenterOriginPos;
                footRight.localPosition = new Vector3(Mathf.Cos(moveContinueTime * 5 * 1.5f * footAnimaSpeed) * 0.2f * footAnimaXRange, Mathf.Max(Mathf.Sin(moveContinueTime * 5 * 1.5f * footAnimaSpeed), 0) * 0.1f * footAnimaYRange, 0) + footCenterOriginPos;
                break;
            case MoveState.idle:
                moveContinueTime = 0;
                footLeft.localPosition = footLeftOriginPos;
                footRight.localPosition = footRightOriginPos;
                break;
            case MoveState.jump:
                moveContinueTime = 0;
                footLeft.localPosition = footLeftOriginPos - playerControl.rigidBody.velocity.y * new Vector3(0, 0.02f, 0);
                footRight.localPosition = footRightOriginPos - playerControl.rigidBody.velocity.y * new Vector3(0, 0.02f, 0);
                break;
            default:
                break;
        }
    }    

    void BodyAnima()
    {
        body.localPosition = bodyOriginPos + bodyAnimaBreathIntensity * 0.03f * Mathf.Sin(Time.time * bodyAnimaBreathSpeed) * Vector3.up;
        body.localScale = bodyOriginScale * (1 + bodyAnimaBreathIntensity * 0.07f * Mathf.Sin(Time.time * bodyAnimaBreathSpeed));
    }

    void HandAnima()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        hand.localPosition = bodyOriginPos + handLength * Vector3.Normalize(mousePos - transform.position) * 0.4f;
    }
    void HeadAnima()
    {
        head.localPosition = Vector3.Lerp(head.localPosition, headOriginPos, 0.1f);
    }
    void HeadAnima(Vector3 dir, float damage)
    {
        head.localPosition = headOriginPos + dir.normalized * Mathf.Log10(damage) * 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.gameObject.layer == 9)
        {
            if(collision.gameObject.GetComponent<Projectile>())
            {
                HeadAnima(transform.position - collision.transform.position, collision.gameObject.GetComponent<Projectile>().damage);
            }
        }
    }

    public void StartDeadAnima(Vector2 d)
    {
        StartCoroutine(DeadAnima(d));
    }

    IEnumerator DeadAnima(Vector2 d)
    {
        float t = 0;
        float p = 10;
        while(t < deadAnimaTime)
        {
            t += Time.deltaTime;
            head.localPosition += new Vector3(d.x * p, 1f * d.y * p, 0) * Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime;
            head.localPosition = new Vector3(head.localPosition.x, Mathf.Max(head.localPosition.y, footCenterOriginPos.y), 0);

            body.localPosition += new Vector3(d.x * p, 0.5f * d.y * p, 0) * Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime;
            body.localPosition = new Vector3(body.localPosition.x, Mathf.Max(body.localPosition.y, footCenterOriginPos.y), 0);
            body.Rotate(0, 0, Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime * 30);

            hand.localPosition += new Vector3(d.x * p,  0.6f * d.y * p, 0) * Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime;
            hand.localPosition = new Vector3(hand.localPosition.x, Mathf.Max(hand.localPosition.y, footCenterOriginPos.y), 0);

            footLeft.localPosition += new Vector3(d.x * p, 0, 0) * Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime;
            footRight.localPosition += new Vector3(d.x * 0.9f * p, 0, 0) * Time.deltaTime * (deadAnimaTime - t) / deadAnimaTime;

            yield return null;
        }
    }
}
