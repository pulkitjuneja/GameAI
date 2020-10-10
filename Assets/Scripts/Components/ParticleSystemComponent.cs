using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemComponent : ToggleAbleComponent
{
    public ParticleSystem particleSystem;

    public void Start () {
      particleSystem.Stop();
    }
    public override void toggle (bool toggle) {
      if(toggle) {
        particleSystem.Play();
      } else {
        particleSystem.Stop();
      }
    }

}
