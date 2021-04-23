using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private float jumpForce = 100f;
    private Rigidbody2D rb;
    private Animator animator;
    public int healthPoints = 10;
    private SpriteRenderer sprite;
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite emptyHeart;

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
        lock (this)
        {
            if (healthPoints > 2 * hearts.Length) healthPoints = 2 * hearts.Length;
            if (healthPoints < 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            for (int i = 0; i < hearts.Length; i++)
            {
                //hearts[i].sprite = (int)(healthPoints / 2) > i ? fullHeart : healthPoints % 2 == 1 ? halfHeart : emptyHeart; 
                if (healthPoints >= (i+1)*2) hearts[i].sprite = fullHeart;
                else if (healthPoints == i*2 + 1) hearts[i].sprite = halfHeart;
                else hearts[i].sprite = emptyHeart;
                //1:57 Система здоровья для того, чтобы поменять max количество сердец
            }
        }


        if (Input.GetButton("Horizontal"))
        {
            Move();
            animator.SetInteger("state", 1);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            && Physics2D.OverlapCircleAll(transform.position, 0.75f).Length > 1)
        {
            Jump();
            animator.SetInteger("state", 2);
        }
        if (!Input.anyKey) animator.SetInteger("state", 0);
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

    public void HealthDown()
    {
        healthPoints--;
    }
}
