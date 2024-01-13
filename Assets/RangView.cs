using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;

    public TMP_Text Text => text;
    public Image Image => image;
}