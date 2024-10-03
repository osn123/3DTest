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
        // ���͂̎擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �J�����̌�������ɂ����ړ������̌v�Z
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        // �ړ��x�N�g���̌v�Z
        movement = (forward * verticalInput + right * horizontalInput).normalized;

        // �L�����N�^�[�̈ړ�
        if (movement.magnitude > 0.1f)
        {
            // �ړ�����������
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

            // �ړ��̓K�p
            controller.Move(movement * moveSpeed * Time.deltaTime);
        }
    }
}