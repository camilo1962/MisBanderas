using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardButton : MonoBehaviour
{

    public Sprite SurvivalGameIcon;
    public Sprite ShortGameIcon;
    public Sprite TimeTrailIcon;

    public Sprite EuropeButton;
    public Sprite AsiaButton;
    public Sprite AfricaButton;
    public Sprite NorthAmericaButton;
    public Sprite SouthAmericaButton;
    public Sprite OceaniaButton;

    public int ButtonIndex = 0;

    public GameObject ScoreButton;
    public GameObject GameModeIcon;
    public GameObject CorrectNumberText;
    public GameObject TotalFlagNumberText;



    void Start()
    {

        if(ButtonIndex >= Config.LastGameScores.Count)
        {
            Debug.LogError("El índice del botón es demasiado alto, no hay suficientes datos");
        }
        else
        {
            var record = Config.LastGameScores[ButtonIndex];
            var continent = GameSettings.GetContinentTypeFromString(record.continent_name);
            if (continent == GameSettings.EContinentType.E_EUROPE) ScoreButton.GetComponent<Image>().sprite = EuropeButton;
            if (continent == GameSettings.EContinentType.E_ASIA) ScoreButton.GetComponent<Image>().sprite = AsiaButton;
            if (continent == GameSettings.EContinentType.E_AFRICA) ScoreButton.GetComponent<Image>().sprite = AfricaButton;
            if (continent == GameSettings.EContinentType.E_NOTH_AMERICA) ScoreButton.GetComponent<Image>().sprite = NorthAmericaButton;
            if (continent == GameSettings.EContinentType.E_SOUTH_AMERICA) ScoreButton.GetComponent<Image>().sprite = SouthAmericaButton;
            if (continent == GameSettings.EContinentType.E_OCEANIA) ScoreButton.GetComponent<Image>().sprite = OceaniaButton;

            var game_mode_type = GameSettings.GetGameModeTypeFromString(record.game_mode_name);
            if (game_mode_type == GameSettings.EGameMode.SHORT_MODE) GameModeIcon.GetComponent<Image>().sprite = ShortGameIcon;
            if (game_mode_type == GameSettings.EGameMode.SURVIVAL_MODE) GameModeIcon.GetComponent<Image>().sprite = SurvivalGameIcon;
            if (game_mode_type == GameSettings.EGameMode.TIME_TRAIL_MODE) GameModeIcon.GetComponent<Image>().sprite = TimeTrailIcon;

            CorrectNumberText.GetComponent<Text>().text = record.correct.ToString();
            TotalFlagNumberText.GetComponent<Text>().text = record.total_flags.ToString();

        }
        
    }
        
  
}
