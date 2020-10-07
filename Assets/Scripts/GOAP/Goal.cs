using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Todo: see if this can be a scriptableObject
public class Goal {

  private string goalName;
  private  Dictionary<string, bool> preconditions;
  private  Dictionary<string, bool> goalState;

  public Goal (string goalName, Dictionary<string, bool> preconditions, Dictionary<string, bool> goals) {
		this.goalName = goalName;
    this.preconditions = preconditions;
    this.goalState = goals;
  }

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

	public Dictionary<string, bool> Preconditions {
		get {
			return preconditions;
		}
	}

  public Dictionary<string, bool> GoalState {
		get {
			return goalState;
		}
	}

  public static string prettyPrint(Goal[] goals) {
		String s = "";
		foreach (Goal goal in goals) {
			s += goal.GetType().Name;
			s += ", ";
		}
		return s;
	}

	public static string prettyPrint(Goal goal) {
		String s = ""+goal.GetType().Name;
		return s;
	}

}