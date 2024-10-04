﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top2PlayerScript : MonoBehaviour
{
    #region prop
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    private Camera mainCamera;
    private CharacterController controller;
    private Vector3 movement;

    public VariableJoystick joystick;
    Vector3 moveDirection;
    #endregion

    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputProcess();
        // 入力の取得

        if (joystick.Direction != Vector2.zero)
        {
            moveDirection = joystick.Direction.normalized;
        }
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
            // 移動方向を向く（オプション：必要に応じてコメントアウト可能）
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

            // 移動の適用（現在の位置を維持）
            Vector3 motion = movement * moveSpeed * Time.deltaTime;
            controller.Move(motion);
            controller.Move(moveDirection);

        }

        // 重力の適用（必要に応じて）
        controller.Move(Physics.gravity * Time.deltaTime);


    }

    // 入力受付
    void InputProcess()
    {
        // 入力を正規化して受け取る
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // 入力を正規化する
        moveDirection = new Vector2(x, y).normalized;
    }
}