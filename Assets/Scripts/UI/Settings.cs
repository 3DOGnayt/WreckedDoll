using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject settingsMenu = null;

    [Space]
    [SerializeField] private Toggle mute = null;
    [SerializeField] private Slider volume = null;

    [Space]
    [SerializeField] private Button close = null;
    
    private void Awake()
    {
        volume.onValueChanged.AddListener(SetVolume);        
        mute.onValueChanged.AddListener(MuteGame);
        close.onClick.AddListener(CloseSettings);
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
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}