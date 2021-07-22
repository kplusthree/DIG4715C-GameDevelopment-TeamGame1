using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float rotateSpeed;
    public GameObject projectilePrefab;
    public ParticleSystem smoke;
    float lookDirection;
    private static Rigidbody2D proj2D;

    public AudioClip AsteroidMed;
    public AudioClip Projectile;
    [HideInInspector]
    public AudioSource projSource;
    [HideInInspector]
    public bool transported;
    [HideInInspector]
    public bool invincible;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        projSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        lookDirection = Input.GetAxisRaw("Vertical");
        transported = false;
        invincible = false;
        animator.SetBool("IsDead", false);
    }

    // Physics
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 rotate = new Vector3(0, 0, horizontal * rotateSpeed);
        transform.Rotate(rotate);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

    //Fires Projectile
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
        projSource.clip = Projectile;
        projSource.Play();
        proj2D = projectileObject.GetComponent<Projectile>().rigidbody2d;
        proj2D.AddForce(transform.up * 300);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Launch();
        }

        if (invincible == true)
        {
            GameObject[] BigAsts = GameObject.FindGameObjectsWithTag("Big");
            GameObject[] MedAsts = GameObject.FindGameObjectsWithTag("Medium");
            GameObject[] SmallAsts = GameObject.FindGameObjectsWithTag("Small");
            GameObject[] UFOs = GameObject.FindGameObjectsWithTag("UFO");
            foreach (GameObject Big in BigAsts)
            {
                Physics2D.IgnoreCollision(Big.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            foreach (GameObject Medium in MedAsts)
            {
                Physics2D.IgnoreCollision(Medium.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            foreach (GameObject Small in SmallAsts)
            {
                Physics2D.IgnoreCollision(Small.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            foreach (GameObject UFO in UFOs)
            {
                Physics2D.IgnoreCollision(UFO.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

        if (invincible == false)
        {
            GameObject[] BigAsts = GameObject.FindGameObjectsWithTag("Big");
            GameObject[] MedAsts = GameObject.FindGameObjectsWithTag("Medium");
            GameObject[] SmallAsts = GameObject.FindGameObjectsWithTag("Small");
            GameObject[] UFOs = GameObject.FindGameObjectsWithTag("UFO");
            foreach (GameObject Big in BigAsts)
            {
                if (Big.GetComponent<Asteroid>().stopEnemy == true)
                {
                    Physics2D.IgnoreCollision(Big.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                else
                {
                    Physics2D.IgnoreCollision(Big.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
            }
            foreach (GameObject Medium in MedAsts)
            {
                if (Medium.GetComponent<Asteroid>().stopEnemy == true)
                {
                    Physics2D.IgnoreCollision(Medium.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                else
                {
                    Physics2D.IgnoreCollision(Medium.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
            }
            foreach (GameObject Small in SmallAsts)
            {
                if (Small.GetComponent<Asteroid>().stopEnemy == true)
                {
                    Physics2D.IgnoreCollision(Small.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                else
                {
                    Physics2D.IgnoreCollision(Small.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
            }
            foreach (GameObject UFO in UFOs)
            {
                if (UFO.GetComponent<Asteroid>().stopEnemy == true)
                {
                    Physics2D.IgnoreCollision(UFO.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                else
                {
                    Physics2D.IgnoreCollision(UFO.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameController instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        GameObject life = GameObject.FindGameObjectWithTag("Life");
        GameObject life2 = GameObject.FindGameObjectWithTag("Life2");

        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Boundary")
        {
            animator.SetBool("IsDead", true);
            invincible = true;
            Invoke("ResetInvincibility", 5);
            projSource.clip = AsteroidMed;
            projSource.Play();
            speed = 0;
            rotateSpeed = 0;
            rb2d.velocity = Vector3.zero;
            Invoke("SetToStart", 2);
            instance.lives--;
            smoke.Play();

            if (instance.lives == 2)
            {
                Destroy(life2);
            }
            if (instance.lives == 1)
            {
                Destroy(life);
            }
            if (instance.lives == 0)
            {
                Invoke("Destruction", 2);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary" && transported == false)
        {
            transported = true;
            StartCoroutine(ResetTransported());
        }
    }

    void SetToStart()
    {
        animator.SetBool("IsDead", false);
        transform.position = Vector3.zero;
        speed = 4;
        rotateSpeed = 5;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        rb2d.velocity = Vector3.zero;
    }

    void ResetInvincibility()
    {
        invincible = false;
    }

    IEnumerator ResetTransported()
    {
        yield return new WaitForSeconds(1);
        transported = false;
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
