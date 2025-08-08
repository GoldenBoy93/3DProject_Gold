using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); // BaseUI���� UIManager ����

        // ��ư Ŭ�� �̺�Ʈ ���� (���̾��Ű�� �ִ� �̺�Ʈ�ý���)
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        // GameManager�� StartGame �޼��� ȣ��
        GameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        Application.Quit(); // ����� ���ø����̼� ���� (�����Ϳ����� �۵����� ����)
    }

    protected override UIState GetUIState()
    {
        return UIState.Intro;
    }
}