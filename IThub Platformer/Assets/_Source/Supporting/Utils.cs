using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supporting
{
    public class Utils
    {
        public static bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }

}
