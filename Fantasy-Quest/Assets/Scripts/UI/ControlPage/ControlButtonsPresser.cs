using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ControlPage
{
    [AddComponentMenu("Scripts/UI/ControlPage/UI.ControlPage.ControlButtonsPresser")]
    public class ControlButtonsPresser : MonoBehaviour
    {
        [SerializeField]
        private ControlButtonsBackgrounds controlButtonsBackgrounds;

        [SerializeField]
        private Color pressedColor;

        [SerializeField]
        private float duration = 1f;

        [SerializeField]
        private float maxFade = 0.8f;

        private GameplayInput mainMenuInput;

        private void Awake()
        {
            mainMenuInput = new();
            mainMenuInput.Enable();
            controlButtonsBackgrounds.Initialize();

            foreach (Image background in controlButtonsBackgrounds.AllBackgrounds)
            {
                background.gameObject.SetActive(true);
                background.color = new Color(pressedColor.r, pressedColor.g, pressedColor.b, 0f);
            }
        }

        private void OnEnable()
        {
            mainMenuInput.UI.Up.started += ctx => Press(controlButtonsBackgrounds.Up);
            mainMenuInput.UI.Up.canceled += ctx => UnPress(controlButtonsBackgrounds.Up);

            mainMenuInput.UI.Left.started += ctx => Press(controlButtonsBackgrounds.Left);
            mainMenuInput.UI.Left.canceled += ctx => UnPress(controlButtonsBackgrounds.Left);

            mainMenuInput.UI.Down.started += ctx => Press(controlButtonsBackgrounds.Down);
            mainMenuInput.UI.Down.canceled += ctx => UnPress(controlButtonsBackgrounds.Down);

            mainMenuInput.UI.Right.started += ctx => Press(controlButtonsBackgrounds.Right);
            mainMenuInput.UI.Right.canceled += ctx => UnPress(controlButtonsBackgrounds.Right);

            mainMenuInput.UI.CatInteraction.started += ctx =>
                Press(controlButtonsBackgrounds.CatInteraction);
            mainMenuInput.UI.CatInteraction.canceled += ctx =>
                UnPress(controlButtonsBackgrounds.CatInteraction);

            mainMenuInput.UI.HumanInteraction.started += ctx =>
                Press(controlButtonsBackgrounds.HumanInteraction);
            mainMenuInput.UI.HumanInteraction.canceled += ctx =>
                UnPress(controlButtonsBackgrounds.HumanInteraction);

            mainMenuInput.UI.Meow.started += ctx => Press(controlButtonsBackgrounds.Meow);
            mainMenuInput.UI.Meow.canceled += ctx => UnPress(controlButtonsBackgrounds.Meow);

            mainMenuInput.UI.Mure.started += ctx => Press(controlButtonsBackgrounds.Mure);
            mainMenuInput.UI.Mure.canceled += ctx => UnPress(controlButtonsBackgrounds.Mure);
        }

        private void OnDisable()
        {
            mainMenuInput.UI.Up.started -= ctx => Press(controlButtonsBackgrounds.Up);
            mainMenuInput.UI.Up.canceled -= ctx => UnPress(controlButtonsBackgrounds.Up);

            mainMenuInput.UI.Left.started -= ctx => Press(controlButtonsBackgrounds.Left);
            mainMenuInput.UI.Left.canceled -= ctx => UnPress(controlButtonsBackgrounds.Left);

            mainMenuInput.UI.Down.started -= ctx => Press(controlButtonsBackgrounds.Down);
            mainMenuInput.UI.Down.canceled -= ctx => UnPress(controlButtonsBackgrounds.Down);

            mainMenuInput.UI.Right.started -= ctx => Press(controlButtonsBackgrounds.Right);
            mainMenuInput.UI.Right.canceled -= ctx => UnPress(controlButtonsBackgrounds.Right);

            mainMenuInput.UI.CatInteraction.started -= ctx =>
                Press(controlButtonsBackgrounds.CatInteraction);
            mainMenuInput.UI.CatInteraction.canceled -= ctx =>
                UnPress(controlButtonsBackgrounds.CatInteraction);

            mainMenuInput.UI.HumanInteraction.started -= ctx =>
                Press(controlButtonsBackgrounds.HumanInteraction);
            mainMenuInput.UI.HumanInteraction.canceled -= ctx =>
                UnPress(controlButtonsBackgrounds.HumanInteraction);

            mainMenuInput.UI.Meow.started -= ctx => Press(controlButtonsBackgrounds.Meow);
            mainMenuInput.UI.Meow.canceled -= ctx => UnPress(controlButtonsBackgrounds.Meow);

            mainMenuInput.UI.Mure.started -= ctx => Press(controlButtonsBackgrounds.Mure);
            mainMenuInput.UI.Mure.canceled -= ctx => UnPress(controlButtonsBackgrounds.Mure);
        }

        private void Press(Image background)
        {
            _ = background.DOFade(maxFade, duration).SetUpdate(true);
        }

        private void UnPress(Image background)
        {
            _ = background.DOFade(0, duration).SetUpdate(true);
        }
    }

    [Serializable]
    public class ControlButtonsBackgrounds
    {
        public Image Up;
        public Image Left;
        public Image Right;
        public Image Down;
        public Image CatInteraction;
        public Image HumanInteraction;
        public Image Meow;
        public Image Mure;

        [HideInInspector]
        public List<Image> AllBackgrounds;

        public void Initialize()
        {
            AllBackgrounds = new List<Image>
            {
                Up,
                Left,
                Down,
                Right,
                CatInteraction,
                HumanInteraction,
                Meow,
                Mure
            };
        }
    }
}
