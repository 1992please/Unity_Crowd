using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public int[] moneyObjective;
    public int[] distanceObjective;
    public int currentLevel;
    public Text chapterName;

    public int noOfLevels;
    
    // Use this for initialization
    void Awake()
    {
        noOfLevels = moneyObjective.Length;
        currentLevel = PlayerPrefs.GetInt("CuttentLevel");
        chapterName.text = "Chapter: " + currentLevel;
    }
    public bool CheckFinish(int distance)
    {
        if (currentLevel == noOfLevels)
            return true;
        if (distance >= distanceObjective [currentLevel])
        {
            chapterName.text = "Chapter: " + currentLevel;
            return true;
        }
        return false;
    }
    public bool checkEnoughMoney(int money)
    {
        if (money >= moneyObjective [currentLevel])
        {
            return true;
        }
        return false;
    }
    public bool checkFinalLevel()
    {
        if (currentLevel >= noOfLevels)
        {
            return true;
        }
        return false;
    }
}
