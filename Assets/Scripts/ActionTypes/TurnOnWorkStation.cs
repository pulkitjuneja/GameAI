using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/TurnOnWorkStation")]
public class TurnOnWorkStation : Action {
    public bool done;
    private float interval;
    public float workDuration = 1.0f;
    public TurnOnWorkStation () {
        done = false;
        addPrecondition("WorkStationOn", false);
        addEffect("WorkStationOn", true);
    }

    public override void reset() {
        done = false;
    }

    public override bool isDone() {
        return done;
    }

    public override bool perform (Agent agent) {
        if (interval == 0 ) {
            interval = Time.time;
        }
        if(Time.time - interval > workDuration) {
          Debug.Log(target);
          ScreensComponent screens = target.GetComponent<ScreensComponent>();
          screens.ToggleScreens(true);
          agent.worldStateProvider.changeWorldState("WorkStationOn", true);
          done = true;
        }
        return true;
    }
}