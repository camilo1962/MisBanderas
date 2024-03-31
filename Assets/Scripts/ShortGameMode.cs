using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortGameMode : MonoBehaviour
{

    public List<GameObject> Questions;
    public Sprite CorrectSprite;
    public Sprite WrongSprite;
    public GameObject GameOverPanel;

    public GameObject CorrectGuessedText;
    public GameObject WrongGuessedText;
    private Scores m_Scores;

    private int MaxuestionsNumber;
    private CurrentGameData m_GameData;
    private int QuestionNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        MaxuestionsNumber = Questions.Count;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;

        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
        {
            this.enabled = true;

            foreach (GameObject o in Questions)
            {
                o.SetActive(true);
            }
        }
        else
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool ShoulFinishGame()
    {
        if (QuestionNumber == (MaxuestionsNumber - 1))
            return true;
        return false;
                
    }

    public void Rotate(bool IsCorrect)
    {
        if (ShoulFinishGame())
        {
            UpdateGameHistory();
            CorrectGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
            WrongGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();


            GameOverPanel.SetActive(true);
            m_GameData.SetGameOver();

            foreach (GameObject o in Questions)
            {
                o.SetActive(false);
            }
        }

        StartCoroutine(LoopRotation(90f, IsCorrect));
    }

    IEnumerator LoopRotation(float angle, bool IsCorrect)
    {
        float dir = 1f;
        float rotSpeed = 90.0f;
        float startAngle = angle;
        bool assigned = false;

        while(angle > 0)
        {
            float step = Time.deltaTime * rotSpeed;
            Questions[QuestionNumber].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);

            if( angle <= (startAngle /3) && assigned == false)
            {
                if (IsCorrect)
                    Questions[QuestionNumber].GetComponent<SpriteRenderer>().sprite = CorrectSprite;
                else
                    Questions[QuestionNumber].GetComponent<SpriteRenderer>().sprite = WrongSprite;
                
            }
            angle -= 2;

            yield return null;
        }

        Questions[QuestionNumber].GetComponent<Transform>().rotation = Quaternion.identity;
        QuestionNumber++;

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
