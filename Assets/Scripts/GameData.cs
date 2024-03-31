using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    [System.Serializable]

    public struct CountryData
    {
        public string Name;
        public Sprite Flag;
        public bool Guessed;
        public bool BeenQuestioned;
    }

    public CountryData[] EuropeanCountryDataSet;
    public CountryData[] AfricaCountryDataSet;
    public CountryData[] AsiaCountryDataSet;
    public CountryData[] NorthAmericaCountryDataSet;
    public CountryData[] SouthAmericaCountryDataSet;
    public CountryData[] OceaniaCountryDataSet;

    [HideInInspector]
    public CountryData[] CountrySetPerGame;
    [HideInInspector]
    public CountryData[] CountryDataSet;

    public static GameData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Config.CreateScoreFile();
        }
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignArrayOfCountry()
    {
        switch (GameSettings.Instance.GetContinentType())
        {
            case GameSettings.EContinentType.E_EUROPE:
                CountryDataSet = new CountryData[EuropeanCountryDataSet.Length];
                EuropeanCountryDataSet.CopyTo(CountryDataSet, 0);
                break;

            case GameSettings.EContinentType.E_AFRICA:
                CountryDataSet = new CountryData[AfricaCountryDataSet.Length];
                AfricaCountryDataSet.CopyTo(CountryDataSet, 0);
                break;

            case GameSettings.EContinentType.E_ASIA:
                CountryDataSet = new CountryData[AsiaCountryDataSet.Length];
                AsiaCountryDataSet.CopyTo(CountryDataSet, 0);
                break;

            case GameSettings.EContinentType.E_NOTH_AMERICA:
                CountryDataSet = new CountryData[NorthAmericaCountryDataSet.Length];
                NorthAmericaCountryDataSet.CopyTo(CountryDataSet, 0);
                break;

            case GameSettings.EContinentType.E_SOUTH_AMERICA:
                CountryDataSet = new CountryData[SouthAmericaCountryDataSet.Length];
                SouthAmericaCountryDataSet.CopyTo(CountryDataSet, 0);
                break;

            case GameSettings.EContinentType.E_OCEANIA:
                CountryDataSet = new CountryData[OceaniaCountryDataSet.Length];
                OceaniaCountryDataSet.CopyTo(CountryDataSet, 0);
                break;
        }
        
    }
}
