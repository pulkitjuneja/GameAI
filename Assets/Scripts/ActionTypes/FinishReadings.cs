using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/FinishReadings")]
public class FinishReadings : Action {

    public bool done;
    private float interval = 0.0f;
    public float workDuration = 0.5f;
    public FinishReadings () {
        done = false;
        addPrecondition("RequiresFood", false);
        addPrecondition("HasToGo", false);
        addPrecondition("IsProductive", true);
        addPrecondition("lectureCompleted", true);
        addPrecondition("WorkStationOn", true);
        addEffect("readingsCompleted", true);
    }

    public override void reset() {
        done = false;
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
            studentData.readingsProgress +=5;
            studentData.productivity -= 1;
            if(studentData.readingsProgress >= 100) {
                done = true;
            }
            interval = Time.time;
        }
        return true;
    }
}