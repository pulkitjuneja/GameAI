using System.Collections.Generic;
using UnityEngine;

public class StoveFlameComponent : ToggleAbleComponent
{
    public ParticleSystem stoveFlame;

    public void Start () {
      stoveFlame.Stop();
    }
    public override void toggle (bool toggle) {
      if(toggle) {
        stoveFlame.Play();
      } else {
        stoveFlame.Stop();
      }
    }

}
