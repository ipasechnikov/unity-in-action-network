using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    private Camera cam;
    private CharacterController characterController;

    [SerializeField] float gravity = -9.8f;
    [SerializeField] float movementSpeed = 7.0f;
    [SerializeField] float rotationSpeed = 9.0f;
    [SerializeField] float rotationVerMinDeg = -45.0f;
    [SerializeField] float rotationVerMaxDeg = 45.0f;

    private float rotationVer = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    private void HandleKeyboardInput()
    {
        var deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        var deltaZ = Input.GetAxis("Vertical") * movementSpeed;

        var movement = new Vector3(deltaX, gravity, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    private void HandleMouseInput()
    {
        var rotationVerDelta = Input.GetAxis("Mouse Y") * rotationSpeed;
        var rotationHorDelta = Input.GetAxis("Mouse X") * rotationSpeed;

        // Horizontal rotation
        var bodyRotation = new Vector3(0, rotationHorDelta, 0);
        transform.Rotate(bodyRotation);

        // Vertical rotation
        rotationVer -= rotationVerDelta;
        rotationVer = Mathf.Clamp(rotationVer, rotationVerMinDeg, rotationVerMaxDeg);
        var cameraAngles = new Vector3(rotationVer, 0, 0);
        cam.transform.localEulerAngles = cameraAngles;
    }
}
