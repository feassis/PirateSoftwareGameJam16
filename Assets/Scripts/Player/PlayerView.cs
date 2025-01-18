using Input;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private float lookSensitivityX = 100f;
    [SerializeField] private float lookSensitivityY = 3f;
    [SerializeField] private float verticalLookUperBounds = 90f;
    [SerializeField] private float verticalLookLowerBounds = -90f;
    [SerializeField] private Transform cameraTransform;
    
    private CharacterController characterController;

    private PlayerInput playerInput;
    private Vector2 lookInput;
    private float xCameraRotation = 10f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LookInput(Vector2 vector2)
    {
        Debug.Log(vector2.normalized);
        lookInput = vector2.normalized;
    }

    private void OnEnable()
    {
        if(playerInput == null)
        {
            return;
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        if(playerInput == null)
        {
            return;
        }

        playerInput.Disable();
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        characterController.transform.Rotate(Vector3.up * lookInput.x * lookSensitivityX * Time.deltaTime);
        xCameraRotation -= lookInput.y * lookSensitivityY * Time.deltaTime;
        xCameraRotation = Mathf.Clamp(xCameraRotation, verticalLookLowerBounds, verticalLookUperBounds);

        cameraTransform.localRotation = Quaternion.Euler(xCameraRotation, 0f, 0f);

        lookInput = Vector2.zero;
    }
}
