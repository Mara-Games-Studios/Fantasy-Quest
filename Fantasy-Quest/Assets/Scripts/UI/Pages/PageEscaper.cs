using UnityEngine;

namespace UI
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.PageEscaper")]
    public class PageEscaper : MonoBehaviour
    {
        private Pages.View currentPage;
        private MainMenuInput mainMenuInput;    

        private void Awake()
        {
            mainMenuInput = new();
            mainMenuInput.Enable();
        }
        
        private void OnEnable()
        {
            Pages.View.OnPageShowing += SetPage;
            mainMenuInput.UI.Exit.performed += ctx => GetBack();
        }

        private void OnDisable()
        {
            Pages.View.OnPageShowing -= SetPage;
            mainMenuInput.UI.Exit.performed -= ctx => GetBack();
        }
        
        private void GetBack()
        {
            if (currentPage == null)
                return;
            if(currentPage.LastPage == null)
                return;

            currentPage.Hide();
            currentPage.LastPage.Show();
        }
        
        private void SetPage(Pages.View page)
        {
            currentPage = page;
        }
    }
}
