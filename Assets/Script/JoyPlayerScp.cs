using System.Collections;
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
        // ���͂̏���
        ProcessInput();

        // �ړ������̌v�Z
        CalculateMovement();

        // �L�����N�^�[�̈ړ��Ɖ�]
        MoveAndRotateCharacter();

        // �d�͂̓K�p
        ApplyGravity();
    }

    void ProcessInput()
    {
        // �W���C�X�e�B�b�N���͂̏���
        Vector2 joystickInput = joystick.Direction;

        // �L�[�{�[�h���͂̏���
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �W���C�X�e�B�b�N���͂ƃL�[�{�[�h���͂�g�ݍ��킹��
        moveDirection = new Vector3(joystickInput.x + horizontalInput, 0, joystickInput.y + verticalInput).normalized;
    }

    void CalculateMovement()
    {
        // �J�����̌�������ɂ����ړ������̌v�Z
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        // �ړ��x�N�g���̌v�Z
        movement = (forward * moveDirection.z + right * moveDirection.x).normalized;
    }

    void MoveAndRotateCharacter()
    {
        if (movement.magnitude > 0.1f)
        {
            // �ړ�����������
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

            // �ړ��̓K�p
            Vector3 motion = movement * moveSpeed * Time.deltaTime;
            controller.Move(motion);
        }
    }

    void ApplyGravity()
    {
        controller.Move(Physics.gravity * Time.deltaTime);
    }
}