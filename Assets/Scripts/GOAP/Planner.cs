using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Planner
{
	public Queue<Action> plan(Agent agent, List<Action> availableActions, Dictionary<string, bool> worldState,  Dictionary<string, bool> goal) 
	{
		foreach (Action a in availableActions) {
			a.doReset ();
		}

		List<StateSearchNode> leaves = new List<StateSearchNode>();

		StateSearchNode start = new StateSearchNode (null, 0, worldState, null);
		StateSearchNode result = searchPlan(start, availableActions, goal);

		List<Action> plan = new List<Action> ();
		StateSearchNode n = result;
		while (n != null) {
			if (n.action != null) {
				plan.Insert(0, n.action);
			}
			n = n.parent;
		}

		Queue<Action> queue = new Queue<Action> ();
		foreach (Action a in plan) {
			queue.Enqueue(a);
		}

		return queue;
	}

	private StateSearchNode searchPlan (StateSearchNode parent, List<Action> availableActions, Dictionary<string, bool> goal) {
			PriorityQueue<StateSearchNode> fringe = new PriorityQueue<StateSearchNode>();
			List<StateSearchNode> visited = new List<StateSearchNode>();
			float minDistanceToGoal = Int32.MaxValue;
			StateSearchNode result = null;
			fringe.Enqueue(parent);
			while(fringe.Count() > 0) {
				StateSearchNode current = fringe.Dequeue();
				if(inState(goal, current.state) && current.runningCost < minDistanceToGoal) {
					minDistanceToGoal = current.runningCost;
					result = current;
				}
				if(!visited.Contains(current)) {
					foreach (Action action in availableActions) { 
						if(inState(action.Preconditions, current.state)) {
							Dictionary<string, bool> nextState = populateState (current.state, action.Effects);
							StateSearchNode nextNode = new StateSearchNode(current, current.runningCost+action.cost, nextState, action);
							fringe.Enqueue(nextNode);
					}
				}
				visited.Add(current);
			}
		}
		return result;
	}
	
	private bool inState(Dictionary<string, bool> test, Dictionary<string, bool> state) {
		return test.All(precondition => state.Contains(precondition));
	}
	
	private Dictionary<string, bool> populateState(Dictionary<string, bool> currentState, Dictionary<string, bool> stateChange) {
		Dictionary<string, bool> state = new Dictionary<string, bool> ();
		// copy the KVPs over as new objects
		foreach (KeyValuePair<string,bool> s in currentState) {
			state.Add(s.Key,s.Value);
		}

		foreach (KeyValuePair<string, bool> kv in stateChange) {
			if(state.ContainsKey(kv.Key)) {
				state[kv.Key] = kv.Value;
			} 
			else {
				state.Add(kv.Key, kv.Value);
			}
		}
		return state;
	}

}


