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
    }
}
