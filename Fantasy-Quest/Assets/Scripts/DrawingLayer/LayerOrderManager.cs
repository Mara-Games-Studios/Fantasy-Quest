using Sirenix.OdinInspector;
using UnityEngine;

namespace DrawingLayer
{
    [AddComponentMenu("Scripts/DrawingLayer/DrawingLayer.LayerOrderManager")]
    internal class LayerOrderManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject cat;

        [SerializeField]
        private GameObject symon;

        private Renderer catSkeleton;
        private Renderer symonSkeleton;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            catSkeleton = cat.GetComponentInChildren<Renderer>();
            symonSkeleton = symon.GetComponentInChildren<Renderer>();
        }

        private void Update()
        {
            ChooseLayer();
        }

        private void ChooseLayer()
        {
            if (symon.transform.position.y < cat.transform.position.y)
            {
                symonSkeleton.sortingOrder = 3;
                catSkeleton.sortingOrder = 2;
            }
            else
            {
                symonSkeleton.sortingOrder = 2;
                catSkeleton.sortingOrder = 3;
            }
        }

        [Button]
        private void FindCharacters()
        {
            cat = GameObject.Find("Cat");
            symon = GameObject.Find("Symon");
        }
    }
}
