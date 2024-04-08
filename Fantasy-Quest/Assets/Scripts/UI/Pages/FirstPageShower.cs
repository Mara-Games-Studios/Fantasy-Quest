using UnityEngine;

namespace UI
{
    [AddComponentMenu("Scripts/UI/Pages/UI.Pages.FirstPageShower")]
    public class FirstPageShower : MonoBehaviour
    {
        [SerializeField]
        private Pages.View page;

        private void Start()
        {
            page.ShowFromStart();
        }

        //in future -- cutscene starter then first page shower
    }
}
