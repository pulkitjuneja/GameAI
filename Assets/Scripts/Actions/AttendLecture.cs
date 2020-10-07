using System;
using UnityEngine;

public class AttendLecture : Action {

    public bool done;
    private float intervalTime;
    private float workDuration = 0.5f;
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
        if (intervalTime == 0 ) {
            intervalTime = Time.time;
        }
        if(Time.time - intervalTime > workDuration) {
            Debug.Log(Time.time - intervalTime);
            Student studentData = agent.agentStateProvider as Student;
            studentData.lectureProgress +=5;
            studentData.productivity -= 1;
            if(studentData.lectureProgress >= 100) {
                done = true;
            }
            intervalTime = Time.time;
        }
        return true;
    }
}