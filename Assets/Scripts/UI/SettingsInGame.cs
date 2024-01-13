using UI;
using UnityEngine;
using UnityEngine.UI;

public class SettingsInGame : MonoBehaviour
{
    [SerializeField] private GameObject slowMo = null;
    [SerializeField] private GameObject punch = null;
    [Space]
    [SerializeField] private GameObject settingsMenu = null;
    [SerializeField] private Button pause = null;

    [Space]
    [SerializeField] private Toggle mute = null;
    [SerializeField] private Slider volume = null;
    [SerializeField] private Button closeSettings = null;
    [SerializeField] private Button returnInMenu = null;
    [SerializeField] private Button restartGame = null;
    
    private void Awake()
    {
        volume.onValueChanged.AddListener(SetVolume);
        mute.onValueChanged.AddListener(MuteGame);
        closeSettings.onClick.AddListener(CloseSettings);
        returnInMenu.onClick.AddListener(ReturnInMenu);
        restartGame.onClick.AddListener(Restart);
        LevelRestartController.Instance.SetSettings(this);
    }
    
    private void SetVolume(float value)
    {
        GameSettings.instance.Volume = (int)value;
    }

    private void MuteGame(bool value)
    {
        GameSettings.instance.Mute = value; 
    }

    private void CloseSettings()
    {
        settingsMenu.SetActive (false);
        pause.gameObject.SetActive(true);
        slowMo.gameObject.GetComponent<SlowMo>().enabled = true;
        Time.timeScale = 1f;
    }
    
    public void ShowSetings()
    {
        settingsMenu.SetActive (false);
        pause.gameObject.SetActive(false);
        slowMo.gameObject.GetComponent<SlowMo>().enabled = false;
        Time.timeScale = 0f;
    }
    
    private void ReturnInMenu()
    {
        LevelRestartController.Instance.ShowMenu();   
    }

    private void Restart()
    {
        CloseSettings();
        LevelRestartController.Instance.Restart();
        punch.gameObject.GetComponent<Punch>().firstTouch = true;
    }
}