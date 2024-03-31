using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressSummary : MonoBehaviour
{

    public GameObject FlagButton;
    public GameObject ButtonParent;
    public GameObject TitleText;
    public Sprite CorrectImage;
    public Sprite WrongImage;

    private int NumberOfFlags;

    void Start()
    {
        Config.UpdateScoreList();
        GameData.Instance.AssignArrayOfCountry();
        NumberOfFlags = GameData.Instance.CountryDataSet.Length;

        switch (GameSettings.Instance.GetContinentType())
        {
            case GameSettings.EContinentType.E_EUROPE: TitleText.GetComponent<Text>().text = "PROGRESIÓN EUROPA"; break;
            case GameSettings.EContinentType.E_ASIA: TitleText.GetComponent<Text>().text = "PROGRESIÓN ASIA"; break;
            case GameSettings.EContinentType.E_AFRICA: TitleText.GetComponent<Text>().text = "APROGRESIÓN AFRICA"; break;
            case GameSettings.EContinentType.E_NOTH_AMERICA: TitleText.GetComponent<Text>().text = "PROGRESIÓN NORTEAMERICA"; break;
            case GameSettings.EContinentType.E_SOUTH_AMERICA: TitleText.GetComponent<Text>().text = "PROGRESIÓN SUDAMERICA"; break;
            case GameSettings.EContinentType.E_OCEANIA: TitleText.GetComponent<Text>().text = "PROGRESIÓN OCEANIA"; break;
        }

        for (int Index = 0; Index < NumberOfFlags; Index++)
        {
            CreateButton(Index);
        }
    }

    void CreateButton(int index)
    {
        GameObject button = (GameObject)Instantiate(FlagButton);
        button.GetComponent<RectTransform>().SetParent(ButtonParent.transform, false);

        string Name = GameData.Instance.CountryDataSet[index].Name;
        int MaxLength = 14;
        int Lines = Name.Length / MaxLength;

        if(Name.Length > MaxLength)
        {
            string result = "";
            for(int LineNumber = 0; LineNumber < Lines; LineNumber++)
            {
                if (LineNumber < 1)
                    result += Name.Substring(0, MaxLength);
                int number = -1;

                for(int Counter = 0; Counter < result.Length; Counter++)
                {
                    if(result[Counter] == ' ')
                    {
                        number = Counter;
                    }
                }

                if(number >= 0)
                {
                    result = Name.Substring(0, number);
                    result += "\n";
                    result += Name.Substring(number + 1);
                    Name = result;
                }
                
            }
        }

        button.transform.GetChild(0).GetComponent<Text>().text = Name;

        if (Config.IsFlagGuessed(index))
            button.transform.GetChild(1).GetComponent<Image>().sprite = CorrectImage;
        else
            button.transform.GetChild(1).GetComponent<Image>().sprite = WrongImage;

        button.transform.GetChild(2).GetComponent<Image>().sprite = GameData.Instance.CountryDataSet[index].Flag; 
    }

   
}
