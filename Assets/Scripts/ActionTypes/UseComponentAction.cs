using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "GOAP/Actions/ToggleComponentAction")]
public class UseComponentAction : Action {
    public bool done;
    private float interval;
    public float workDuration = 1.0f;
    public bool toggle;
    public bool shouldEffectWorldState;
    public UseComponentAction () {
        done = false;
    }

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
          ToggleAbleComponent component = target.GetComponent<ToggleAbleComponent>();
          component.toggle(toggle);
          PickableComponent pickupComponent = target.GetComponent<PickableComponent>();
          if(pickupComponent != null) {
            if(agent.agentStateProvider is Student) {
              if(toggle) {
               (agent.agentStateProvider as Student).addInventoryItem(pickupComponent.pickupObjectName); 
              } else {
               (agent.agentStateProvider as Student).removeInventoryItem(pickupComponent.pickupObjectName); 
              }
            } else {
              Debug.Log("Agent does not have an inventory");
            }
          }
          if(shouldEffectWorldState) {
            foreach(KeyValuePair<string,bool> kv in effects) {
              agent.worldStateProvider.changeWorldState(kv.Key, kv.Value);
            }
          }
          done = true;
        }
        return true;
    }
}