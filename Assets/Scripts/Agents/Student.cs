using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Student : GoapAgentData {
  public int bladder;
  public int hunger;
  public int productivity;
  public int lectureProgress;
  public int readingsProgress;
  public int projectWorkCompleted;
  public List<string> inventory;


  public override void createInitialState() {
    // bladder = 0;
    // hunger = 0;
    // productivity = 100;
    // lectureProgress = 0;
    // readingsProgress = 0;
    // projectWorkCompleted = 0;
  }
  public override StringBoolDictionary getAgentState () {
    StringBoolDictionary agentState = new StringBoolDictionary();
    agentState.Add("HasToGo", bladder > 60 ? true : false);
    agentState.Add("RequiresFood", hunger > 60 ? true : false);
    agentState.Add("IsProductive", productivity > 40 ? true : false);
    agentState.Add("lectureCompleted", lectureProgress > 100 ? true : false);
    agentState.Add("readingsCompleted", readingsProgress > 100 ? true : false);
    agentState.Add("projectWorkCompleted", projectWorkCompleted > 100 ? true : false);
    return agentState;
  }
  public override void planAborted(Action aborter) { }
  
	public override bool moveAgent(Action nextAction){
    NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent;
    Vector3 targetPositionInPlane = new Vector3(nextAction.target.transform.position.x, 0, nextAction.target.transform.position.z);
    Vector3 distance = transform.position - targetPositionInPlane ;
    if(distance.magnitude <= 5.0f) {
      navMeshAgent.isStopped = true;
      isMoving = false;
      nextAction.setInRange(true);
      return true;
    }
    if(!isMoving) {
      if(navMeshAgent == null) {
        Debug.Log("The object is not a navvmesh agent");
      }
      navMeshAgent.isStopped = false;
      navMeshAgent.SetDestination(targetPositionInPlane);
      isMoving = true;
      Debug.Log("Next destination-" + navMeshAgent.destination);
    }
    return false;
  }
  }
