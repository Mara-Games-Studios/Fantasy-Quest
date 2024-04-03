using UnityEngine;

namespace UI
{
    public class FirstPageShower : MonoBehaviour
    {
        [SerializeField] private Pages.View page;

        private void Start()
        {
            page.ShowFromStart();
        }
        
        //in future -- cutscene starter then first page shower 
    }
}
