using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    //Quelle: https://www.youtube.com/watch?v=K1xZ-rycYY8 
    //Einfaches Bewegungsskript für den Player. Nahezu unverändert übernommen. 

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        //Wert in Variable speichern
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
        }
        Flip();
    }

    private void FixedUpdate()
    {
        //Berechnung der Geschwindigkeit 
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        //Überprüfung ob Charakter am Boden ist -> Bei Kollision mit Boden (Player steht) kann gesprungen werden
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        //Drehen des Charakters abhängig von der Richtung
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}