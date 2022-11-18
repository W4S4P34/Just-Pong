using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ultilities;

public class CameraHelper : GenericSingleton<CameraHelper>
{
    private float lastWidth = 0;
    private float lastHeight = 0;

    private void Update()
    {
        if (lastWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, Mathf.RoundToInt(Screen.width * (9f / 16f)), false);
        }
        else if (lastHeight != Screen.height)
        {
            Screen.SetResolution(Mathf.RoundToInt(Screen.height * (16f / 9f)), Screen.height, false);
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }
}
