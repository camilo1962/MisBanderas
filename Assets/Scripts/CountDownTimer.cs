using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownTimer : MonoBehaviour
{
    public float timeLeft;
    private CurrentGameData m_GameData;

    public GUIStyle ClockStyle;

    private bool StartedGameOverTimer = false;

    //Game over
    public GUIStyle GameOverStyle;
    public GameObject GameOverPanel;
    public GameObject CorrectGuessedText;
    public GameObject WrongGuessedText;

  private Scores m_Scores;

    private bool EndGuiActivated;

    
    void Start()
    {
        StartedGameOverTimer = false;
        EndGuiActivated = false;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;

        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.TIME_TRAIL_MODE)
            this.enabled = true;
        else
            this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(EndGuiActivated && StartedGameOverTimer == false)
        {
            StartedGameOverTimer = true;
        }
    }

    private void OnGUI()
    {
        if (timeLeft > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 20, 10, 200, 100), "" + (int)timeLeft, ClockStyle);
        }
        else
        {
            if (m_GameData.HasGameFinished() == false && EndGuiActivated == true)
            {
                m_GameData.SetGameOver();
            }
            if (EndGuiActivated == false)
                 ActivateGameOverGui();
        }
    }

    private void ActivateGameOverGui()
    {
        UpdateGameHistory();
        CorrectGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
        WrongGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();

        GameOverPanel.SetActive(true);
        EndGuiActivated = true;
    }

    private void UpdateGameHistory()
    {
        Config.LastGameResult game_results = new Config.LastGameResult { };
        game_results.correct = m_Scores.GetCurrentScore();
        game_results.total_flags = m_GameData.GetFlagNumber();
        game_results.game_mode_name = GameSettings.GetGameModeNameFromType(GameSettings.Instance.GetGameMode());
        game_results.continent_name = GameSettings.GetContinentNameTypeFromType(GameSettings.Instance.GetContinentType());

        Config.UpdateLastGameScore(GameSettings.Instance.GetContinentType(), game_results);
    }


}
