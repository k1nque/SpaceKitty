using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private float jumpForce = 100f;
    private Rigidbody2D rb;
    private bool faceRight = true;
    private Animator animator;
    [SerializeField]
    private int healthPoints = 10;
    private SpriteRenderer sprite;
    // Start is called before the first frame update

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal")) Move();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) 
            && Physics2D.OverlapCircleAll(transform.position, 0.75f).Length > 1) Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + Vector2.right * (moveX * speed * Time.deltaTime));

        sprite.flipX = moveX < 0;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
