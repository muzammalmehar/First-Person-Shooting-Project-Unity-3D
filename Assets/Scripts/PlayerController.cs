using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 movementDirection = Vector3.zero;

    [SerializeField] protected GameObject characterHead;
    [SerializeField] protected float lookSpeed = 30.0f;
    [SerializeField] protected float movementSpeed = 10.0f;
    [SerializeField] protected float jumpForce = 10.0f;
    [SerializeField] protected float gravity = 9.807f;

    [SerializeField] protected bool invertVerticalLookDirection;

    [SerializeField] protected GameObject weapon1;
    [SerializeField] protected GameObject weapon2;

    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enableOverlapRecovery = true;
    }

    protected virtual void Update()
    {
        float lookRight = Input.GetAxisRaw("Mouse X");
        float lookUp = Input.GetAxisRaw("Mouse Y");

        float moveRight = Input.GetAxisRaw("Horizontal");
        float moveForward = Input.GetAxisRaw("Vertical");

        bool isJumping = Input.GetButtonDown("Jump");

        Move(moveRight, moveForward, isJumping);
        Look(lookRight, lookUp);
        SwitchWeapon();

        movementDirection.y -= gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    public void Move(float moveRight, float moveForward, bool isJumping)
    {
        if (characterController.isGrounded)
        {
            movementDirection = new Vector3(moveRight, 0.0f, moveForward);
            movementDirection = transform.TransformDirection(movementDirection);
            movementDirection *= movementSpeed;

            if (isJumping) { movementDirection.y = jumpForce; }
        }
    }

    public void Look(float lookRight, float lookUp)
    {
        float verticalLookDelta = (lookUp * lookSpeed) * Time.deltaTime;
        float horizontalLookDelta = (lookRight * lookSpeed) * Time.deltaTime;

        float desiredVerticalLookDelta = (invertVerticalLookDirection) ? verticalLookDelta : -verticalLookDelta;

        transform.Rotate(new Vector3(0.0f, horizontalLookDelta, 0.0f));
        characterHead.transform.Rotate(new Vector3(desiredVerticalLookDelta, 0.0f, 0.0f));
    }

    public void SwitchWeapon()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0) // Scroll up
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        else if (scrollInput < 0) // Scroll down
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }
    }
}
