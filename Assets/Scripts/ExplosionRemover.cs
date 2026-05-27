using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosionRemover : MonoBehaviour
{
    public float explosionTime = 1.0f;
    void Start()
    {
        // 폭파 파티클 1초후 제거
        Destroy(gameObject, explosionTime);
    }
}
