using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishView : MonoBehaviour
    {
        [SerializeField] private Button restartLevel;
        [SerializeField] private Button nextLevel;
        [SerializeField] private RangView resultText;
        [Space]
        [SerializeField] private new Transform transform;

        public Button RestartLevel => restartLevel;
        public Button NextLevel => nextLevel;
        public RangView ResultText => resultText;

        public void SpawnResult(List<(PartsName, int)> reslist)
        {
            foreach (var result in reslist)
            {
                var a = Instantiate(resultText, transform);
                a.Image.sprite = result.Item1.Name;
                a.Text.text =$"{result.Item2}";                
            }
        }
    }
}