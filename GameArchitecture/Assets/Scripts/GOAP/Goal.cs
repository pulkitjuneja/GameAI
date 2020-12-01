using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

//Todo: see if this can be a scriptableObject

[CreateAssetMenu (menuName = "GOAP/Goal")]
public class Goal: ScriptableObject {

  public string goalName;
	[SerializeField]
  public  StringBoolDictionary preconditions;
	[SerializeField]
  public StringBoolDictionary goalState;

  public void addPrecondition(string key, bool value) {
		preconditions.Add (key, value);
	}

  public string getName() {
    return goalName;
  }

	public void removePrecondition(string key) {
		if (preconditions.ContainsKey(key)){
			preconditions.Remove(key);
		}
	}

  public void addGoalState(string key, bool value) {
		goalState.Add (key, value);
	}

	public void addGoalState(string key) {
		if (goalState.ContainsKey(key)){
			goalState.Remove(key);
		}
	}

	public StringBoolDictionary Preconditions {
		get {
			return preconditions;
		}
	}

  public StringBoolDictionary GoalState {
		get {
			return goalState;
		}
	}

  public static string log(Goal[] goals) {
		String s = "";
		foreach (Goal goal in goals) {
			s += goal.GetType().Name;
			s += ", ";
		}
		return s;
	}

	public static string log(Goal goal) {
		String s = ""+goal.GetType().Name;
		return s;
	}

}