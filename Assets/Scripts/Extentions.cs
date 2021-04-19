using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static void SetTransparity(this UnityEngine.UI.Image image, float transparency)
    {
        if (image != null)
        {
            UnityEngine.Color alpha = image.color;
            alpha.a = transparency;
            image.color = alpha;
        }
    }
}
