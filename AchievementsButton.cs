using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System;

public class AchievementsButton : MonoBehaviour
{
    public GameObject achievementsPanel;
    public Text achievementsText;

    public void ShowAchievements()
    {
        
        string achievementData = "Achievement 1: Completed\nAchievement 2: In progress\nAchievement 3: Locked";

        
        achievementsText.text = achievementData;

        
        achievementsPanel.SetActive(true);
    }

    public void HideAchievements()
    {
        
        achievementsPanel.SetActive(false);
    }
}
