using System;
using UnityEngine;

public class FinishReadings : Action {

    public bool done;
    private float intervalTime;
    private float workDuration = 0.5f;
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
        if (intervalTime == 0 ) {
            intervalTime = Time.time;
        }
        if(Time.time - intervalTime > workDuration) {
            Student studentData = agent.agentStateProvider as Student;
            studentData.readingsProgress +=5;
            studentData.productivity -= 1;
            if(studentData.readingsProgress >= 100) {
                done = true;
            }
            intervalTime = Time.time;
        }
        return true;
    }
}