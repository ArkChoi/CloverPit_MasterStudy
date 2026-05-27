using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    public GameObject explosion;
    public int healthValue = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            GameController.instance.MinusHealth(healthValue);
            if (GameController.instance.health == 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                GameController.instance.GameOver();
            }

        }
    }
}
