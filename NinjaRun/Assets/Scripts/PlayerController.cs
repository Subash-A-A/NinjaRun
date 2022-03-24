using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float laneGap = 3f;
    [SerializeField] float moveSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] float JumpForce = 2f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask whatIsGround;
    private float checkRadius = 0.1f;

    [Header("Controls")]
    [SerializeField] KeyCode JumpKey = KeyCode.Space;
    [SerializeField] KeyCode Left = KeyCode.A;
    [SerializeField] KeyCode Right = KeyCode.D;

    private float currentLane = 0;

    private Rigidbody rb;

    // Input
    private bool moveLeft;
    private bool moveRight;
    private bool isJumping;
    private bool isGrounded;

    private void Awake()
    {
        currentLane = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
        Move();
        LaneSwitch();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    void MyInput()
    {
        moveLeft = Input.GetKeyDown(Left) || Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(Right) || Input.GetKeyDown(KeyCode.RightArrow);
        isJumping = Input.GetKey(JumpKey);
    }

    void LaneSwitch()
    {
        if (moveLeft)
        {
            currentLane -= laneGap;
        }
        else if (moveRight)
        {
            currentLane += laneGap;
        }
        currentLane = Mathf.Clamp(currentLane, -laneGap, laneGap);

        Vector3 direction = new Vector3(currentLane, rb.position.y, rb.position.z);
        transform.position = Vector3.Lerp(transform.position, direction, 10 * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, checkRadius, whatIsGround);
        if (isGrounded && isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void Move()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }
}
