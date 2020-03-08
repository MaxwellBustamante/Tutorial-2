using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;

    private Rigidbody2D rd2d;
    private int scoreValue = 0;
    private int lives = 3;

    public AudioSource musicSource;
    public AudioClip musicClipOne;

    private bool facingRight = true;
    Animator anim;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        SetLivesText();
        anim = GetComponent<Animator>();
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            SetScore();
            if (scoreValue == 4)
            {
                lives = 3;
                SetLivesText();
                transform.position = new Vector2(60.0f, -1.61f);
            }
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            Destroy(collision.collider.gameObject);
            SetLivesText();
        }

        if (scoreValue == 10)
        {
            musicSource.Stop();
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = false;
            winText.text = "You Win! Game created by Maxwell Bustamante";
            Destroy(this);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            }
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            winText.text = "You Lose! Game created by Maxwell Bustamante";
            Destroy(this);
        }
    }
    void SetScore()
    {
        scoreValue += 1;
        score.text = scoreValue.ToString();
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}