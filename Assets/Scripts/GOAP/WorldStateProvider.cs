using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateProvider: MonoBehaviour {
  public Dictionary<string, bool> worldState;

  void Start () {
    setInitialWorldState();
  }

  void setInitialWorldState () {
        worldState = new Dictionary<string, bool>() {
          {"WorkStationOn", false}
        };
  }

  public void changeWorldState (string key, bool value) {
    if(worldState.ContainsKey(key)) {
      worldState[key] = value;
    } else {
      worldState.Add(key, value);
    }
  } 
  public Dictionary<string, bool> getWorldState () {
    return worldState;
  }
}