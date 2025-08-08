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
        base.Init(uiManager); // BaseUI에서 UIManager 저장

        // 버튼 클릭 이벤트 연결 (하이어라키에 있는 이벤트시스템)
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        // GameManager의 StartGame 메서드 호출
        GameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        Application.Quit(); // 빌드된 애플리케이션 종료 (에디터에서는 작동하지 않음)
    }

    protected override UIState GetUIState()
    {
        return UIState.Intro;
    }
}