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
    [Range(0f, 1f)]
    private float targetAlpha;

    [SerializeField]
    private List<Line> lines;

    // Prepared to be invoked by Timeline.
    [Button]
    public void Show()
    {
        foreach (Line line in lines)
        {
            line.Image.rectTransform.position = line.StartPoint.position;
            _ = line.Image.rectTransform.DOMove(line.EndPoint.position, duration);
            _ = line.Image.DOFade(targetAlpha, duration);
        }
    }

    // Prepared to be invoked by Timeline.
    [Button]
    public void Hide()
    {
        foreach (Line line in lines)
        {
            line.Image.rectTransform.position = line.EndPoint.position;
            _ = line.Image.rectTransform.DOMove(line.StartPoint.position, duration);
            _ = line.Image.DOFade(0f, duration);
        }
    }
}
