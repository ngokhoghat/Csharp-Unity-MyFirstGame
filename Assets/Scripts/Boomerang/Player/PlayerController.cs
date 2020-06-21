using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveForce = 20f;
    public float jumpForce = 700f;
    public float maxVelocity = 4f;
    private bool grounded;
    private Rigidbody2D playerBody;
    private Animator anim;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayerMoveKeyBoard();
    }

    void PlayerMoveKeyBoard()
    {
        float forceX = 0f;
        float forceY = 0f;
        float vel = Mathf.Abs(playerBody.velocity.x);
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            if (vel < maxVelocity)
                forceX = moveForce;

            Vector3 scale = transform.localScale;
            scale.x = 1f;
            transform.localScale = scale;
            anim.SetBool("isWalk", true);

        }
        else if (h < 0)
        {
            if (vel < maxVelocity)
                forceX = -moveForce;

            Vector3 scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
            anim.SetBool("isWalk", true);
        }
        else if (h == 0)
        {
            anim.SetBool("isWalk", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                forceY = jumpForce;
            }
        }

        playerBody.AddForce(new Vector2(forceX, forceY));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

}
