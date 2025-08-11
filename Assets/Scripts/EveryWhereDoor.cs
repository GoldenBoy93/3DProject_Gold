using UnityEngine;

public class EveryWhereDoor : MonoBehaviour
{
    private Vector3 Point1; // ��ġ1
    private Vector3 Point2; // ��ġ2
    private Quaternion point1Rotation;
    private Quaternion point2Rotation;

    private void Awake()
    {
        Point1 = transform.position; // EveryWhereDoor�� ó�� ��ġ�� �Ҵ�
        Point2 = new Vector3(-15, 0, -48); // EveryWhereDoor�� ��ġ2�� �Ҵ�

        // ȸ���� �Ҵ� (��: Y������ 90�� ȸ��)
        point1Rotation = Quaternion.Euler(0, 90, 0);

        // ȸ���� �Ҵ� (��: Y������ 90�� ȸ��)
        point2Rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ�� (�±� ���)
        if (other.gameObject.CompareTag("Player"))
        {
            // �÷��̾ MovingPad�� �ڽ����� �����
            other.transform.parent = transform;

            Warp();

            other.transform.parent = null;
        }
    }

    void Warp()
    {
        if (transform.position == Point1)
        {
            // ���� ��ġ�� ��ġ1�̶�� ��ġ2�� �̵�
            transform.position = Point2;
            transform.rotation = point2Rotation; // ��ġ2�� �̵� �� ȸ������ ����
        }
        else if (transform.position != Point1)
        {
            // ���� ��ġ�� ��ġ2��� ��ġ1�� �̵�
            transform.position = Point1;
            transform.rotation = point1Rotation; // ��ġ1�� �̵� �� ȸ������ �ʱ�ȭ
        }
    }
}
