using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0.8f, 1f);
    public float pitch = 2f;
    public float maxZoom = 10f;
    public float minZoom = 3f;
    public float zoomSpeed = 50f;

    public float rotateSpeed = 5f;

    private float currentZoom = 5f;
    bool isMouseButton3Down = false;
    GameSettingsManager gameSettings;

    private void Start()
    {
        transform.position = transform.position = target.position + offset * currentZoom;
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
        if (isMouseButton3Down)
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
        }
        if (gameSettings.isGamePadConnected) 
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("HorizontalView") * rotateSpeed);
        }
        transform.LookAt(target.position + Vector3.up * pitch);
        offset.z = -Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180f);       // Mathf stand for math float.
        offset.x = Mathf.Sin(-transform.eulerAngles.y * Mathf.PI / 180f);
        transform.position = Vector3.Lerp(transform.position, target.position + offset * currentZoom, Time.deltaTime * 5f);
    }
}
