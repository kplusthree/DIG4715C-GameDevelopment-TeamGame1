using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    public AudioClip AsteroidLarge;
    public AudioClip AsteroidMed;
    public AudioClip AsteroidSmall;
    public AudioClip UFO;
    public ParticleSystem particle;
    [HideInInspector]
    public bool stopEnemy;
    [HideInInspector]
    public AudioSource soundSource;
    [HideInInspector]
    public AudioSource UFOSource;
    [HideInInspector]
    public bool transported;
    [HideInInspector]
    public bool invincible;

    public float rotationSpeed;
    public float speed;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        UFOSource = GameObject.FindGameObjectWithTag("UFONoise").GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        transported = false;
        stopEnemy = false;
        invincible = false;

        if (gameObject.tag != "UFO")
        {
            rigidbody2d.velocity = Random.onUnitSphere * speed;
        }
        else
        {
            UFOSource.clip = UFO;
            UFOSource.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotate = new Vector3(0, 0, rotationSpeed);
        transform.Rotate(rotate);

        if (invincible == true)
        {
            GameObject[] Bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject Bullet in Bullets)
            {
                Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

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

        if (gameObject.tag == "UFO" && stopEnemy == false)
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameController instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();

       if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Big")
        {
            invincible = true;
            stopEnemy = true;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.Sleep();
            particle.Play();
            GameObject Big = collision.gameObject;
            Destroy(Big);
            soundSource.clip = AsteroidLarge;
            soundSource.Play();
            instance.AddPoints(20);
            Vector3 location = transform.position;
            instance.BreakUp(location, 1);
            instance.astCounter++;
            Invoke("Destruction", 1);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Medium")
        {
            invincible = true;
            stopEnemy = true;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.Sleep();
            particle.Play();
            GameObject Medium = collision.gameObject;
            Destroy(Medium);
            soundSource.clip = AsteroidMed;
            soundSource.Play();
            instance.AddPoints(50);
            Vector3 location = transform.position;
            instance.BreakUp(location, 2);
            instance.astCounter++;
            Invoke("Destruction", 1);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Small")
        {
            invincible = true;
            stopEnemy = true;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.Sleep();
            particle.Play();
            GameObject Small = collision.gameObject;
            Destroy(Small);
            soundSource.clip = AsteroidSmall;
            soundSource.Play();
            instance.AddPoints(100);
            Vector3 location = transform.position;
            instance.astCounter++;
            Invoke("Destruction", 1);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "UFO")
        {
            invincible = true;
            animator.SetBool("IsDead", true);
            stopEnemy = true;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.Sleep();
            particle.Play();
            GameObject UFO = collision.gameObject;
            Destroy(UFO);
            soundSource.clip = AsteroidMed;
            soundSource.Play();
            UFOSource.Stop();
            instance.AddPoints(200);
            Vector3 location = transform.position;
            Invoke("Destruction", 2);
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

    IEnumerator ResetTransported()
    {
        yield return new WaitForSeconds(3);
        transported = false;
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}