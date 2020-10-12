using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StudentUIController : AgentUIController {
  public Image HungerMeter;
  public Image BladderMeter;
  public Image ProductivityMeter;
  public Image LectureProgress;
  public Image ReadingProgress;
  public Image ProjectProgress;
  public Text inventoryText;

  public GameObject gameOverPanel;

  void Start () {
    gameOverPanel.SetActive(false);
  }

  public void updateAgentStats(Student student) {
    HungerMeter.fillAmount = (float)student.hunger/100.0f;
    BladderMeter.fillAmount = (float)student.bladder/100.0f;
    ProductivityMeter.fillAmount = (float)student.productivity/100.0f;
    LectureProgress.fillAmount = (float)student.lectureProgress/100.0f;
    ReadingProgress.fillAmount = (float)student.readingsProgress/100.0f;
    ProjectProgress.fillAmount = (float)student.projectWorkCompleted/100.0f;
  }

  public void updateInventoryUI(List<string> inventory) {
    string inventoryString = "inventory : ";
    foreach (string item in inventory) {
      inventoryString += item+", ";
    }
    inventoryText.text = inventoryString;
  }

  public void displayGameOverPanel () {
    if(!gameOverPanel.active) {
      gameOverPanel.SetActive(true);
    }
  }

}

