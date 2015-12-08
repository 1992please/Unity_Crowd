using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using Soomla.Store.Example;
public class MainMenuManager : MonoBehaviour
{
    // Use this for initialization
    public Text Gold;
    public Text Diamonds;
    public Text Time;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Time"))
        {
            reset();

        }
        Gold.text = "Gold: " + PlayerPrefs.GetInt("Score");
        //if (StoreWindow.instance == null)
        //    Application.LoadLevel("StartupScene");
        //StoreWindow.instance.Time = PlayerPrefs.GetInt("Time");
    }
    public void reset()
    {
        PlayerPrefs.SetInt("BestDistance", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Time", 3600);
        PlayerPrefs.SetInt("CuttentLevel", 0);
        PlayerPrefs.SetInt("DiamondsCount", 0);
        PlayerPrefs.SetInt("relifesCount", 0);
    }
    public void quit()
    {
        Application.Quit();

    }
    void Update()
    {
        //Time.text = "Time: " + StoreWindow.instance.Time;
        //Diamonds.text = "Diamond: " + StoreWindow.instance.DiamondsCount;
        if (Input.GetKey(KeyCode.Escape))
        {
            quit();
        }
    }
    //public void RemoveAdsButtonPressed()
    //{
    //    StoreWindow.instance.purchaseNoAds();
    //}
    //public void BuyTenReliefsPressed()
    //{
    //    StoreWindow.instance.purchaseRelifes();
    //}
    //public void BuyTenMinutesPressed()
    //{
    //    StoreWindow.instance.purchaseMinutes();
    //}
    //public void BuyHundDiamondsPressed()
    //{
    //    StoreWindow.instance.purchaseHundPacks();
    //}
}
