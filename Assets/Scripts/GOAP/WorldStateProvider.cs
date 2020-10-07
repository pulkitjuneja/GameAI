using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateProvider: MonoBehaviour {
  public StringBoolDictionary worldState;

  // void Start () {
  //   setInitialWorldState();
  // }

  // void setInitialWorldState () {
  //       worldState = new StringBoolDictionary() {
  //         {"WorkStationOn", false}
  //       };
  // }

  public void changeWorldState (string key, bool value) {
    if(worldState.ContainsKey(key)) {
      worldState[key] = value;
    } else {
      worldState.Add(key, value);
    }
  } 
  public StringBoolDictionary getWorldState () {
    return worldState;
  }
}