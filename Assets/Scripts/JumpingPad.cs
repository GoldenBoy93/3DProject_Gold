using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad ���� �Ŀ�


    // �浹�� ��ü�� PlayerController�� ��ӹް� ������ ����
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        playerController.HighJump(padJumpPower);
    }
}
