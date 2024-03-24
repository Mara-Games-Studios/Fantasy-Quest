using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class IndicatingButton : Button
    {
        public UnityAction OnMouseEntered;
        
        public void OnMouseEnter()
        {
            OnMouseEntered.Invoke();
        }
    }
}
