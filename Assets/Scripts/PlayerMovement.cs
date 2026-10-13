using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
   [SerializeField] float movementSpeed = 6f;
   [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundedCheck;
    [SerializeField] LayerMask ground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput =Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(horizontalInput * movementSpeed, rb.linearVelocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            Debug.Log("fdfds");
        }
    }

    bool IsGrounded()
    {
       return Physics.CheckSphere(groundedCheck.position, .1f, ground);
    }
}
