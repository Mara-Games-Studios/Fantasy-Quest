using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BlackLinesShower : MonoBehaviour
{
    [Serializable] 
    private struct Line
    {
        public RectTransform StartPoint;
        public RectTransform EndPoint;
        public UnityEngine.UI.Image Image;
    }

    [SerializeField] 
    private float duration;

    [SerializeField] 
    private float targetAlpha;
    
    [SerializeField] 
    private List<Line> lines;

    //used by Timeline
    [Button]
    public void Show()
    {
        foreach (var line in lines)
        {
            line.Image.rectTransform.position = line.StartPoint.position;
            var moveTween = line.Image.rectTransform.DOMove(line.EndPoint.position, duration);
            var fadeTween = line.Image.DOFade(targetAlpha, duration);
        }        
    }

    //used by Timeline
    [Button]
    public void Hide()
    {
        foreach (var line in lines)
        {
            line.Image.rectTransform.position = line.EndPoint.position;
            var moveTween = line.Image.rectTransform.DOMove(line.StartPoint.position, duration);
            var fadeTween = line.Image.DOFade(0f, duration);
        }        
    }
}


