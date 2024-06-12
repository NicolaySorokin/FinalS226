using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class Control : Sounds
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private UnityEngine.Object explosion;
    public float footstepInterval = 0.1f;
    private float footstepTimer;
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject gameOverPanel; 
    public Button retryButton; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 2f;
        animator = GetComponent<Animator>();
        explosion = Resources.Load("Explosion");

        gameOverPanel.SetActive(false); 
        retryButton.onClick.AddListener(RestartLevel); 
    }

    void Update()
    {
        CheckingGround();
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
            PlaySound(sounds[0]);
        }
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (moveInput != 0 && onGround)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                PlaySound(sounds[1]);
                footstepTimer = footstepInterval;
            }
        }

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        animator.SetBool("onGround", onGround);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn") || other.CompareTag("Enemy"))
        {
            health--;
            PlaySound(sounds[4]);

            if (health <= 0)
            {
                PlaySound(sounds[3]);
                ShowGameOverPanel();
            }

            UpdateHealthUI();

            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}