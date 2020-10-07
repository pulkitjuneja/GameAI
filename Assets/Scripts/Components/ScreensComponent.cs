using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreensComponent : MonoBehaviour
{
    public List<Renderer> screenPlanes;

    public void ToggleScreens (bool toggle) {
        foreach (Renderer screen in screenPlanes) {
            Color screenColor = toggle ? new Color(0,0,1,1) : new Color(0,0,0,1);
            screen.material.SetColor("_BaseColor", screenColor);
        }
    }

}
