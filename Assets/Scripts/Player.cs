
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IActor
{

    ///To change/////
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject hpBar;
    /////////////////

    ////Sword and bow
    [SerializeField]
    GameObject sword;
    [SerializeField]
    GameObject bow;

    /// Variables
    [SerializeField]
    int currentLevel        = 1;
    [SerializeField]
    int exp                 = 0;
    [SerializeField]
    int health              = 100;
    [SerializeField]
    int speed               = 10;
    [SerializeField]
    int jumpHeight          = 10;
    [SerializeField]
    float expNeeded         = 10;

    /// Events
    public event System.Action levelUpEvent;
    public event System.Action showMapEvent;
    public event System.Action playerDead;

    /// Inside Iterators and data
    int jumpHeightCurrent   = 0;
    Vector3 wektorRuchu     = Vector3.zero;
    bool isGrounded         = false;
    Rigidbody2D rb;

    /// Sword or Bow / Sword = 0 / Bow = 1
    bool bowOrSword = false;

    /// Coroutines
    IEnumerator CoroutineLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelUp();
    }

    /// Interfaces
    public void TakeDamage(int Damage)
    {
        health = health - Damage;
        hpBar.GetComponent<HpBar>().UpdateHP(health);
    }


    /// Game Loop
    void Start()
    {
        //StartCoroutine(CoroutineLevel());
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        InputCollect();
    }

    private void FixedUpdate()
    {
        Move();
        HealthCheck();
    }

    /// Unity Functions
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    /// Class Functions
    void InputCollect()
    {
        wektorRuchu = Vector3.zero;                         //One collect per Update
        if (Input.GetKey("w"))
        {
            Jump();
        }
        if (Input.GetKey("a"))
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            wektorRuchu += new Vector3(-1 * speed, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            wektorRuchu += new Vector3(0, -0.1f * speed, 0);
        }
        if (Input.GetKey("d"))
        {
            transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
            wektorRuchu += new Vector3(1 * speed, 0, 0);
        }
        if (Input.GetKeyDown("m"))
        {
            if(showMapEvent!=null)
            { 
                showMapEvent.Invoke(); 
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if (bowOrSword == false)
            {
                sword.GetComponent<IAction>().Action();
            }
            else
            {
                bow.GetComponent<IAction>().Action();
            }

        }

        if (Input.GetKeyDown("1"))
        {
            bowOrSword = false;
            bow.GetComponent<Weapon>().Show = false;
            sword.GetComponent<Weapon>().Show = true;
        }
        if (Input.GetKeyDown("2"))
        {
            bowOrSword = true;
            bow.GetComponent<Weapon>().Show = true;
            sword.GetComponent<Weapon>().Show = false;
        }



        ///To change [Narazie, zeby dalo sie wyjsc z builda]////
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!GameObject.FindGameObjectWithTag("Options"))
            {
                Instantiate(menu);
            }
        }
        /////////////////////////////////////////////////////////
    }
    void Move()
    {
        

        
        if (jumpHeightCurrent > 0)
        {
            wektorRuchu += new Vector3(0, 50, 0);
            jumpHeightCurrent -= 1;
        }
        if (wektorRuchu.y < 0)   // Faster falling, without affecting vertical speed and jump
        {
            wektorRuchu += new Vector3(0, -20, 0);
        }


        rb.AddForce(wektorRuchu * speed);

        
        wektorRuchu = Vector3.zero;

        VelocityCheck();
        

    }
    
    void Jump()
    {
        if(jumpHeightCurrent == 0 && isGrounded ==true) // JumpHeight == 0 make it, run once per Update()
        { 
            wektorRuchu += new Vector3(0, 50, 0);
            jumpHeightCurrent = jumpHeight;
            isGrounded = false;
        }
    }

    void VelocityCheck()
    {
        Vector3 velocity = rb.velocity;

        if(velocity.magnitude > speed)
        {
            velocity = velocity.normalized * speed;
            rb.velocity = velocity;
        }
    }

    void LevelUp()
    {
        if (exp >= expNeeded)
        {
            currentLevel += 1;
            expNeeded *= 1.5f;
            levelUpEvent.Invoke();
        }    
    }

    void HealthCheck()
    {
        if(health <= 0)
        {
            playerDead.Invoke();
        }
    }
}

