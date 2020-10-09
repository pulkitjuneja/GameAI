using System.Collections.Generic;
using UnityEngine;

public class PickableComponent : ToggleAbleComponent
{
    public Renderer meshRenderer;
    public override void toggle (bool toggle) {
      meshRenderer.enabled = !toggle;
    }

}
