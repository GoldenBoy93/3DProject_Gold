using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���¸� �����ϱ� ���� enum ����
public enum ChestState
{
    Closed,
    Open,
}

public class Chest : MonoBehaviour
{
    private ChestState state;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Open �����϶� �ִϸ������� PlayAnimation �Ķ���͸� true�� ����
        animator.SetBool("PlayAnimation", state == ChestState.Open);
    }

    public void SetState()
    {
        switch (state)
        {
            // ���� Oen ���¶�� Closed�� ����
            case ChestState.Open:
                state = ChestState.Closed;
                return;

            // ���� Closed ���¶�� Open���� ����
            case ChestState.Closed:
                state = ChestState.Open;
                return;
        }
    }
}