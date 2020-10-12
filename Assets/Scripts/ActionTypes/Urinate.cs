using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/Urinate")]
public class Urinate : Action {
    public bool done;
    private float interval = 0.0f;
    public float workDuration = 0.5f;

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
            studentData.bladder = Mathf.Clamp(studentData.bladder - 100, 0, 100);
            studentData.updateStatsUI();
            done = true;
        }
        return true;
    }
}