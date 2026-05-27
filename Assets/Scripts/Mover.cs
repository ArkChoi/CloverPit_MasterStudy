using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mover : MonoBehaviour
{
    public float speed=-10;
    public float tumble=90;
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}
