using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;  // ���� �Է� ��
    public float dashSpeed;
    public float useStamina;  // ��� �� �Ҹ�Ǵ� ���¹̳�
    public bool isDash;  // ��� ����
    public float jumpPower;
    public LayerMask groundLayerMask;  // ���̾� ����

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;  // �ּ� �þ߰�
    public float maxXLook;  // �ִ� �þ߰�
    private float camCurXRot;
    public float lookSensitivity; // ī�޶� �ΰ���

    private Vector2 mouseDelta;  // ���콺 ��ȭ��

    [HideInInspector]
    public bool canLook = true;

    public Action inventory;  // �κ��丮 ����/�ݱ� Action
    // Rigidbody�� rigidbody�� �����ؼ� �������� Ȱ���� �� �ֵ�����.
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        // ������ ������ rigidbody ������ ���� ������Ʈ�� �پ��ִ� Rigidbody ������Ʈ�� ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Ŀ�� ��װ� �����
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ���� ����
    private void FixedUpdate()
    {
        Move();
    }

    // ī�޶� ���� -> ��� ������ ������ ī�޶� ������
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // �Է°� ó��
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // �Է°� ó��
    // 'context'�� InputAction�� ���¸� ����
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Performed�� InputAction�� �Ϸ�Ǿ��� ���� ����.
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started
            && IsGrounded()
            && GameManager.Instance.Player.condition.UseStamina(useStamina))
        {
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isDash = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDash = false;
        }
    }

    private void Move()
    {
        // ���� �������� �̵� ����
        if (IsGrounded())
        {
            // ���� �Է��� y ���� z ��(=forward, �յ�)�� ���Ѵ�.
            // -> y�Է°��� 2D���� �յ� �̵��� ������ 3D������ z���� �յ� �̵��̹Ƿ� ġȯ�� �ʿ�
            // ���� �Է��� x ���� x ��(=right, �¿�)�� ���Ѵ�.
            Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

            // ��� ���� + �������¹̳� �� ���� �ӵ� ����
            if (isDash == true && GameManager.Instance.Player.condition.UseStamina(useStamina))
            {
                dir *= dashSpeed;  // ���⿡ Dash�ӷ��� �����ش�.
            }
            else
            {
                dir *= moveSpeed;  // ���⿡ �ӷ��� �����ش�.
            }

            dir.y = playerRigidbody.velocity.y;  // y���� ������ �����ϰ�� ��ȭ�� ������ϱ⿡ �⺻ velocity(��ȭ��)�� y ���� �־��ش�.

            playerRigidbody.velocity = dir;  // ����� �ӵ��� velocity(��ȭ��)�� �־��ش�.
        }
    }

    // �ӵ�������, �����ð� �޾ƿ�
    public void SpeedBoost(float amount, float Second)
    {
        moveSpeed += amount;  // amount��ŭ �ӵ� ����

        // Second�� �� Ÿ�ӿ���
        StartCoroutine(SpeedCountdownSecond(Second, amount));
    }

    public IEnumerator SpeedCountdownSecond(float Second, float amount)
    {
        yield return new WaitForSeconds(Second);

        // �ð��� ������ ���꽺�ǵ��� �����ӵ���ŭ ����
        moveSpeed -= amount;
    }

    void CameraLook()
    {
        // ���콺 �������� ��ȭ��(mouseDelta)�� y(�� �Ʒ�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �� �Ʒ��� ȸ���Ϸ��� rotation�� x ���� �־��ش�. -> �ǽ����� Ȯ��
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // ���콺 �������� ��ȭ��(mouseDelta)�� x(�¿�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �¿�� ȸ���Ϸ��� rotation�� y ���� �־��ش�. -> �ǽ����� Ȯ��
        // �¿� ȸ���� �÷��̾�(transform)�� ȸ�������ش�.
        // Why? ȸ����Ų ������ �������� �յ��¿� ���������ϴϱ�.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        // 4���� Ray�� �����.
        // �÷��̾�(transform)�� �������� �յ��¿� 0.2�� ����߷���.
        // 0.01 ���� ��¦ ���� �ø���.
        // ���̶���Ʈ �κ��� �������� �� �� �κ��� ������ �м��غ�����.
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        // 4���� Ray �� groundLayerMask�� �ش��ϴ� ������Ʈ�� �浹�ߴ��� ��ȸ�Ѵ�.
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        // �κ��丮 ����/�ݱ�
        if (context.phase == InputActionPhase.Started)
        {
            // inventory�� null�� �ƴ϶��, ���Ե� ��� �Լ��� ����
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;

        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}