using UnityEngine;
using System.Collections.Generic;
using System;


public sealed class Agent : MonoBehaviour {

	private FSM stateMachine;
	public FSMState idleState;
	public FSMState moveToState;
	public FSMState performActionState;
	public Queue<Action> currentActions;
	public GoapAgentData agentStateProvider;
	public WorldStateProvider worldStateProvider; 
	public Planner planner;


	void Start () {
		stateMachine = new FSM ();
		currentActions = new Queue<Action> ();
		planner = new Planner ();
		idleState = new IdleState();
		moveToState = new MoveToState();
		performActionState = new PerformActionState();
		stateMachine.pushState (idleState);
	}

	void Update () {
		stateMachine.Update (this);
	}
	public bool hasActionPlan() {
		return currentActions.Count > 0;
	}

	public StringBoolDictionary getCurrentState() {
		StringBoolDictionary agentState = agentStateProvider.getAgentState();
		StringBoolDictionary worldState = worldStateProvider.getWorldState();

		// Merge world state and agent state with conflict priority to world state
		foreach(KeyValuePair<string,bool> state in worldState) {
			if(agentState.ContainsKey(state.Key)) {
				agentState[state.Key] = state.Value;
			} else {
				agentState.Add(state.Key, state.Value);
			}
		}
		return agentState;
	}

	public static string prettyPrint(Dictionary<string, object> state) {
		String s = "";
		foreach (KeyValuePair<string,object> kvp in state) {
			s += kvp.Key + ":" + kvp.Value.ToString();
			s += ", ";
		}
		return s;
	}
}
