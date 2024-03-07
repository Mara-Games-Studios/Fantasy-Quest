using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.CutoutMaskUI")]
    public class CutoutMaskUI : Image
    {
        public override Material materialForRendering
        {
            get
            {
                Material material = new(base.materialForRendering);
                material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
                return material;
            }
        }
    }
}
