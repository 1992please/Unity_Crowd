using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int money;
    public int distance;
    public int moneyToMakeHappy;
    public int timeToMakeHappy;
    public GameObject losePanel;
    public GameObject startPanel;
    public GameObject finishLevelPanel;
    public GameObject finishGamePanel;
    public GameObject respawnButton;
    public PlayerController PC;
    public BlocksController BC;
    public LevelController LC;
    public Text moneyText;
    public Text timeText;
    public Text distanceText;
    public Text statusText;
    private int time = 3600;
    private int minuteTime;
    private int secTime;
    private int bestDistance;
    private bool bestDistanceFlag;
    private bool stopUpdatingScoreBoard;
    // Use this for initialization
    void Start()
    {
        if (LC.checkFinalLevel())
        {
            finishGamePanel.SetActive(true);
        }
        stopUpdatingScoreBoard = true;
        time = PlayerPrefs.GetInt("Time");
        money = PlayerPrefs.GetInt("Score");
        StartCoroutine(clock());
        Time.timeScale = 0;
        //    BC.LoadBlocks();
    }
    // Update is called once per frame
    void Update()
    {
        if (!stopUpdatingScoreBoard)
        {
            UpdateScoreBoard();
        }
    }
    public bool MakeAHappyMan(animateScript happyAnim)
    {
        if (money >= moneyToMakeHappy && time >= timeToMakeHappy)
        {
            money -= moneyToMakeHappy;
            time -= timeToMakeHappy;
            happyAnim.runAnimation();
            // UpdateScoreBoard();
            return true;
        }
        else
        {
            statusText.gameObject.SetActive(true);
            statusText.text = "Not Enough Resources";
            return false;
        }
    }
    public void getGoldenCoin()
    {
        money++;
        // UpdateScoreBoard();
    }
    public void getBreakCoin()
    {

    }
    public void getFloatingCoin()
    {

    }
    public void startOrReset()
    {
        respawnButton.SetActive(true);
        bestDistance = PlayerPrefs.GetInt("BestDistance");
        bestDistanceFlag = true;
        if (bestDistance > 0)
        {
            bestDistanceFlag = true;
            BC.SetBestDistanceFlagPosition(bestDistance);
        }
        PC.startOrReset();
        BC.startOrReset();
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        stopUpdatingScoreBoard = true;
    }
    public void startGame()
    {
        Time.timeScale = 1;
        stopUpdatingScoreBoard = false;
        startOrReset();
    }
    public void respawn()
    {
        Time.timeScale = 1;
        stopUpdatingScoreBoard = false;
        StartCoroutine(PC.respawn());
    }
    public void lose()
    {
        if (!bestDistanceFlag)
        {
            PlayerPrefs.SetInt("BestDistance", distance);
        }
        PlayerPrefs.SetInt("Score", money);
        PlayerPrefs.SetInt("Time", time);
        losePanel.SetActive(true);
        stopUpdatingScoreBoard = true;

        Time.timeScale = 0;
    }
    private void triggerFinishLevelRoutine()
    {
        BC.finish = true;
        if (LC.checkEnoughMoney(money))
        {
            PC.canFinish = true;
        }
    }
    private void SaveProgress()
    {
        if (!bestDistanceFlag)
        {
            PlayerPrefs.SetInt("BestDistance", distance);
        }
        PlayerPrefs.SetInt("Score", money);
        PlayerPrefs.SetInt("Time", time);
        PlayerPrefs.SetInt("CuttentLevel", ++LC.currentLevel);
    }
    public void UpdateScoreBoard()
    {

        moneyText.text = money + "$";
        timeText.text = minuteTime + ":" + secTime;
        distance = (int)(BC.blocksDistance + PC.transform.position.x) / 10;
        distanceText.text = (int)distance + "";
        if (distance > bestDistance && bestDistanceFlag)
        {
            statusText.gameObject.SetActive(true);
            statusText.text = "New Distance Record";
            bestDistanceFlag = false;
        }
        if (LC.CheckFinish(distance))
            triggerFinishLevelRoutine();
    }
    IEnumerator clock()
    {
        while (true)
        {
            minuteTime = time / 60;
            secTime = time - time / 60 * 60;
            //UpdateScoreBoard();
            yield return new WaitForSeconds(1);
            time--;
        }
    }
    public void FinishLevelRoutine()
    {
        Time.timeScale = 0;
        stopUpdatingScoreBoard = true;

        SaveProgress();
        if (LC.checkFinalLevel())
        {
            finishGamePanel.SetActive(true);
        }
        else
        {
            finishLevelPanel.SetActive(true);
        }
    }

}
