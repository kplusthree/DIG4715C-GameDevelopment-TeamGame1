using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public bool isTransported;

    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            isTransported = col.gameObject.GetComponent<Projectile>().transported;
            if (isTransported == true)
            {
                Vector3 location = col.transform.position;
                col.transform.position = new Vector3(-location.x, -location.y, location.z);
            }
        }
        else if (col.gameObject.tag == "Ship")
        {
            isTransported = col.gameObject.GetComponent<ShipController>().transported;
            if (isTransported == true)
            {
                Vector3 location = col.transform.position;
                col.transform.position = new Vector3(-location.x, -location.y, location.z);
            }
        }
        else if (col.gameObject.tag == "Big" || col.gameObject.tag == "Medium" || col.gameObject.tag == "Small" || col.gameObject.tag == "UFO")
        {
            isTransported = col.gameObject.GetComponent<Asteroid>().transported;
            if (isTransported == true)
            {
                rigidbody2d = col.gameObject.GetComponent<Rigidbody2D>();
                Vector3 location = col.transform.position;
                col.transform.position = new Vector3(-location.x, -location.y, location.z);
            }
        }
    }
}
