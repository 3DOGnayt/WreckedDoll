using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LavelNextController : MonoBehaviour
{
    [SerializeField] private Button nextLevel = null;
    [Space]
    [SerializeField] private int sceneIndex;
    [SerializeField] private int finalSceneComplete;

    private void Awake()
    {
        nextLevel.onClick.AddListener(NextLevel);
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        finalSceneComplete = 0;

        if (PlayerPrefs.HasKey("LastLevelCompleted"))
        {
            finalSceneComplete = 1;
        }     

        if (sceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            finalSceneComplete = 1;
            PlayerPrefs.SetInt("LastLevelCompleted", finalSceneComplete);
        }
    }

    private void NextLevel()
    {
        if (sceneIndex == (SceneManager.sceneCountInBuildSettings - 1) || finalSceneComplete == 1)
        {
            SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings - 1));
        }
        else
        SceneManager.LoadScene(sceneIndex + 1);
    }
}