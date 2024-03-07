using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("Scripts/Cutscene/Cutscene.BlackLinesShower")]
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

    // Prepared to be invoked by Timeline.
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

    // Prepared to be invoked by Timeline.
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


