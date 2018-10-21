using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Transform trans;
    private Rigidbody2D rb2d;
    private BoxCollider2D collide;
    private SpriteRenderer spr;
    private AudioSource source;
    private PlayerAnimator anim;
    private bool facingRight = true;
    private int jumpWait = 0;
    private int damageWait = 0;
    private int health = 0;

    public float speed;
    public float jumpforce;
    public int invulnerabilityFrames;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public Camera theCamera;
    private AudioSource cameraAudio;

    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public AudioClip powerUpAppearsSound;
    public AudioClip stompSound;
    public AudioClip deathSound;
    public AudioClip winSound;

    public Rigidbody2D mushroom;

    public Text coinsText;
    public Text winText;
    public Text loseText;
    private int coins;



    void Start () {
        trans = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerAnimator>();
        cameraAudio = theCamera.GetComponent<AudioSource>();

        winText.text = "";
        loseText.text = "";
        coins = 0;
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update(){
       
    }

    
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal"); ;



        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround + ", " + moveHorizontal);

        if (isOnGround)
        {
            anim.animJump(false);
        }
        else
        {
            anim.animJump(true);
        }

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        coinsText.text = "Coins Collected: " + coins;

        jumpWait = Mathf.Max(0, jumpWait - 1);
        damageWait = Mathf.Max(0, damageWait - 1);

        if (damageWait > 0)
        {
            Blink(false);
        }
        else
        {
            Blink(true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void Blink(bool forceNormalState)
    {
        if (forceNormalState)
        {
            spr.enabled = true;
        }
        else
        {
            spr.enabled = !spr.enabled;
        }
    }

    void takeDamage(bool instantKill)
    {
        if (instantKill)
        {
            cameraAudio.Stop();
            cameraAudio.PlayOneShot(deathSound);
            loseText.text = "YOU LOSE!";
            gameObject.SetActive(false);
        }
        else if (damageWait == 0)
        {
            if (health > 0)
            {
                source.PlayOneShot(powerDownSound);
                damageWait = invulnerabilityFrames;
                health -= 1;
                anim.animPower(false);
                collide.size = new Vector2(0.8f, 0.8f);
                trans.position = new Vector2(trans.position.x, trans.position.y - 0.5f);
            }
            else
            {
                cameraAudio.Stop();
                cameraAudio.PlayOneShot(deathSound);
                loseText.text = "YOU LOSE!";
                gameObject.SetActive(false);
            }
        }
    }

    void powerUp()
    {
        source.PlayOneShot(powerUpSound);
        if (health == 0)
        {
            health = 1;
            anim.animPower(true);
            collide.size = new Vector2(0.8f, 1.8f);
            trans.position = new Vector2(trans.position.x, trans.position.y + 0.5f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isOnGround)
        {

            if (Input.GetKey(KeyCode.UpArrow) && jumpWait == 0)
            {
                Debug.Log("Jumping!");
               // rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
               rb2d.velocity = Vector2.up * jumpforce;


               source.PlayOneShot(jumpSound);
                jumpWait = 15;
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            takeDamage(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mushroom")
        {
            powerUp();
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "CoinBlock" || collision.tag == "Coin") {
            collision.gameObject.SetActive(false);
            source.PlayOneShot(coinSound);
            coins += 1;
        }
        else if (collision.tag == "MushroomBlock")
        {
            collision.gameObject.SetActive(false);
            source.PlayOneShot(powerUpAppearsSound);
            Instantiate(mushroom, new Vector3(collision.transform.position.x, collision.transform.position.y + 1f, 0), Quaternion.identity);

        }
        else if (collision.tag == "EnemyKill")
        {
            collision.GetComponent<EnemyDamaged>().activate();
            cameraAudio.PlayOneShot(stompSound);
            rb2d.velocity = Vector2.up * 4;
        }
        else if (collision.tag == "WinTrigger")
        {
            cameraAudio.Stop();
            cameraAudio.PlayOneShot(winSound);
            winText.text = "YOU WIN!";
            gameObject.SetActive(false);
        }
    }
}
