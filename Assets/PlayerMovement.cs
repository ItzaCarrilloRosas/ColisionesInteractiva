using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Detección de suelo")]
    public Transform groundCheck; // Un punto vacío en los pies
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal
        horizontalInput = 0;
        if (Keyboard.current.aKey.isPressed) horizontalInput = -1;
        if (Keyboard.current.dKey.isPressed) horizontalInput = 1;

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Detectar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Saltar
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("JumpTrigger");
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento horizontal
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    // Método para activar animación de interacción
    public void Interact()
    {
        anim.SetBool("Interacting", true);
        Invoke("StopInteract", 0.5f); // Cambia 0.5f por la duración de tu animación
    }

    private void StopInteract()
    {
        anim.SetBool("Interacting", false);
    }
}
