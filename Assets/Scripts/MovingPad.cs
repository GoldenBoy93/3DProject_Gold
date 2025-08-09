using Unity.VisualScripting;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    public float speed = 1f; // MovingPad�� �̵� �ӵ�
    public float targetPointDistance = 1f; // Ÿ�� ����Ʈ������ �Ÿ�
    private Vector3 startPoint; // ó�� ��ġ
    bool movingForward = true;  // ���� MovingPad�� ���ư��� ������ ����

    private void Awake()
    {
        startPoint = transform.position; // MovingPad�� ó�� ��ġ�� �Ҵ�
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // targetPoint�� ���ư������̶�� startPoint���� targetPointDistance��ŭ �������� �̵��� ��ġ, �ƴ϶�� startPoint ��ġ
        Vector3 targetPoint = movingForward ? startPoint - Vector3.right * targetPointDistance : startPoint;

        // ���� ��ġ�� Ÿ������Ʈ�� �Ÿ��� 0.1 �̸��̶�� false�� ����
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            // ��������� �ݴ��
            movingForward = !movingForward;
            Debug.Log("MovingPad ���� ����: " + (movingForward ? "���ư�����" : "�ǵ��ư�����"));
        }

        // ���ư������̶�� �������� �̵�
        if (movingForward)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        // ���ư������� �ƴ϶�� ���������� �̵�
        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ�� (�±� ���)
        if (other.gameObject.CompareTag("Player"))
        {
            // �÷��̾ MovingPad�� �ڽ����� �����
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // �浹�� ���� ������Ʈ�� �÷��̾����� Ȯ��
        if (other.gameObject.CompareTag("Player"))
        {
            // �÷��̾� ����� ����
            other.transform.parent = null;
        }
    }
}
