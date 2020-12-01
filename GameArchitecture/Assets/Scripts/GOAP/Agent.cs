using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public sealed class Agent : MonoBehaviour {

	private FSM stateMachine;
	public FSMState idleState;
	public FSMState moveToState;
	public FSMState performActionState;
	public Queue<Action> currentActions;
	public GoapAgentData agentStateProvider;
	public WorldStateProvider worldStateProvider; 
	public Planner planner;
	public Goal currentGoal;
	public float replanCheckInterval;
	public AgentUIController agentUIController;


	void Start () {
		stateMachine = new FSM ();
		currentActions = new Queue<Action> ();
		planner = new Planner ();
		idleState = new IdleState();
		moveToState = new MoveToState();
		performActionState = new PerformActionState();
		stateMachine.pushState (idleState);
	}

	void FixedUpdate () {
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

	public void setCurrentGoal (Goal goal) {
		this.currentGoal = goal;
		this.agentUIController.updateCurrentGoalUI(goal);
	}

	public bool isPlanStillValid() {
		StringBoolDictionary worldState = this.getCurrentState();
		StringBoolDictionary preconditions = this.currentGoal.preconditions;
		bool result = preconditions.All(precondition => worldState.ContainsKey (precondition.Key) && worldState[precondition.Key] == precondition.Value);
		return result;
	}

	public StringBoolDictionary getPrioritizedGoalState (StringBoolDictionary worldState) {
		foreach (Goal goal in agentStateProvider.goals) {
      bool instate = goal.preconditions.All(pre => worldState.ContainsKey(pre.Key) && worldState[pre.Key] == pre.Value);
      if(instate) {
				setCurrentGoal(goal);
        return goal.GoalState;
      }
    }
    // If no goal was found to match return the least priority goal
		setCurrentGoal(agentStateProvider.goals[agentStateProvider.goals.Count -1]);
    return this.currentGoal.GoalState;
	}

	public static string log(Dictionary<string, object> state) {
		String s = "";
		foreach (KeyValuePair<string,object> kvp in state) {
			s += kvp.Key + ":" + kvp.Value.ToString();
			s += ", ";
		}
		return s;
	}
}
