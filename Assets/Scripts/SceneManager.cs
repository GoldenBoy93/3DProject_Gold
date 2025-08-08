using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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