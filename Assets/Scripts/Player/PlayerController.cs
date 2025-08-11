using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;  // 현재 입력 값
    public float dashSpeed;
    public float useStamina;  // 대시 시 소모되는 스태미너
    public bool isDash;  // 대시 여부
    public float jumpPower;
    public LayerMask groundLayerMask;  // 레이어 정보

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;  // 최소 시야각
    public float maxXLook;  // 최대 시야각
    private float camCurXRot;
    public float lookSensitivity; // 카메라 민감도

    private Vector2 mouseDelta;  // 마우스 변화값

    [HideInInspector]
    public bool canLook = true;

    public Action inventory;  // 인벤토리 열기/닫기 Action
    // Rigidbody를 rigidbody로 선언해서 변수명을 활용할 수 있도록함.
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        // 위에서 선언한 rigidbody 변수를 현재 오브젝트에 붙어있는 Rigidbody 컴포넌트를 가져와 할당
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 커서 잠그고 숨기기
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 물리 연산
    private void FixedUpdate()
    {
        Move();
    }

    // 카메라 연산 -> 모든 연산이 끝나고 카메라 움직임
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // 입력값 처리
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 입력값 처리
    // 'context'는 InputAction의 상태를 받음
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Performed는 InputAction이 완료되었을 때를 말함.
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
        // 땅에 있을때만 이동 가능
        if (IsGrounded())
        {
            // 현재 입력의 y 값은 z 축(=forward, 앞뒤)에 곱한다.
            // -> y입력값은 2D에선 앞뒤 이동이 맞지만 3D에서는 z축이 앞뒤 이동이므로 치환이 필요
            // 현재 입력의 x 값은 x 축(=right, 좌우)에 곱한다.
            Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

            // 대시 여부 + 보유스태미너 에 따라 속도 적용
            if (isDash == true && GameManager.Instance.Player.condition.UseStamina(useStamina))
            {
                dir *= dashSpeed;  // 방향에 Dash속력을 곱해준다.
            }
            else
            {
                dir *= moveSpeed;  // 방향에 속력을 곱해준다.
            }

            dir.y = playerRigidbody.velocity.y;  // y값은 점프시 제외하고는 변화가 없어야하기에 기본 velocity(변화량)의 y 값을 넣어준다.

            playerRigidbody.velocity = dir;  // 연산된 속도를 velocity(변화량)에 넣어준다.
        }
    }

    // 속도증가량, 증가시간 받아옴
    public void SpeedBoost(float amount, float Second)
    {
        moveSpeed += amount;  // amount만큼 속도 증가

        // Second초 후 타임오버
        StartCoroutine(SpeedCountdownSecond(Second, amount));
    }

    public IEnumerator SpeedCountdownSecond(float Second, float amount)
    {
        yield return new WaitForSeconds(Second);

        // 시간이 지나면 무브스피드의 증가속도만큼 감소
        moveSpeed -= amount;
    }

    void CameraLook()
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        // 카메라가 위 아래로 회전하려면 rotation의 x 값에 넣어준다. -> 실습으로 확인
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        // 4개의 Ray를 만든다.
        // 플레이어(transform)을 기준으로 앞뒤좌우 0.2씩 떨어뜨려서.
        // 0.01 정도 살짝 위로 올린다.
        // 하이라이트 부분의 차이점과 그 외 부분을 나눠서 분석해보세요.
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        // 4개의 Ray 중 groundLayerMask에 해당하는 오브젝트가 충돌했는지 조회한다.
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
        // 인벤토리 열기/닫기
        if (context.phase == InputActionPhase.Started)
        {
            // inventory가 null이 아니라면, 포함된 모든 함수를 실행
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