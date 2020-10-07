using UnityEngine;
using System.Collections;

public interface FSMState 
{
	void Update (FSM fsm, Agent agent);
}

