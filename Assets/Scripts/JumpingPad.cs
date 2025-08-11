using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad ���� �Ŀ�

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� �ݶ��̴��� ������ٵ� ������ ������
        Rigidbody Rb = other.GetComponent<Rigidbody>();

        // ������ٵ� ��������
        Rb.AddForce(Vector2.up * padJumpPower, ForceMode.Impulse);
    }
}
