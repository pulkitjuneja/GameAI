using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/AttendLecture")]
public class AttendLecture : Action {
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
            studentData.lectureProgress +=5;
            studentData.productivity -= 3;
            studentData.bladder += 2;
            studentData.hunger += 3;
            studentData.updateStatsUI();
            if(studentData.lectureProgress >= 100) {
                done = true;
            }
            interval = Time.time;
        }
        return true;
    }
}