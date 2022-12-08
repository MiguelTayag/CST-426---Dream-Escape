using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public CharacterController characterController;
    public float movementSpeed = 2f;
    public int playerHealth = 100;
    private float gravity = -20f;
    Vector3 velocity;

    GameObject groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private float jumpHeight = 3f;

    private Animator characterAnimator;

    private GameObject Gun;

    private bool isGunActive;

    private void Start()
    {
        groundCheck = GameObject.FindWithTag("groundcheck");
        Gun = GameObject.FindWithTag("Gun");
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xDirection + transform.forward * zDirection;

        characterController.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (move.magnitude == 0)
        {
            characterAnimator.SetFloat("Speed", 0f);
        }
        else if (move.magnitude >= 0.1)
        {
            characterAnimator.SetFloat("Speed", 1);
        }

   
    }

    public void togglePlayerWeapon()
    {
        if(isGunActive == false)
        {
            characterAnimator.SetBool("isStanding", true);
            Gun.SetActive(false);
            isGunActive = true;
        }
        else if (isGunActive)
        {
            characterAnimator.SetBool("isStanding", false);
            Gun.SetActive(true);
            isGunActive = false;
        }
    }
}
