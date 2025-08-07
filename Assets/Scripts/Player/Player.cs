using System;
using UnityEngine;

// Player�� ���õ� ����� ��Ƶδ� ��.
// �̰��� ���� ��ɿ� ���� ����. (ex.CharacterManager.Instance.Player.controller)
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equip;

    public ItemData itemData; // ��ȣ�ۿ� ������ ������
    public Action addItem; // ������ �߰� �̺�Ʈ

    public Transform dropPosition; // ������ ���� �� �ʿ��� ��ġ

    private void Awake()
    {
        // ĳ���͸Ŵ����� Player get set �Լ��� �ڽ��� �Ҵ��ؼ� �ű⼭ '_player' ���� Ȱ�� �� �� �ֵ��� ��
        // Player(class) -> Player(Method) -> return _player ����
        CharacterManager.Instance.Player = this;

        // ��������Ʈ�� ���� ������Ʈ�� �پ��ִ� 'PlayerController'�� ã�Ƽ� controller��� ������ �Ҵ�
        // �̷��� �ϸ� ���� PlayerController ��ũ��Ʈ�� �߰����� �ʾƵ� Player ������Ʈ��
        // PlayerController�� �پ��ִٸ� Awake ������ �ڵ����� �߰���
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }
}