using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float MoveSpeed = 5.0f;
    public Camera childCamera = null;
    public float LookSensitivity = 5.0f;
    public float LookSmooth = 2.0f;
    private Vector2 LookDirection;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        ControlMovement();
        ControlLookAround();
    

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void ControlMovement()
    {
        float xAxisMove = Input.GetAxis("Horizontal");
        float zAxisMove = Input.GetAxis("Vertical");

        this.transform.Translate(xAxisMove * MoveSpeed * Time.deltaTime, 0.0f, zAxisMove * MoveSpeed * Time.deltaTime);
    }

    
    private void ControlLookAround()
    {
        Vector2 mouseDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseDir = Vector2.Scale(mouseDir, new Vector2(LookSensitivity, LookSensitivity));

        Vector2 LookDelta = new Vector2();
        LookDelta.x = Mathf.Lerp(LookDelta.x, mouseDir.x, 1.0f / LookSmooth);
        LookDelta.y = Mathf.Lerp(LookDelta.y, mouseDir.y, 1.0f / LookSmooth);
        LookDirection += LookDelta;

        LookDirection.y = Mathf.Clamp(LookDirection.y, -75.0f, 75.0f);
        childCamera.transform.localRotation = Quaternion.AngleAxis(-LookDirection.y, Vector3.right);

        this.transform.localRotation = Quaternion.AngleAxis(LookDirection.x, this.transform.up); 

    }
}
