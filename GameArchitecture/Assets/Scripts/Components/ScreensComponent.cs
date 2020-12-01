using System.Collections.Generic;
using UnityEngine;

public class ScreensComponent : UsableComponent
{
    public List<Renderer> screenPlanes;

    public override void toggle (bool toggle) {
        foreach (Renderer screen in screenPlanes) {
            Color screenColor = toggle ? new Color(0,0,1,1) : new Color(0,0,0,1);
            screen.material.SetColor("_BaseColor", screenColor);
        }
    }

}
