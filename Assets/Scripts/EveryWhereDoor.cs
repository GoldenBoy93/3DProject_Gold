using UnityEngine;

public class EveryWhereDoor : MonoBehaviour
{
    private Vector3 Point1; // 위치1
    private Vector3 Point2; // 위치2
    private Quaternion point1Rotation;
    private Quaternion point2Rotation;

    private void Awake()
    {
        Point1 = transform.position; // EveryWhereDoor의 처음 위치를 할당
        Point2 = new Vector3(-15, 0, -48); // EveryWhereDoor의 위치2를 할당

        // 회전값 할당 (예: Y축으로 90도 회전)
        point1Rotation = Quaternion.Euler(0, 90, 0);

        // 회전값 할당 (예: Y축으로 90도 회전)
        point2Rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인 (태그 사용)
        if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어를 MovingPad의 자식으로 만들기
            other.transform.parent = transform;

            Warp();

            other.transform.parent = null;
        }
    }

    void Warp()
    {
        if (transform.position == Point1)
        {
            // 현재 위치가 위치1이라면 위치2로 이동
            transform.position = Point2;
            transform.rotation = point2Rotation; // 위치2로 이동 시 회전값도 설정
        }
        else if (transform.position != Point1)
        {
            // 현재 위치가 위치2라면 위치1로 이동
            transform.position = Point1;
            transform.rotation = point1Rotation; // 위치1로 이동 시 회전값을 초기화
        }
    }
}
