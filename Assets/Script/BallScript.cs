using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = new Vector3(-7.0f, 6.0f, 0.0f);
    }
    void Update()
    {

    }
}
