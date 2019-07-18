using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int score;
    private int lives;
    public float speed;
    public float jump;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    public AudioClip MusicClip;
    public AudioClip MusicClip2;
    public AudioSource MusicSource;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        SetAllText();
        winText.text = "";
        MusicSource.clip = MusicClip;
        MusicSource.Play();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

        if (Input.GetKey("escape"))
        Application.Quit();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
     if (other.gameObject.CompareTag("pickup"))
     {
          other.gameObject.SetActive(false);
          score = score + 1;
          SetAllText();
     }
     if (score == 4)
     {
         transform.position = new Vector2(30.0f, -2.0f);
         Camera.main.transform.position = new Vector3 (30.0f, 0.5f, -10.0f);
         lives = 3;
         SetAllText();
     }
     if (other.gameObject.CompareTag("enemy"))
     {
          other.gameObject.SetActive(false);
          lives = lives - 1;
          SetAllText();
        if (lives == 0)
        {
         Destroy(this);
         winText.text = "You Lose!";
        }
     }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }
        }
    }
    void SetAllText ()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        if (score >= 8)
        {
            winText.text = ("You Win!");
            MusicSource.clip = MusicClip2;
            MusicSource.Play();
        }

    }
}
