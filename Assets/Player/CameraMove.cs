using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    [Range(0, 100f)]
    public float OffsetX;
    [Range(0, 100f)]
    public float OffsetDownZ;
    [Range(0, 100f)]
    public float OffsetUpZ;
    private Vector3 cameraCenter;
    void Awake()
    {
        cameraCenter = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = new Vector3(Mathf.Clamp(Player.transform.position.x, cameraCenter.x - OffsetX, cameraCenter.x + OffsetX), cameraCenter.y, Mathf.Clamp(cameraCenter.z + Player.transform.position.z, cameraCenter.z - OffsetDownZ, cameraCenter.z + OffsetUpZ));
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        //Debug.Log(transform.position);
    }
}