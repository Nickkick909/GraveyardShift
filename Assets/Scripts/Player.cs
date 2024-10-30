using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private CameraLook cameraLook;

    [SerializeField] GameObject flashLight;

    public bool blockInput = true;
    public bool blockJump = false;
    public bool blockLook = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockInput = true;
        //controller = gameObject.AddComponent<CharacterController>();
        //transform.position = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!blockInput)
            HandleMovement();

        // Keep the camera block input in sync with the player so we only have to toggle it on the player object
        if (cameraLook.blockInput !=  blockLook)
        {
            cameraLook.blockInput = blockLook;
        }
       
    }

    void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * y);
        controller.Move(playerSpeed * Time.deltaTime * move);

        if (Input.GetButtonDown("Jump") && groundedPlayer && !blockJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void DisableFlashLight()
    {
        flashLight.SetActive(false);
    }

    public void EnableFlashLight()
    {
        flashLight.SetActive(true);
    }

    public void SetMovementSpeed(float speed)
    {
        playerSpeed = speed;
    }
}
