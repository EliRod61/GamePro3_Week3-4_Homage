using UnityEngine;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour
{
    //Cookie Clicker/Clicker Games

    public float speed;
    public float jumpForce;

    //Money
    private float playerWallet;
    private float passiveMoney;
    private bool passiveIncome = false;

    public Transform orientation;
    Rigidbody rb;
    
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    [Header("Blob Shadow")]
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
        if (passiveIncome == true)
        {
            //when passiveincome is set true(player gets upgrade for getting money without clicking)
            //the passiveMoney (which will get bigger with more upgrades)
            //will be added into the the players wallet
            playerWallet += passiveMoney;
        }

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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); 
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

