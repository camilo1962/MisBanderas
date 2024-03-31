using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGameData : MonoBehaviour
{
    [HideInInspector]
    public int FirstFlagIndex = 0;
    [HideInInspector]
    public int SecondFlagIndex;
    [HideInInspector]
    public int FinalFlagIndex;

    private int PrevFinalFlagIndex;
    private int CountriesPerGame = 60; // Nyumero de paises maxomo que deberian estar en el juego.
    private bool GameFinished = false;

    public void ResetGameOver() { GameFinished = false; }

    public bool HasGameFinished() { return GameFinished; }

    public void SetGameOver()
    {
        GameFinished = true;

        if (GameFinished)
        {
            int NumberOfFlags = GameData.Instance.CountrySetPerGame.Length;
            Config.UpdateScoreList();
            for(int i=0; i< NumberOfFlags; i++)
            {
                Config.SaveScore(i, GameData.Instance.CountrySetPerGame[i].Guessed, (int)GameSettings.Instance.GetContinentType());
            }

            Config.SaveScoreList();
            Config.UpdateScoreList();
        }
    }
    

    void Start()
    {
        FinalFlagIndex = 0;
        PrevFinalFlagIndex = 0;
        FirstFlagIndex = 0;
        SecondFlagIndex = 0;
        GameData.Instance.AssignArrayOfCountry();

        if (CountriesPerGame >= GameData.Instance.CountryDataSet.Length)
            CountriesPerGame = GameData.Instance.CountryDataSet.Length;

        GameData.Instance.CountrySetPerGame = new GameData.CountryData[CountriesPerGame];

        GameFinished = false;

        PickCountriesForGame();
        GetNewCountries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickCountriesForGame()
    {
        int PickedCountryNumber = 0;

        for (int Index = 0; Index < GameData.Instance.CountryDataSet.Length; Index++)
        {
            if (PickedCountryNumber >= CountriesPerGame)
                break;

            else
            {
                if (GameData.Instance.CountryDataSet[Index].Guessed == false)
                {
                    GameData.Instance.CountrySetPerGame[PickedCountryNumber] = GameData.Instance.CountryDataSet[Index];
                    PickedCountryNumber++;
                }
            }
        
        }

        if(PickedCountryNumber < CountriesPerGame - 1)
        {

      
              for (int Index = 0; Index < GameData.Instance.CountryDataSet.Length; Index++)
              {
                  if (PickedCountryNumber >= CountriesPerGame)
                      break;
              
                  else
                  {
                      if (GameData.Instance.CountryDataSet[Index].Guessed == true)
                      {
                        GameData.Instance.CountrySetPerGame[PickedCountryNumber] = GameData.Instance.CountryDataSet[Index];
                        PickedCountryNumber++;
                    }
                  }
              
              }
        }
    }

    public void GetNewCountries()
    {
        PrevFinalFlagIndex = FinalFlagIndex;

        if (GetNumberOfFlagsLeft() > 0)
        {
            do
            {
                FinalFlagIndex = (int)Random.Range(0, GameData.Instance.CountrySetPerGame.Length);
            } while (PrevFinalFlagIndex == FinalFlagIndex || GameData.Instance.CountrySetPerGame[FinalFlagIndex].Guessed == true);

            do
            {
                FirstFlagIndex = (int)Random.Range(0, GameData.Instance.CountrySetPerGame.Length);
            } while (FirstFlagIndex == FinalFlagIndex);

            do
            {
                SecondFlagIndex = (int)Random.Range(0, GameData.Instance.CountrySetPerGame.Length);
            } while (SecondFlagIndex == FirstFlagIndex || SecondFlagIndex == FinalFlagIndex);

            GameData.Instance.CountrySetPerGame[FinalFlagIndex].BeenQuestioned = true;

        }
        else
        {
            GameFinished = true;

        }
    }

    private int GetNumberOfFlagsLeft()
    {
        int left = 0;

        for(int Index = 0; Index < GameData.Instance.CountrySetPerGame.Length; Index++)
        {
            if (GameData.Instance.CountrySetPerGame[Index].Guessed == false)
                left++;
        }

        return left;
    }

    public string GetFlagName(int index)
    {
        return GameData.Instance.CountrySetPerGame[index].Name;
    }

    public int GetFlagNameLenght(int FlagIndex)
    {
        return GameData.Instance.CountrySetPerGame[FlagIndex].Name.Length;
    }

    public int GetFirstFlagIndex()
    {
        return FirstFlagIndex;
    }

    public int GetSecondFlagIndex()
    {
        return SecondFlagIndex;
    }

    public int GetFinalFlagIndex()
    {
        return FinalFlagIndex;
    }

    public void SetGuessed()
    {
        GameData.Instance.CountrySetPerGame[FinalFlagIndex].Guessed = true;
    }

    public Sprite GetFlagSpriteIndex(int FlagIndex)
    {
        return GameData.Instance.CountrySetPerGame[FlagIndex].Flag;
    }

    public int GetFlagNumber() { return GameData.Instance.CountryDataSet.Length; }
}

