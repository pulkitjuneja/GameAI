using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AgentUIController : MonoBehaviour {
  public Text currentGoalText;

  public void updateCurrentGoalUI(Goal goal) {
    currentGoalText.text = "current Goal: " + goal.name;
  }

}

