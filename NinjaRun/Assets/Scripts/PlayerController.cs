using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float laneGap = 3f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float laneSwitchSmooth = 5f;
    [SerializeField] Transform cam;

    [Header("Jumping")]
    [SerializeField] float JumpForce = 2f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask whatIsGround;
    private float checkRadius = 0.15f;

    [Header("Controls")]
    [SerializeField] KeyCode JumpKey = KeyCode.Space;
    [SerializeField] KeyCode Left = KeyCode.A;
    [SerializeField] KeyCode Right = KeyCode.D;
    [SerializeField] KeyCode SlideKey = KeyCode.LeftShift;

    [Header("Animations")]
    [SerializeField] Animator anim;



    private float currentLane = 0f;

    [HideInInspector]
    public float camCurrentLane = 0f;

    // Components
    private Rigidbody rb;

    // Input
    private bool moveLeft;
    private bool moveRight;
    private bool isJumping;
    private bool isGrounded;
    private bool isSliding;

    private float jumpDir = 0.5f;

    private void Awake()
    {
        currentLane = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
        LaneSwitch();
        MovementAnimations();
        DashAnimations();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Slide();
    }

    void MyInput()
    {
        moveLeft = Input.GetKeyDown(Left) || Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(Right) || Input.GetKeyDown(KeyCode.RightArrow);
        isJumping = Input.GetKey(JumpKey) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        isSliding = Input.GetKey(SlideKey) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }

    void LaneSwitch()
    {
        if (moveLeft)
        {
            currentLane -= laneGap;
            camCurrentLane -= (laneGap - 0.5f);
        }
        else if (moveRight)
        {
            currentLane += laneGap;
            camCurrentLane += (laneGap - 0.5f);
        }
        currentLane = Mathf.Clamp(currentLane, -laneGap, laneGap);
        camCurrentLane = Mathf.Clamp(camCurrentLane, -(laneGap - 0.5f), (laneGap - 0.5f));

        Vector3 camDirection = new Vector3(currentLane, cam.position.y, cam.position.z);
        Vector3 direction = new Vector3(currentLane, rb.position.y, rb.position.z);

        // cam.position = Vector3.Lerp(cam.position, camDirection, (laneSwitchSmooth) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, direction, (laneSwitchSmooth) * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, checkRadius, whatIsGround);
        if (isGrounded && isJumping && !isSliding)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void Move()
    {
        // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        rb.AddForce(Vector3.forward * moveSpeed, ForceMode.Force);
    }

    void Slide()
    {
        if (isSliding && !isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.down * JumpForce * 2.5f, ForceMode.Impulse);
        }
    }

    void MovementAnimations()
    {
        if (isGrounded)
        {
            jumpDir = 0.5f;
        }
        if (!isGrounded && moveLeft)
        {
            jumpDir = Mathf.Lerp(jumpDir, 0f, 35 * Time.deltaTime);
        }
        if (!isGrounded && moveRight)
        {
            jumpDir = Mathf.Lerp(jumpDir, 1f, 35 * Time.deltaTime);
        }

        anim.SetFloat("Velocity", Mathf.Clamp(rb.velocity.z, 0f, 50f));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("Random", jumpDir);
    }

    void DashAnimations()
    {
        if (moveLeft && isGrounded)
        {
            anim.SetTrigger("DashLeft");
        }
        if (moveRight && isGrounded)
        {
            anim.SetTrigger("DashRight");
        }
    }

}
