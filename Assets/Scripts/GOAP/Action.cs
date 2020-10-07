using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class Action : ScriptableObject {

	public StringBoolDictionary preconditions;
	public StringBoolDictionary effects;
	public bool inRange = false;
	public float cost = 1f;

	[HideInInspector]
	public GameObject target;
	public bool requiresRange;
	
	public Action () {
		preconditions = new StringBoolDictionary();
		effects = new StringBoolDictionary();
	}
	public void doReset() {
		inRange = false;
		reset ();
	}

	public abstract void reset();
	public abstract bool isDone();
	public abstract bool perform(Agent agent); 

	public bool requiresInRange () {
		return requiresRange;
	}

	public bool isInRange () {
		return inRange;
	}
	
	public void setInRange(bool inRange) {
		this.inRange = inRange;
	}

	public bool isPlanStillValid(Agent agent) {
			StringBoolDictionary worldState = agent.getCurrentState();
			StringBoolDictionary preconditions = this.preconditions;
			bool result = preconditions.All(precondition => worldState.ContainsKey (precondition.Key) && worldState[precondition.Key] == precondition.Value);
			return result;
	}

	public void addPrecondition(string key, bool value) {
		preconditions.Add (key, value);
	}

	public void removePrecondition(string key) {
		if (preconditions.ContainsKey(key)){
			preconditions.Remove(key);
		}
	}

	public void addEffect(string key, bool value) {
		effects.Add (key, value);
	}

	public void removeEffect(string key) {
				if (effects.ContainsKey(key)){
			effects.Remove(key);
		}
	}
	
	public StringBoolDictionary Preconditions {
		get {
			return preconditions;
		}
	}

	public StringBoolDictionary Effects {
		get {
			return effects;
		}
	}

	public static string prettyPrint(Queue<Action> actions) {
		String s = "";
		foreach (Action a in actions) {
			s += a.GetType().Name;
			s += "-> ";
		}
		s += "GOAL";
		return s;
	}

	public static string prettyPrint(Action[] actions) {
		String s = "";
		foreach (Action a in actions) {
			s += a.GetType().Name;
			s += ", ";
		}
		return s;
	}

	public static string prettyPrint(Action action) {
		String s = ""+action.GetType().Name;
		return s;
	}

}