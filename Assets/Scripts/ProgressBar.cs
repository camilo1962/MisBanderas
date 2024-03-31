using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Transform LoadingBar;
    public Transform TextIndicator;

    public GameSettings.EContinentType ContinentType;

    private float TargetAmount;
    private float CurrentAmount = 0.0f;
    private float Speed = 30;

    void Start()
    {
        CurrentAmount = 0.0f;
        TextIndicator.GetComponent<Text>().text = ((0).ToString() + "%");

        switch (ContinentType)
        {
            case GameSettings.EContinentType.E_EUROPE:
                {
                    float currentFlagPrc = ((int)Config.GetEuropeScore() / (float)GameData.Instance.EuropeanCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }break;
            case GameSettings.EContinentType.E_AFRICA:
                {
                    float currentFlagPrc = ((int)Config.GetAfricaScore() / (float)GameData.Instance.AfricaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_ASIA:
                {
                    float currentFlagPrc = ((int)Config.GetAsiaScore() / (float)GameData.Instance.AsiaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_NOTH_AMERICA:
                {
                    float currentFlagPrc = ((int)Config.GetNorthAmericaScore() / (float)GameData.Instance.NorthAmericaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_SOUTH_AMERICA:
                {
                    float currentFlagPrc = ((int)Config.GetSouthAmericaScore() / (float)GameData.Instance.SouthAmericaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_OCEANIA:
                {
                    float currentFlagPrc = ((int)Config.GetOceaniaScore() / (float)GameData.Instance.OceaniaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_NOT_SET:
                {
                    TargetAmount = 0.0f;
                }break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentAmount < TargetAmount)
        {
            CurrentAmount += Speed * Time.deltaTime;
            TextIndicator.GetComponent<Text>().text = (((int)CurrentAmount).ToString() + "%");
            LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
        }
    }
}
