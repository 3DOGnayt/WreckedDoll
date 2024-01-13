using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject slowMo = null;
    [SerializeField] private Button pause = null;
    [SerializeField] private GameObject settingsInGame = null;

    private void Awake()
    {
        pause.onClick.AddListener(PauseInGame);
    }

    private void PauseInGame()
    {
        slowMo.gameObject.GetComponent<SlowMo>().enabled = false;
        Time.timeScale = 0f;
        settingsInGame.SetActive(true);
        pause.gameObject.SetActive(false);
    }
}