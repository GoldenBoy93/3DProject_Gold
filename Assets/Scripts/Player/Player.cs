using System;
using UnityEngine;

// Player와 관련된 기능을 모아두는 곳.
// 이곳을 통해 기능에 각각 접근. (ex.CharacterManager.Instance.Player.controller)
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equip;

    public ItemData itemData; // 상호작용 아이템 데이터
    public Action addItem; // 아이템 추가 이벤트

    public Transform dropPosition; // 아이템 버릴 때 필요한 위치

    private void Awake()
    {
        // 캐릭터매니저의 Player get set 함수에 자신을 할당해서 거기서 '_player' 갖고 활용 할 수 있도록 함
        // Player(class) -> Player(Method) -> return _player 순서
        CharacterManager.Instance.Player = this;

        // 겟컴포넌트로 현재 오브젝트에 붙어있는 'PlayerController'를 찾아서 controller라는 변수에 할당
        // 이렇게 하면 따로 PlayerController 스크립트를 추가하지 않아도 Player 오브젝트에
        // PlayerController가 붙어있다면 Awake 시점에 자동으로 추가됨
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }
}