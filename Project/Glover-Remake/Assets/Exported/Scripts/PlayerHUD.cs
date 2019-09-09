using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    public Text HealthText;
    public Text PowerText;
    public Text LevelText;
    public Text GameInfo;
    // refrences
    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        GameInfo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerText();
    }

    public void UpdatePlayerText()
    {
        HealthText.text = "HP " + stats.currentHP.ToString();
        PowerText.text = "POWER " + stats.Power.ToString();
        LevelText.text = "LV " + stats.Level.ToString();
    }

    public void DisplayGameInfo(string info,float timeDelay)
    {
        GameInfo.text = info;

        Invoke("DisableGameInfoText", timeDelay);
    }

    public void DisableGameInfoText()
    {
        GameInfo.text = "";
    }
}
