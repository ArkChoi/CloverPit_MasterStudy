using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public Rigidbody prefabBullet;
    public float bulletForce = 1000.0f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 총알 오브젝트(InstanceBullet) 생성   
            Rigidbody instanceBullet = Instantiate(prefabBullet, transform.position, transform.rotation) as Rigidbody;
            instanceBullet.AddForce(transform.forward * bulletForce);    // 총알 오브젝트 앞으로 발사하는 힘 추가
            Physics.IgnoreCollision(instanceBullet.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
            // Player의 Collider 와의 충돌 무시
        }
    }
}
