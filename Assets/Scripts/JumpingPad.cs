using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad ���� �Ŀ�


    // �浹�� ��ü�� PlayerController�� ��ӹް� ������ ����
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRb = other.GetComponent<Rigidbody>();

        playerRb.AddForce(Vector2.up * padJumpPower, ForceMode.Impulse);
    }
}
