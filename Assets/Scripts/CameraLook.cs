using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float mouseSpeed = 100f;
    [SerializeField] Transform playerBody;

    public bool blockInput = true;

    float xRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        blockInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blockInput)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

            playerBody.Rotate(Vector3.up, mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        
    }
}
