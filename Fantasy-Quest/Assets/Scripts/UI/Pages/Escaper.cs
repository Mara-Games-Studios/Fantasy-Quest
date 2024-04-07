using UnityEngine;

namespace UI
{
    [AddComponentMenu("Scripts/UI/Pages/UI.Pages.Escaper")]
    public class Escaper : MonoBehaviour
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
        
        public void Exit()
        {
            if (currentPage == null)
                return;
            if(currentPage.PreviousPage == null)
                return;
            lastPage = currentPage.PreviousPage;
            
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
