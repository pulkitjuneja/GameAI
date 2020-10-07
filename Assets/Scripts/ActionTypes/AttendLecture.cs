using System;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/AttendLecture")]
public class AttendLecture : Action {
    public bool done;
    private float interval = 0.0f;
    public float workDuration = 0.5f;
    public AttendLecture () {
        done = false;
        addPrecondition("RequiresFood", false);
        addPrecondition("HasToGo", false);
        addPrecondition("IsProductive", true);
        addPrecondition("WorkStationOn", true);
        addEffect("lectureCompleted", true);
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
            Debug.Log("in");
            Student studentData = agent.agentStateProvider as Student;
            studentData.lectureProgress +=5;
            studentData.productivity -= 1;
            if(studentData.lectureProgress >= 100) {
                done = true;
            }
            interval = Time.time;
        }
        return true;
    }
}