using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/CookFood")]
public class CookFood : Action {

    public bool done;
    private float interval = 0.0f;
    public float workDuration = 3.0f;
    public override void reset() {
        done = false;
        interval = 0.0f;
    }

    public override bool isDone() {
        return done;
    }

    public override bool perform (Agent agent) {
        if (interval == 0.0f ) {
            interval = Time.time;
        }
        if(Time.time - interval > workDuration) {
            Student studentData = agent.agentStateProvider as Student;
            studentData.inventory.Remove("RawFood");
            studentData.inventory.Add("CookedFood");
            done = true;
        }
        return true;
    }
}