using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    private Camera mainCamera;
    private CharacterController controller;
    private Vector3 movement;

    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 入力の取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // カメラの向きを基準にした移動方向の計算
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        // 移動ベクトルの計算
        movement = (forward * verticalInput + right * horizontalInput).normalized;

        // キャラクターの移動
        if (movement.magnitude > 0.1f)
        {
            // 移動方向を向く
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

            // 移動の適用
            controller.Move(movement * moveSpeed * Time.deltaTime);
        }
    }
}