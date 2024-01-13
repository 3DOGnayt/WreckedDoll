using DG.Tweening;
using System.Collections;
using UI;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private FinishView finishUIPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Score score;
    [SerializeField] private SlowMo slowMo;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Ease ease;
    [SerializeField] private float delay;

    private FinishView finishUI;
    private bool isLevelEnded = false;

    private void Start()
    {
        finishUI = Instantiate(finishUIPrefab, canvas.transform);
        finishUI.RestartLevel.onClick.AddListener(() => LevelRestartController.Instance.Restart());
        finishUI.gameObject.SetActive(false);
        finishUI.transform.DOScale(0, 0);
    }

    public void LevelEnd()
    {
        if(isLevelEnded) return;
        isLevelEnded = true;
        slowMo.SlowFinish();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(delay);
        score.hitBar.SetActive(false);
        finishUI.NextLevel.interactable = score.isPassedLevel;
        Camera.main.nearClipPlane = 0.3f;
        finishUI.gameObject.SetActive(true);
        finishUI.SpawnResult(score.GetTopOfHits(3));
        
        finishUI.transform.DOScale(1, scaleDuration).SetEase(ease).SetUpdate(true);
    }
}