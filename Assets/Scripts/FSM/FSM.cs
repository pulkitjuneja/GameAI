using System.Collections.Generic;

/**
 * Stack-based Finite State Machine.
 * Push and pop states to the FSM.
 * 
 * States should push other states onto the stack 
 * and pop themselves off.
 */

public class FSM {

	private Stack<FSMState> stateStack = new Stack<FSMState> ();

	public void Update (Agent agent) {
		if (stateStack.Peek() != null)
			stateStack.Peek().Update (this, agent);
	}

	public void pushState(FSMState state) {
		stateStack.Push (state);
	}

	public void popState() {
		stateStack.Pop ();
	}
}
