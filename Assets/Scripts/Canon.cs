using System;
using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public int shootPower; // �߻� �Ŀ�

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� �ݶ��̴��� ������ٵ� ������ ������
        Rigidbody Rb = other.GetComponent<Rigidbody>();

        StartCoroutine(Shoot(Rb));
    }

    IEnumerator Shoot(Rigidbody Rb)
    {
        // 00�� ���
        yield return new WaitForSeconds(2f);

        // ������ٵ� ��������
        Rb.AddForce(new Vector3(1, 1, 0) * shootPower, ForceMode.Impulse);
    }
}
