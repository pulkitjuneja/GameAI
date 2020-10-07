using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

//Todo: see if this can be a scriptableObject
public abstract class Action : MonoBehaviour {

	private Dictionary<string, bool> preconditions;
	private Dictionary<string, bool> effects;
	private bool inRange = false;
	
	public float cost = 1f;
	public GameObject target;
	public bool requiresRange;

	public Action() {
		preconditions = new Dictionary<string, bool> ();
		effects = new Dictionary<string, bool> ();
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
			Dictionary<string, bool> worldState = agent.getCurrentState();
			Dictionary<string, bool> preconditions = this.preconditions;
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
	
	public Dictionary<string, bool> Preconditions {
		get {
			return preconditions;
		}
	}

	public Dictionary<string, bool> Effects {
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