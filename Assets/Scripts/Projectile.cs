using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    [HideInInspector]
    public GameObject ship;
    [HideInInspector]
    public bool transported;

    //public GameObject Spaceship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipController>();

    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        transported = false;

        StartCoroutine(SelfDestruct());
    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics2D.IgnoreCollision(ship.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        GameObject[] Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject Bullet in Bullets)
        {
            Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    IEnumerator ResetTransported()
    {
        yield return new WaitForSeconds(2);
        transported = false;
    }
}
