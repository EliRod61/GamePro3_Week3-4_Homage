using UnityEngine;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour
{
    //Cookie Clicker/Clicker Games

    public float speed;
    public float jumpForce;
    public Transform orientation;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;
    public float groundDrag;

    [Header("Money")]
    private float playerWallet;
    private float passiveMoney;
    private bool passiveIncome = false;
    
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Player Shadow")]
    public GameObject shadow;
    public RaycastHit hit;
    public float offset;

    [Header("Keybinds")]
    public KeyCode menuKey = KeyCode.I;

    public GameObject buyMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check       0.5f is half the players height and 0.2f is extra length
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.2f, Ground);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        speedControl();

        if (passiveIncome == true)
        {
            //when passiveincome is set true(player gets upgrade for getting money without clicking)
            //the passiveMoney (which will get bigger with more upgrades)
            //will be added into the the players wallet
            playerWallet += passiveMoney;
        }

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        menuController();
        movePlayer();
        playerShadow();
    }

    private void menuController()
    {
        if (Input.GetKeyDown(menuKey))
        {
            //Open Menu
            buyMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Close Menu
            buyMenu.SetActive(false);
        }
    }

    private void movePlayer()
    {
        //caculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); 
        }
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit velocity
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void playerShadow()
    {
        Ray downRay = new Ray(new Vector3(this.transform.position.x, this.transform.position.y - offset, this.transform.position.z), -Vector3.up);

        //gets the hit from the raycast and converts it unto a vector3
        Vector3 hitPosition = hit.point;
        //transform the shadow to the location
        shadow.transform.position = hitPosition;
    }
}

