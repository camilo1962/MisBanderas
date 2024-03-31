using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Scores : MonoBehaviour
{

    public TextMeshProUGUI ScoreText;

    private CurrentGameData m_GameData;
    private int m_FlagNumber;

    private int m_Scores;
    private int m_WrongScores;
    private bool Initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = 0;
        m_WrongScores = 0;

    }

    private void Update()
    {
        if (!Initialized)
        {
            if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
                m_FlagNumber = 13;
            else
                m_FlagNumber = m_GameData.GetFlagNumber();

            DisplayScores();
            Initialized = true;

        }
    }

    public int GetCurrentScore() { return m_Scores; }
    public int GetCurrentWrongScore() { return m_WrongScores; }
    public int GetQuestionsNumber() { return m_FlagNumber; }

    public void AddScore()
    {
        if (m_Scores < m_FlagNumber)
            m_Scores += 1;
        DisplayScores();
    }

    public void RemoveScore()
    {
        if(m_Scores > 0)
            m_Scores -= 1;
        DisplayScores();
        
    }

    public void AddWrongScore()
    {
        if (m_WrongScores < m_FlagNumber)
            m_WrongScores += 1;
    }


    void DisplayScores()
    {
        string DisplayString = "Puntos: " + m_Scores + "/" + m_FlagNumber;
        ScoreText.text = DisplayString;
    }
    
}
