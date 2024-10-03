using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    Rigidbody rigid;
    float jump_speed = 8.0f; // ジャンプの度合い

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // マウス左ボタンが押されたら
        if (Input.GetMouseButtonDown(0))
        {
            // ジャンプをする
            rigid.velocity = Vector3.up * jump_speed;
        }
    }
}