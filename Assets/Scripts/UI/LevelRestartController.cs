using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelRestartController : MonoBehaviour
    {
        private SettingsInGame _settingsInGame;
        private Menu _menu;
        private static LevelRestartController _instance;
        private bool _isToShowMenuOnLoad = true;

        public void SetSettings(SettingsInGame settingsInGame) => _settingsInGame = settingsInGame;

        public void SetMenu(Menu menu) => _menu = menu;

        public void LoadMenu()
        {
            if (!_isToShowMenuOnLoad)
                _menu.StartGame();
            else
            {
                _menu.gameObject.SetActive(true);
                _settingsInGame.ShowSetings();
            }
        }

        public static LevelRestartController Instance => _instance
            ? _instance
            : (_instance = new GameObject("LevelRestartController").AddComponent<LevelRestartController>());        

        public void Restart()
        {
            _isToShowMenuOnLoad = false;
            LoadCurrentScene();
        }

        public void LoadSceneById(int id)
        {
            SceneManager.LoadScene(id);
        }

        private void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

        public void ShowMenu()
        {
            _isToShowMenuOnLoad = true;
            LoadCurrentScene();
        }
    }
}