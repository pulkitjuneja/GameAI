using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class Student : GoapAgentData {
  public int bladder;
  public int hunger;
  public int productivity;
  public int lectureProgress;
  public int readingsProgress;
  public int projectWorkCompleted;


  public override void createInitialState() {
    // bladder = 0;
    // hunger = 0;
    // productivity = 100;
    // lectureProgress = 0;
    // readingsProgress = 0;
    // projectWorkCompleted = 0;
  }
  public override void createGoals () {
    this.addGoal(new Goal("Work", new Dictionary<string, bool> {},
    new Dictionary<string, bool> {
     { "projectWorkCompleted", true}
    }
    ));
  }
  public override Dictionary<string, bool> getAgentState () {
    Dictionary<string, bool> agentState = new Dictionary<string, bool>();
    agentState.Add("HasToGo", bladder > 60 ? true : false);
    agentState.Add("RequiresFood", hunger > 60 ? true : false);
    agentState.Add("IsProductive", productivity > 60 ? true : false);
    agentState.Add("lectureCompleted", lectureProgress > 100 ? true : false);
    agentState.Add("readingsCompleted", readingsProgress > 100 ? true : false);
    agentState.Add("projectWorkCompleted", projectWorkCompleted > 100 ? true : false);
    return agentState;
  }
	public override Dictionary<string, bool> getPrioritizedGoalState () {
    // while(goals.Count > 0) {
    //   Goal currentGoal = goals.Dequeue();

    // }
    return goals.Peek().GoalState;
  }
  
  public override void planAborted(Action aborter) { }
  
	public override bool moveAgent(Action nextAction){
    NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent;
    Vector3 targetPositionInPlane = new Vector3(nextAction.target.transform.position.x, this.transform.position.y, nextAction.target.transform.position.z);
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
      navMeshAgent.SetDestination(targetPositionInPlane);
      isMoving = true;
      Debug.Log(isMoving);
    }
    return false;
  }
  }
