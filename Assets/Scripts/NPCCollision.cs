using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCCollision : MonoBehaviour
{
    public GameObject explosion; 
    public int healthValue = 10; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            GameController.instance.MinusHealth(healthValue);
            Destroy(gameObject);
           if (GameController.instance.health == 0) GameController.instance.GameOver();
        }
        else return;      // Floor 혹은 다른 물체와의 충돌 무시
    }
}
