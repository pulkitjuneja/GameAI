using System;
using UnityEngine;

public class TurnOnWorkStation : Action {
    public bool done;
    private float intervalTime;
    private float workDuration = 1.0f;
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
        if (intervalTime == 0 ) {
            intervalTime = Time.time;
        }
        if(Time.time - intervalTime > workDuration) {
          // TODO: implement screen turn on feature
          agent.worldStateProvider.changeWorldState("WorkStationOn", true);
          done = true;
        }
        return true;
    }
}