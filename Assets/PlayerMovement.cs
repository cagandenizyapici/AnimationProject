using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 5f;
    public float backwardSpeed = 3f; 
    public float rotationSpeed = 150f; 
    public float jumpHeight = 1.5f;

    [Header("Yerçekimi Ayarlarý")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    private CharacterController controller;
    private Animator animator;
    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger("JumpTrigger");
            }
        }
    }

    void Update()
    {
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);

       
        float currentSpeed = moveInput.y < 0 ? backwardSpeed : moveSpeed;

        
        Vector3 move = transform.forward * moveInput.y;
        controller.Move(move * currentSpeed * Time.deltaTime);

        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        if (animator != null)
        {
            
            animator.SetFloat("Speed", Mathf.Abs(moveInput.y));
        }
    }
}