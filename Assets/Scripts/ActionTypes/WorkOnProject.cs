using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/WorkOnProject")]
public class WorkOnProject : Action {

    public bool done;
    private float interval;
    public float workDuration = 0.5f;

    public override void reset() {
        done = false;
        interval = 0.0f;
    }

    public override bool isDone() {
        return done;
    }

    public override bool perform (Agent agent) {
        if (interval == 0 ) {
            interval = Time.time;
        }
        if(Time.time - interval > workDuration) {
            Student studentData = agent.agentStateProvider as Student;
            studentData.projectWorkCompleted +=5;
            studentData.productivity -= 2;
            studentData.hunger += 3;
            if(studentData.projectWorkCompleted >= 100) {
                done = true;
            }
            interval = Time.time;
        }
        return true;
    }
}