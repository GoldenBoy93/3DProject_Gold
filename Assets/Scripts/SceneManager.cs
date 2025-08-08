using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // private로 직접 접근 제한
    private static SceneManager _instance;

    // public Instance 프로퍼티를 통해 외부에서 SceneManager에 접근할 수 있도록 함
    public static SceneManager Instance
    {
        get
        {
            // 할당되지 않았을 때, 외부에서 SceneManager.Instance 로 접근하는 경우
            // 게임 오브젝트를 만들어주고 SceneManager 스크립트를 AddComponent로 붙여준다.
            if (_instance == null)
            {
                // 게임오브젝트가 없어도 시작시 없는걸 확인후 매니저를 게임오브젝트로 생성
                _instance = new GameObject("SceneManager").AddComponent<SceneManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeToNextScene()
    {
        // 현재 씬 인덱스 할당
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // 다음 씬 인덱스 할당 (현재 씬 인덱스 + 1)
        int nextSceneIndex = currentSceneIndex + 1;

        // 만약 다음 씬 인덱스가 빌드 설정에서 정의된 씬의 개수를 초과하면, 첫 번째 씬으로 돌아감
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }
}