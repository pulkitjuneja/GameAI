using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class GoapAgentData : MonoBehaviour
{

	public List<Action> availableActions;
	protected Queue<Goal> goals;
	protected bool isMoving = false;

	public void addAction(Action a) {
		availableActions.Add (a);
	}

	public void removeAction(Action action) {
		availableActions.Remove (action);
	}

	public void addGoal(Goal goal) {
		goals.Enqueue (goal);
	}

	public Action getAction(Type action) {
		foreach (Action g in availableActions) {
			if (g.GetType().Equals(action) )
			    return g;
		}
		return null;
	}

	void Start () {
		availableActions = new List<Action>();
		goals = new Queue<Goal>();
		createInitialState();
		loadActions();
		createGoals();
	}
	private void loadActions ()
	{
		Action[] actions = gameObject.GetComponents<Action>();
		foreach (Action a in actions) {
			availableActions.Add (a);
		}
		Debug.Log("Found actions: " + Action.prettyPrint(actions));
	}

	public abstract void createGoals();
	public abstract void createInitialState ();
	public abstract Dictionary<string, bool> getAgentState ();
	public abstract Dictionary<string, bool> getPrioritizedGoalState ();
	public abstract void planAborted (Action aborter);
	public abstract bool moveAgent(Action nextAction);
}

