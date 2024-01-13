using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct PartsName
{
    [SerializeField] private BodyPart _bodyPart;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int score;
    [SerializeField] private Sprite name;

    public BodyPart BodyPart => _bodyPart;

    public Sprite Sprite => sprite;

    public int Score => score;

    public Sprite Name => name;
}

public class Score : MonoBehaviour
{
    [SerializeField] private Image scorePrefab;
    [SerializeField] private float scaleDuration;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private List<PartsName> partsNames;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text popupScoreText;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;
    [SerializeField] private int maxScore;

    public float scrollSpeed = 10f;
    private Queue<BodyPart> hitQueue;
    private Queue<int> multiplies;
    private int score;
    private Dictionary<BodyPart, PartsName> _nameDictionary;
    private Dictionary<BodyPart, int> _countOfHits;
    private RectTransform _contentRectTransform;
    private RectTransform _rectTransform;
    public GameObject hitBar => scrollRect.gameObject;
    public bool isPassedLevel => score >= maxScore;

    public int ScoreValue => score;

    private bool _isCoroutinePlay;

    private void Start()
    {
        _countOfHits = new Dictionary<BodyPart, int>();
        FindObjectsOfType<HitReg>().ToList().ForEach(part =>
        {
            part.OnHit += (bodyPart, multyply) =>
            {
                HitPartScore(bodyPart, multyply);
                if (!_countOfHits.ContainsKey(bodyPart))
                    _countOfHits.Add(bodyPart, 1);
                else
                {
                    _countOfHits[bodyPart]++;
                }
            };
        });

        hitQueue = new Queue<BodyPart>();
        multiplies = new Queue<int>();
        _contentRectTransform = scrollRect.content;
        _rectTransform = scrollRect.viewport;
        _nameDictionary = partsNames.ToDictionary(x => x.BodyPart, x => x);
        score = 0;
        scoreText.text = $"{score}";
        popupScoreText.transform.DOScale(0f, 0f);        
    }
    
    private void HitPartScore(BodyPart bodyPart, int multiply)
    {
        hitQueue.Enqueue(bodyPart);
        
        multiplies.Enqueue(multiply);

        if (!_isCoroutinePlay)
            StartCoroutine(ShowScore());
    }

    private IEnumerator ShowScore()
    {
        _isCoroutinePlay = true;
        yield return new WaitForSecondsRealtime(delay);
        popupScoreText.transform.DOScale(0, 0f);
        var first = hitQueue.Dequeue();
        var multiply = multiplies.Dequeue();
        var i = 1;
        while (hitQueue.Count > 0 && hitQueue.Peek() == first)
        {
            hitQueue.Dequeue();
            multiplies.Dequeue();
            i++;
        }
        popupScoreText.text = i > 1 ? $"{_nameDictionary[first].Score * multiply} x {i}" : $"{_nameDictionary[first].Score * multiply}";
        popupScoreText.transform.DOScale(1, 0.1f).SetEase(ease).SetUpdate(true);
            
        score += _nameDictionary[first].Score * multiply;
        scoreText.text = $"{score}";
            
        ShowScore(first, i);
        _isCoroutinePlay = false;
    }

    public List<(PartsName name, int count)> GetTopOfHits(int count)
    {
       return _countOfHits
           .OrderBy((val => val.Value))
           .Reverse()
           .Select(pair => (_nameDictionary[pair.Key], pair.Value))
           .Take(count)
           .ToList();
        
    }

    private void ShowScore(BodyPart bodyPart, int count)
    {
        var go = Instantiate(scorePrefab, transform);
        go.transform.DOScale(0, 0);
        go.gameObject.GetComponentInChildren<TMP_Text>().text = count > 1 ? $"{count}X" : "";
        go.sprite = _nameDictionary[bodyPart].Sprite;

        go.transform.DOScale(1, scaleDuration).SetUpdate(true);
        UpdateScrollToEnd(go.rectTransform);
    }

    private void UpdateScrollToEnd(RectTransform selectedRectTransform)
    {
        _contentRectTransform.DOComplete();
        var contentRect = _contentRectTransform.rect;
        var scrollRectRect = _rectTransform.rect;

        _contentRectTransform.DOLocalMove(
            new Vector3(0, contentRect.height - scrollRectRect.height + selectedRectTransform.rect.height, 0),
            scrollSpeed).SetEase(ease).SetUpdate(true);
    }
}