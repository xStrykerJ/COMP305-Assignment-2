using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D theRB;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        ////Grab references for Rigidbody and animator
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Flip player when moving left and right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetBool("isGrabbing", isGrabbing());

        //Wall Jump
        if (wallJumpCooldown > 0.2f)
        {
            theRB.velocity = new Vector2(horizontalInput * speed, theRB.velocity.y);

            if (isGrabbing() && !isGrounded())
            {
                theRB.gravityScale = 0;
                theRB.velocity = Vector2.zero;
            }
            else
                theRB.gravityScale = 3;
            
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if (isGrabbing() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                theRB.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                theRB.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCooldown = 0;
        }       
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isGrabbing()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
            //other.collider.transform.SetParent(transform);
        }
        if (other.gameObject.CompareTag("Bouncy"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}

