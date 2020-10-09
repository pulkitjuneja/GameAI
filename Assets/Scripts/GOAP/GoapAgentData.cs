using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class GoapAgentData : MonoBehaviour
{
	public List<Action> availableActions;
	public List<GameObject> actionTargets;
	public List<Goal> goals;
	protected bool isMoving = false;

	public void addAction(Action a) {
		availableActions.Add (a);
	}

	public void removeAction(Action action) {
		availableActions.Remove (action);
	}

	public void addGoal(Goal goal) {
		goals.Add (goal);
	}

	public Action getAction(Type action) {
		foreach (Action g in availableActions) {
			if (g.GetType().Equals(action) )
			    return g;
		}
		return null;
	}

	//TODO: Find a better way to do this
	public void attachActionTargets () {
		for(int i = 0; i< availableActions.Count; i++) {
			if(i < actionTargets.Count) {
				availableActions[i].target = actionTargets[i];
			}
		}
	}

	void Start () {
		createInitialState();
		attachActionTargets();
	}

	// public abstract void createGoals();
	public abstract void createInitialState ();
	public abstract StringBoolDictionary getAgentState ();
	public abstract void planAborted (Action aborter);
	public abstract bool moveAgent(Action nextAction);
}

