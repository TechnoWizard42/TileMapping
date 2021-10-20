using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    private int livesValue = 3;

    public Text lives;

    public Text winText;

    public Text loseText;

    private bool facingRight = true;

    Animator anim;

    public AudioClip musicClipOne;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();

        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        scoreValue = 0;
        livesValue = 3;
        SetWinText();
        winText.text = "";
        SetLoseText();
        loseText.text = "";

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // PHYSICS AND UI BASED CODE PASSED THIS POINT
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

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
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetWinText();
        }

        else if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            SetLoseText();
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(33f, 2f);
            livesValue = 3;
            lives.text = livesValue.ToString();
        }

        if (livesValue == 0)
        {
            Destroy(gameObject);
        }

        else if (scoreValue == 8)
        {
            Destroy(gameObject);
        }
    }

    void SetWinText()
    {
        if (scoreValue >= 8)
        {
            winText.text = "You won Hartley's Platformer!";
        }
    }

    void SetLoseText()
    {
        if (livesValue <= 0)
        {
            loseText.text = "You lost Hartley's Platformer...";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}