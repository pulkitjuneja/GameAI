using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class IdleState: FSMState {
   public void Update (FSM fsm, Agent agent) {

			StringBoolDictionary worldState = agent.getCurrentState();
			StringBoolDictionary goal = agent.agentStateProvider.getPrioritizedGoalState();

			Queue<Action> plan = agent.planner.plan(agent, agent.agentStateProvider.availableActions, worldState, goal);
			if (plan != null && plan.Count > 0) {
				agent.currentActions = plan;

				fsm.popState();
				fsm.pushState(agent.performActionState);

			} else {
				Debug.Log("Failed Plan:"+ goal);
				fsm.popState ();
				fsm.pushState (agent.idleState);
			}
   }
}

public class MoveToState : FSMState {
  public void Update (FSM fsm, Agent agent) {
			Action action = agent.currentActions.Peek();
			if (action.requiresInRange() && action.target == null) {
				Debug.Log("Fatal error: Action requires a target but has none. Planning failed.");
				fsm.popState();
				fsm.popState();
				fsm.pushState(agent.idleState);
				return;
			}
			if ( agent.agentStateProvider.moveAgent(action) ) {
				fsm.popState();
			}
  }
}

public class PerformActionState : FSMState {
  public void Update (FSM fsm, Agent agent) {
    	if (!agent.hasActionPlan()) {
				Debug.Log("Done action");
				fsm.popState();
				fsm.pushState(agent.idleState);
				return;
			}

			Action action = agent.currentActions.Peek();
			if ( action.isDone() ) {
				agent.currentActions.Dequeue();
				Debug.Log(Action.prettyPrint(action) + "Done");	
			}

			action = agent.currentActions.Peek();
			bool inRange = action.requiresInRange() ? action.isInRange() : true;

			if ( inRange ) {
				bool success = action.isPlanStillValid(agent) && action.perform(agent);

				if (!success) {
					Debug.Log("Plan Failed");
					fsm.popState();
					fsm.pushState(agent.idleState);
					agent.agentStateProvider.planAborted(action);
				}
			} else {
				fsm.pushState(agent.moveToState);
			}
  }
}