using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject changeLevelMenu = null;

    [Space]
    [SerializeField] private Button close = null;
    
    private void Awake()
    {
        close.onClick.AddListener(CloseSettings);
    }

    private void CloseSettings()
    {
        changeLevelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}