using Unity.VisualScripting;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    public float speed = 1f;
    public float targetPointDistance = 1f;
    private Vector3 startPoint;
    bool movingForward = true;

    private void Awake()
    {
        startPoint = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // targetPoint는 나아가는중이라면 startPoint에서 targetPointDistance만큼 왼쪽으로 이동한 위치, 아니라면 startPoint 위치
        Vector3 targetPoint = movingForward ? startPoint - Vector3.right * targetPointDistance : startPoint;

        // 현재 위치와 타겟포인트의 거리가 0.1 미만이라면 false로 설정
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            // 현재상태의 반대로
            movingForward = !movingForward;
            Debug.Log("MovingPad 상태 변경: " + (movingForward ? "나아가는중" : "되돌아가는중"));
        }

        // 나아가는중이라면 왼쪽으로 이동
        if (movingForward)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        // 나아가는중이 아니라면 오른쪽으로 이동
        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
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
