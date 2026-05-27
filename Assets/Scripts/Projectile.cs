using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    public GameObject bulletExplosion;
    public int score = 10;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];   // 총알과의 충돌지점 
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);   // 충돌지점의 방향
        Instantiate(bulletExplosion, contact.point, rotation); // 충돌후 폭파(instantiateExplosion) 오브젝트 생성
        if (collision.gameObject.tag == "NPC")
        {
           GameController.instance.AddScore(score);
            Destroy(collision.gameObject); // NPC 오브젝트 제거
            Destroy(gameObject);
        }
        else Destroy(gameObject); // 총알 오브젝트 제거
    }
}
