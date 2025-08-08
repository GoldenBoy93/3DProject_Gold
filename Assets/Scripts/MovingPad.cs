using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 movementVector2;  // 패드 이동방향

    private Rigidbody rigidbody;

    private void Awake()
    {
        // 위에서 선언한 rigidbody 변수를 현재 오브젝트에 붙어있는 Rigidbody 컴포넌트를 가져와 할당
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // '패드' 이동 함수
    private void Move()
    {
        // 앞뒤로만 움직이게 할 함수
        Vector3 dir = transform.forward * movementVector2.y;

        dir *= moveSpeed;  // 방향에 속력을 곱해준다.

        dir.y = rigidbody.velocity.y;  // y값은 점프시 제외하고는 변화가 없어야하기에 기본 velocity(변화량)의 y 값을 넣어준다.

        rigidbody.velocity = dir;  // 연산된 속도를 velocity(변화량)에 넣어준다.
    }

    // 충돌된 객체가 PlayerController를 상속받고 있으면 점프
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인 (태그 사용)
        if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어를 MovingPad의 자식으로 만들기
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // 충돌이 끝난 오브젝트가 플레이어인지 확인
        if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어 상속을 해제
            other.transform.parent = null;
        }
    }
}
