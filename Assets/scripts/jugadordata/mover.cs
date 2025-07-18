using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Animación")]
    public Animator animator;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float horizontalInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Animación
        animator.SetFloat("movement", Mathf.Abs(rb.linearVelocity.x));

        // Flip
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ---------- MÉTODOS PARA BOTONES UI ----------

    public void IniciarMoverIzquierda() => horizontalInput = -1f;

    public void DetenerMoverIzquierda()
    {
        if (horizontalInput < 0f)
            horizontalInput = 0f;
    }

    public void IniciarMoverDerecha() => horizontalInput = 1f;

    public void DetenerMoverDerecha()
    {
        if (horizontalInput > 0f)
            horizontalInput = 0f;
    }
}
