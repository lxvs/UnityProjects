using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, .6f, .8f);
    public float pitch = 2.5f;
    public float maxZoom = 10f;
    public float minZoom = 3f;
    public float zoomSpeed = 50f;

    public float rotateSpeedHorizontal = 5f;
    public float rotateSpeedVertical = 4f;
    public float rotationUpMax = 30f;
    public float rotationDownMax = 45f;
    private float currentZoom = 5f;
    bool isMouseButton3Down = false;
    GameSettingsManager gameSettings;

    private void Start()
    {
        transform.position = target.position + Vector3.up * pitch + offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, target.eulerAngles.y);
        gameSettings = GameSettingsManager.instance;

    }

    private void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel != 0)
            {
                currentZoom = Mathf.Lerp(currentZoom, currentZoom - mouseScrollWheel * zoomSpeed, Time.deltaTime * 5f);
                currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            }

            if (Input.GetMouseButtonDown(2))
            {
                isMouseButton3Down = true;
            }

            if (Input.GetMouseButtonUp(2))
            {
                isMouseButton3Down = false;
            }

        }

        if(Input.GetButtonDown("ViewPointToggle"))
        {
            float zoomNotch = (maxZoom - minZoom) / 3f;
            int currentZoomLevel = (int)Mathf.Round((currentZoom - minZoom)/ zoomNotch);
            currentZoomLevel++;
            if (currentZoomLevel >= 4) currentZoomLevel = 0;
            currentZoom = minZoom + currentZoomLevel * zoomNotch;
            //Debug.Log("cz: " + currentZoom + ". czl: " + currentZoomLevel + ". czlf: " + (currentZoom - minZoom) / zoomNotch + ". round: " + Mathf.Round((currentZoom - minZoom) / zoomNotch));
        }
        
    }
    void LateUpdate()
    {
        float alpha = transform.eulerAngles.x * Mathf.PI / 180f;
        float theta = transform.eulerAngles.y * Mathf.PI / 180f;
        float deltaY;
        Vector3 horizon = new Vector3(-Mathf.Cos(theta), 0, Mathf.Sin(theta));        // perpendicular to camera sight line
        if (isMouseButton3Down)
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeedHorizontal);
            deltaY = Input.GetAxis("Mouse Y");
            if (deltaY != 0)
            {

                if (GetNormalizedEulerAngle(transform.eulerAngles.x) * Mathf.Sign(-deltaY) < (deltaY > 0 ? rotationUpMax : rotationDownMax)) // the sign rotation obeys the left hand rule 
                    transform.RotateAround(target.position, horizon, deltaY * rotateSpeedVertical);
            }

        }
        else if (gameSettings.isGamePadConnected) 
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("HorizontalView") * rotateSpeedHorizontal);
            deltaY = Input.GetAxis("VerticalView");
            if (deltaY != 0)
            {
                if (GetNormalizedEulerAngle(transform.eulerAngles.x) * Mathf.Sign(-deltaY) < (deltaY > 0 ? rotationUpMax : rotationDownMax))
                    transform.RotateAround(target.position + Vector3.up * pitch, horizon, deltaY * rotateSpeedVertical);
            }

        }
        transform.LookAt(target.position + Vector3.up * pitch);
        //Vector3 oldLEA = transform.eulerAngles;
        transform.eulerAngles = new Vector3(Mathf.Clamp(GetNormalizedEulerAngle(transform.eulerAngles.x), -rotationUpMax, rotationDownMax), transform.eulerAngles.y, transform.eulerAngles.z);
        //if (transform.localEulerAngles != oldLEA)
        //    Debug.LogWarning("old LEA = " + oldLEA + ".  new localEulerAngles = " + transform.localEulerAngles + ".   x = " + transform.eulerAngles.x + ".  normalized x = " + GetNormalizedEulerAngle(transform.eulerAngles.x));
        // (transform.rotation.eulerAngles.x > 180f ? transform.rotation.eulerAngles.x - 360f : transform.rotation.eulerAngles.x)

        //if (deltaY != 0)
        //    Debug.Log("deltaY = " + deltaY 
        //        + ".    Signed rotationX = " + (transform.rotation.eulerAngles.x > 180f ? transform.rotation.eulerAngles.x - 360f : transform.rotation.eulerAngles.x) * Mathf.Sign(deltaY)
        //        + ".    threshold = " + (deltaY > 0 ? rotationUpMax : rotationDownMax));

        offset.z = -Mathf.Cos(alpha) * Mathf.Cos(theta);
        offset.x = -Mathf.Cos(alpha) * Mathf.Sin(theta);
        offset.y = Mathf.Sin(alpha);

        //Debug.Log("alpha, sinAlpha = " + transform.rotation.eulerAngles.x + ", " + Mathf.Sin(alpha) + ".  Actual offset = " + (transform.position - target.position - Vector3.up * pitch) /currentZoom);
        transform.position = Vector3.Lerp(transform.position, target.position + Vector3.up * pitch + offset * currentZoom, Time.deltaTime * 5f);
    }

    float GetNormalizedEulerAngle(float angleInDegree)
    {
        while (angleInDegree > 180f)
        {
            angleInDegree -= 360f;
        }
        while (angleInDegree <= -180f)
        {
            angleInDegree += 360f;
        }
        return angleInDegree;
    }
}
