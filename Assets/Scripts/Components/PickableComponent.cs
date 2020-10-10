using System.Collections.Generic;
using UnityEngine;

public class PickableComponent : ToggleAbleComponent
{
    public Renderer meshRenderer;
    public bool hideMeshOnPickup;
    public string pickupObjectName;
    public override void toggle (bool toggle) {
      if(hideMeshOnPickup) {
      meshRenderer.enabled = !toggle;
      }
    }

}
