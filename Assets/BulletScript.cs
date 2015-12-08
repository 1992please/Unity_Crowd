using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Invoke("destroyAftertime", .4f);
    }

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Problems")
        {
            other.GetComponent<animateScript>().loadDifferentAnimation(0, false);
            other.GetComponent<Collider2D>().enabled = false;
        }
        else if (other.tag != "Player")
        {
            Destroy(gameObject);
        }

    }
    void destroyAftertime()
    {
        Destroy(gameObject);
    }
}