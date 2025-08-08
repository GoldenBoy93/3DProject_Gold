using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad ���� �Ŀ�
    public float padJumpRate; // JumpingPad ���� ��

    private List<IJumpable> things = new List<IJumpable>();

    private void Start()
    {
        InvokeRepeating("PadJump", 0, padJumpRate);
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
        Debug.Log(other.name + " on JumpingPad");

        // �浹�� ��ü�� IDamagable�� ��ӵǾ� ������ List�� �߰�
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            Debug.Log("Jumpable detected: " + other.name);
            things.Add(jumpable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit �Ǵ� ��ü�� IDamagable�� ��ӵǾ� ������ List���� ����
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            things.Remove(jumpable);
        }
    }
}
