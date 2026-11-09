using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundedCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpsound;

    [SerializeField] Transform cameraTransform;

    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get camera direction
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection =
                forward * verticalInput +
                right * horizontalInput;

            moveDirection = Vector3.ClampMagnitude(
                moveDirection, 1f
            );
        }
        else
        {
            moveDirection = new Vector3(
                horizontalInput, 0f, verticalInput
            );

            moveDirection = Vector3.ClampMagnitude(
                moveDirection, 1f
            );
        }

        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Move player using Rigidbody
        rb.linearVelocity = new Vector3(
            moveDirection.x * movementSpeed,
            rb.linearVelocity.y,
            moveDirection.z * movementSpeed
        );
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            jumpForce,
            rb.linearVelocity.z
        );

        if (jumpsound != null)
            jumpsound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(
            groundedCheck.position,
            .1f,
            ground
        );
    }
}
