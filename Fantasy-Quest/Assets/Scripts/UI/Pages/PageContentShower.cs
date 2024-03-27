using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    public class PageContentShower : MonoBehaviour
    {
        [SerializeField] private List<EffectsShower> frames;
        [SerializeField] private EffectsShower page;
        
        private int currentPageIndex;

        public void Awake()
        {
            foreach (var frame in frames)
            {
                frame.Initialize();
            }
        }

        private void OnEnable()
        {
            page.OnEffectShowed += () => frames[currentPageIndex].ShowFromStart();
        }

        private void OnDisable()
        {
            page.OnEffectShowed -= () => frames[currentPageIndex].ShowFromStart();
        }

        //invoked by UI buttons
        [Button]
        public void ShowRight()
        {
            frames[currentPageIndex].HideToEnd();
                
            currentPageIndex++;
            if (currentPageIndex >= frames.Count)
            {
                currentPageIndex = 0;
            }
            
            frames[currentPageIndex].ShowFromStart();
        }
        
        //invoked by UI buttons
        [Button]
        public void ShowLeft()
        {
            frames[currentPageIndex].HideToStart();
            
            currentPageIndex--;
            if (currentPageIndex < 0)
            {
                currentPageIndex = frames.Count - 1;
            }
            
            frames[currentPageIndex].ShowFromEnd();
        }
    }
}
