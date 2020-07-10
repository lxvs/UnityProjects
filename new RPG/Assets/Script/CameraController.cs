using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float maxZoom = 15f;
    public float minZoom = 5f;
    public float zoomSpeed = 5f;

    private float currentZoom = 10f;

    private void Start()
    {
        //transform.position = target.position + offset * currentZoom;
    }
    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        
    }
    void LateUpdate()
    {
        transform.LookAt(target.position + Vector3.up * pitch);
        offset.z = -Mathf.Cos(transform.eulerAngles.y * 3.1415926f / 180f);
        offset.x = Mathf.Sin(-transform.eulerAngles.y * 3.1415926f / 180f);
        transform.position = target.position + offset * currentZoom;
        //transform.LookAt(target.position + Vector3.up * pitch);
        //transform.RotateAround(target.position, Vector3.up, (target.eulerAngles.y + 180) );  // because of the offset, camera was set to face to y=180 degrees every frame.
        //Debug.Log(target.eulerAngles.y + "  " + transform.eulerAngles.y + " offset.z:" + offset.z + " offset.x:" + offset.x);
    }
}
