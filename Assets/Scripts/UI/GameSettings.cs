using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private bool mute;
    [SerializeField] private int volume;

    public bool Mute { get => mute; set => mute = value; }
    public int Volume { get => volume; set => volume = value; }

    public static GameSettings instance;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}