using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    public float sensitivity = 3f;
    public float distance = 6f;
    public float minDistance = 1f;
    public float maxDistance = 15f;
    public float zoomSpeed = 3f;
    public float shoulderOffset = 1.5f;
    public float rotationSpeed = 12f;

    public bool shiftLock = false;

    private float yaw;
    private float pitch = 20f;

    void Start()
    {
        if (player != null)
            yaw = player.eulerAngles.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftLock = !shiftLock;

            Cursor.lockState = shiftLock
                ? CursorLockMode.Locked
                : CursorLockMode.None;

            Cursor.visible = !shiftLock;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Camera rotation
        if (shiftLock || Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        }

        pitch = Mathf.Clamp(pitch, -70f, 70f);

        // Rotate player with camera in Shift Lock
        if (shiftLock)
        {
            Quaternion playerRotation =
                Quaternion.Euler(0f, yaw, 0f);

            player.rotation = Quaternion.Slerp(
                player.rotation,
                playerRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(
            distance, minDistance, maxDistance
        );

        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition =
            player.position + Vector3.up * 1.5f;

        if (shiftLock)
        {
            targetPosition +=
                rotation * Vector3.right * shoulderOffset;
        }

        transform.position = targetPosition -
            rotation * Vector3.forward * distance;

        transform.rotation = rotation;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


