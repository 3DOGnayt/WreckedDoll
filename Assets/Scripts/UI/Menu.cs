using UI;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject cameraMain = null;
    [SerializeField] private GameObject punch = null;
    [SerializeField] private GameObject score = null;

    [Space]
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject settingsMenu = null;
    [SerializeField] private GameObject changeLevelMenu = null;

    [Space]
    [SerializeField] private Button startGame = null;
    [SerializeField] private Button showSettings = null;
    [SerializeField] private Button changeLevel = null;
    [SerializeField] private Button pause = null;
    [SerializeField] private SettingsInGame _settingsInGame;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        showSettings.onClick.AddListener(ShowSettings);
        changeLevel.onClick.AddListener(ChangeLevel);
        LevelRestartController.Instance.SetMenu(this);
        LevelRestartController.Instance.SetSettings(_settingsInGame);
        LevelRestartController.Instance.LoadMenu();
    }

    private void Start()
    {
        cameraMain.gameObject.GetComponent<CameraRotateAround>().enabled = false;
        punch.gameObject.GetComponent<Punch>().enabled = false;
        score.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        cameraMain.gameObject.GetComponent<CameraRotateAround>().enabled = true;
        cameraMain.gameObject.GetComponent<SlowMo>().enabled = true;
        cameraMain.gameObject.GetComponent<SlowMo>().SimpleSlow = true;
        punch.gameObject.GetComponent<Punch>().enabled = true;
        mainMenu.SetActive(false);
        pause.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
    }

    private void ShowSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    private void ChangeLevel()
    {
        mainMenu.SetActive(false);
        changeLevelMenu.SetActive(true);
    }
}