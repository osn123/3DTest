using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;
    public float scaleSpeed = 0.1f;

    private Transform modelTransform;

    void Start()
    {
        // FBXモデルのTransformコンポーネントを取得
        modelTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // 移動
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        modelTransform.Translate(movement);

        // 回転
        if (Input.GetKey(KeyCode.Q))
        {
            modelTransform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            modelTransform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        // スケーリング
        if (Input.GetKey(KeyCode.Z))
        {
            modelTransform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.X))
        {
            modelTransform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}