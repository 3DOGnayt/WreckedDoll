using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLevel : MonoBehaviour
{
    [SerializeField] private Button reset = null;
    [SerializeField] private Button load = null;
    [SerializeField] private Dropdown levelChoise;

    private int levelSave;

    private void Awake()
    {
        reset.onClick.AddListener(Reset);
        load.onClick.AddListener(LoadSpecificScene);
    }

    private void Start()
    {
        levelChoise.options = new List<Dropdown.OptionData>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings -1; i++)
        {
            levelChoise.options.Add(new Dropdown.OptionData($"Level {i}"));
        }
        levelSave = SceneManager.GetActiveScene().buildIndex;

        if (levelSave == 0 && PlayerPrefs.HasKey("LevelSave"))
        {
            Load();
            Debug.Log("l-0, s-1");
        }

        if (levelSave == 0 && !PlayerPrefs.HasKey("LevelSave") )
        {
            SceneManager.LoadScene(levelSave + 1);
            Debug.Log("l-0, s-0");
        }

        if (!PlayerPrefs.HasKey("LevelSave"))
        {            
            PlayerPrefs.SetInt("LevelSave", levelSave);
            Debug.Log("s");
        }
        
        if (PlayerPrefs.GetInt("LevelSave") < levelSave)
        {
            PlayerPrefs.SetInt("LevelSave", levelSave);
            Debug.Log("ns");
        }
    }

    private void LoadSpecificScene()
    {
        LevelRestartController.Instance.LoadSceneById(levelChoise.value + 1);
    }
    private void Load()
    {
        
        LevelRestartController.Instance.LoadSceneById(PlayerPrefs.GetInt("LevelSave"));
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}