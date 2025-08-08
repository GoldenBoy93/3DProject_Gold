using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 movementVector2;  // �е� �̵�����

    private Rigidbody rigidbody;

    private void Awake()
    {
        // ������ ������ rigidbody ������ ���� ������Ʈ�� �پ��ִ� Rigidbody ������Ʈ�� ������ �Ҵ�
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // '�е�' �̵� �Լ�
    private void Move()
    {
        // �յڷθ� �����̰� �� �Լ�
        Vector3 dir = transform.forward * movementVector2.y;

        dir *= moveSpeed;  // ���⿡ �ӷ��� �����ش�.

        dir.y = rigidbody.velocity.y;  // y���� ������ �����ϰ�� ��ȭ�� ������ϱ⿡ �⺻ velocity(��ȭ��)�� y ���� �־��ش�.

        rigidbody.velocity = dir;  // ����� �ӵ��� velocity(��ȭ��)�� �־��ش�.
    }

    // �浹�� ��ü�� PlayerController�� ��ӹް� ������ ����
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
