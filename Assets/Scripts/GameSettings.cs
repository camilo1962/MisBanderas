using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    private Dictionary<EContinentType, string> _SceneName = new Dictionary<EContinentType, string>();

    public enum EGameMode
    {
        NOTE_SET,
        TIME_TRAIL_MODE,
        SURVIVAL_MODE,
        SHORT_MODE
    }

    public enum EContinentType
    {
        E_NOT_SET = 0,
        E_EUROPE,
        E_AFRICA,
        E_ASIA,
        E_NOTH_AMERICA,
        E_SOUTH_AMERICA,
        E_OCEANIA,
    };

    private EGameMode _GameMode;

    private EContinentType _Continent;
    private string _ContinentName;

    public static GameSettings Instance;


    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
        
    }

    void Start()
    {
        SetSceneNameAntType();
        _GameMode = EGameMode.NOTE_SET;
        _Continent = EContinentType.E_NOT_SET;
    }

    private void SetSceneNameAntType()
    {
        _SceneName.Add(EContinentType.E_EUROPE, "GameScene");
        _SceneName.Add(EContinentType.E_AFRICA, "GameScene");
        _SceneName.Add(EContinentType.E_ASIA, "GameScene");
        _SceneName.Add(EContinentType.E_NOTH_AMERICA, "GameScene");
        _SceneName.Add(EContinentType.E_SOUTH_AMERICA, "GameScene");
        _SceneName.Add(EContinentType.E_OCEANIA, "GameScene");
    }

    public string GetContinentSceneName()
    {
        string name;

        if(_SceneName.TryGetValue(_Continent, out name))
        {
            return name;
        }
        else
        {
            Debug.Log("Error: Escena del continente no encontrada");
            return ("Error: Escena del continente no encontrada");
        }
    }


    public void SetContinentType(EContinentType type)
    {
        _Continent = type;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }

    public EGameMode GetGameMode()
    {
        return _GameMode;
    }

    public EContinentType GetContinentType()
    {
        return _Continent;
    }

    public void SetContinentName(string Name)
    {
        SetContinentType(GetContinentTypeFromString(Name));
        _ContinentName = Name;
    }

    public static EContinentType GetContinentTypeFromString(string type)
    {
        switch (type)
        {
            case "EUROPE": return EContinentType.E_EUROPE;
            case "AFRICA": return EContinentType.E_AFRICA;
            case "ASIA": return EContinentType.E_ASIA;
            case "NORTHAMERICA": return EContinentType.E_NOTH_AMERICA;
            case "SOUTHAMERICA": return EContinentType.E_SOUTH_AMERICA;
            case "OCEANIA": return EContinentType.E_OCEANIA;
            default: return EContinentType.E_NOT_SET;
        }
    }

    public static string GetContinentNameTypeFromType(EContinentType type)
    {
        switch (type)
        {
            case EContinentType.E_EUROPE: return "EUROPE";
            case EContinentType.E_ASIA: return "ASIA";
            case EContinentType.E_AFRICA: return "AFRICA";
            case EContinentType.E_NOTH_AMERICA: return "NORTHAMERICA";
            case EContinentType.E_SOUTH_AMERICA: return "SOUTHAMERICA";
            case EContinentType.E_OCEANIA: return "OCEANIA";
            default: return "NOT_SET";
        }

    }

    public static string GetGameModeNameFromType(EGameMode type)
    {
        switch (type)
        {
            case EGameMode.SHORT_MODE: return "SHORT_GAME";
            case EGameMode.SURVIVAL_MODE: return "SURVIVAL";
            case EGameMode.TIME_TRAIL_MODE: return "TIMETRAIL";
            default: return "NOT_SET";
        }
    }

    public static EGameMode GetGameModeTypeFromString(string mode)
    {
        switch (mode)
        {
            case "SHORT_GAME": return EGameMode.SHORT_MODE;
            case "SURVIVAL": return EGameMode.SURVIVAL_MODE;
            case "TIMETRAIL": return EGameMode.TIME_TRAIL_MODE;
            default: return EGameMode.NOTE_SET;
        }
        
    }
}

