﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyPlayerScp : MonoBehaviour
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
        // 入力の処理
        ProcessInput();

        // 移動方向の計算
        CalculateMovement();

        // キャラクターの移動と回転
        MoveAndRotateCharacter();

        // 重力の適用
        ApplyGravity();
    }

    void ProcessInput()
    {
        // ジョイスティック入力の処理
        Vector2 joystickInput = joystick.Direction;

        // キーボード入力の処理
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ジョイスティック入力とキーボード入力を組み合わせる
        moveDirection = new Vector3(joystickInput.x + horizontalInput, 0, joystickInput.y + verticalInput).normalized;
    }

    void CalculateMovement()
    {
        // カメラの向きを基準にした移動方向の計算
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        // 移動ベクトルの計算
        movement = (forward * moveDirection.z + right * moveDirection.x).normalized;
    }

    void MoveAndRotateCharacter()
    {
        if (movement.magnitude > 0.1f)
        {
            // 移動方向を向く
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

            // 移動の適用
            Vector3 motion = movement * moveSpeed * Time.deltaTime;
            controller.Move(motion);
        }
    }

    void ApplyGravity()
    {
        controller.Move(Physics.gravity * Time.deltaTime);
    }
}