using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rigidBody;
    public Animator animator;
    private bool _facingRight = true;

    private static readonly int AnimatorJump = Animator.StringToHash("Jump");
    private static readonly int AnimatorMoveHorizontal = Animator.StringToHash("MoveHorizontal");
    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check for any change in the Horizontal axis of controller, if value is !=0 is because the player should move left or right.
        var horizontalMove = Input.GetAxisRaw("Horizontal") * 3;
        // Check if there is any change in the side the player is facing and alter the sprite accordingly.
        switch (horizontalMove)
        {
            // Call Flip() in case the horizontalMove is != to 0
            case < 0 when _facingRight:
            case > 0 when !_facingRight:
                Flip();
                break;
        }
        // Apply any movement to the player if there is any change in the horizontalMove Variable.
        Move(horizontalMove);
        // Check is jump button is pressed and deploy the action.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Execute Jump only if is not jumping already
            if (!animator.GetBool(AnimatorJump))
                Jump(5f);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Move(float horizontalMove)
    {
        // Get a copy of the Player's Transform.position in the current frame.
        Vector2 position = transform.position;
        // Play the running animation
        animator.SetFloat(AnimatorMoveHorizontal, Mathf.Abs(horizontalMove));
        // Calculate X position
        position.x += horizontalMove * Time.deltaTime;
        // Update the player's Transform.position
        transform.position = position;
    }
    private void Jump(float velocity)
    {
        animator.SetBool(AnimatorJump, true);
        rigidBody.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
            animator.SetBool(AnimatorJump, false);
    }
}