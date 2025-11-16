using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;

    public Vector3 camCenterPos;
    public float camFollowSpeed = 1;
    public float camMouseOffset = 0.3f;
    Vector3 mousePos;
    private void LateUpdate()
    {
        if(GameManager.instance.isGamePlaying)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camCenterPos = Vector3.Lerp(PlayerControl.instance.transform.position, mousePos, camMouseOffset);
            camCenterPos.z = -10;

            transform.position = Vector3.Lerp(transform.position, camCenterPos, camFollowSpeed / 10f);
        }
    }
}
