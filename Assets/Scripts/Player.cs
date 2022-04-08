using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;

    private bool isGrounded = true;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called at a constant rate (defined in the preferences, in Time)  
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        PlayerJump();
        AnimatePlayer();
    }

    void PlayerMoveKeyboard()
    {
        // Mapping: left or A --> -1 / no key pressed --> 0 / right or D --> +1
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        // Movement to the right
        if(movementX > 0)
        {
            // Enable "Walk" animation
            anim.SetBool(WALK_ANIMATION, true);
            // Player faces to the right
            sr.flipX = false;
        }
        else if(movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);            
            // Player faces to the left
            sr.flipX = true;
        }
        else
            anim.SetBool(WALK_ANIMATION, false);
    }

    void PlayerJump()
    {
        // For PC, the button should be the space bar and it should be different for every platform (mobile, console...)
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Built-in fct to detect collisions between two objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
            isGrounded = true;

        if(collision.gameObject.CompareTag(ENEMY_TAG))
            Destroy(gameObject);
    }

    // For triggers, ie objects that do not have a solid collider. Here, the ghost passes through all the object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
            Destroy(gameObject);
    }

} // class
