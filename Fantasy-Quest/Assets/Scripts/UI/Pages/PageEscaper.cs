using UnityEngine;

namespace UI
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.PageEscaper")]
    public class PageEscaper : MonoBehaviour
    {
        private Pages.View currentPage;
        private Pages.View lastPage;
        private MainMenuInput mainMenuInput;    

        private void Awake()
        {
            mainMenuInput = new();
            mainMenuInput.Enable();
        }
        
        private void OnEnable()
        {
            Pages.View.OnPageShowing += SetPage;
            mainMenuInput.UI.Exit.performed += ctx => Exit();
        }

        private void OnDisable()
        {
            Pages.View.OnPageShowing -= SetPage;
            mainMenuInput.UI.Exit.performed -= ctx => Exit();
        }
        
        private void Exit()
        {
            if (currentPage == null)
                return;
            if(currentPage.LastPage == null)
                return;
            lastPage = currentPage.LastPage;
            
            currentPage.HideToStart();
            lastPage.ShowFromStart();    
        }
        
        private void SetPage(Pages.View page)
        {
            if(page == null)
                return;
            currentPage = page;
        }
    }
}
