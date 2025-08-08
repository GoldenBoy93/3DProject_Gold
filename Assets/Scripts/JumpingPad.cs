using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad ���� �Ŀ�

    private List<IJumpable> things = new List<IJumpable>();

    private void FixedUpdate()
    {
        PadJump();
    }

    void PadJump()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].HighJump(padJumpPower);
        }
    }

    // �浹�� ��ü�� PlayerController�� ��ӹް� ������ ����
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� IJumpable ��ӵǾ� ������ List�� �߰�
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            things.Add(jumpable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit �Ǵ� ��ü�� IJumpable ��ӵǾ� ������ List���� ����
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            things.Remove(jumpable);
        }
    }
}
