using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/ToggleComponentAction")]
public class ToggleComponentAction : Action {
    public bool done;
    private float interval;
    public float workDuration = 1.0f;
    public bool toggle;
    public ToggleComponentAction () {
        done = false;
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
          ToggleAbleComponent component = target.GetComponent<ToggleAbleComponent>();
          component.toggle(toggle);
          bool isPickable = target.GetComponent<PickableComponent>() != null;
          if(isPickable) {
            if(agent.agentStateProvider is Student) {
               (agent.agentStateProvider as Student).inventory.Add(target.name); 
            }
          }
          foreach(KeyValuePair<string,bool> kv in effects) {
            agent.worldStateProvider.changeWorldState(kv.Key, kv.Value);
          }
          done = true;
        }
        return true;
    }
}