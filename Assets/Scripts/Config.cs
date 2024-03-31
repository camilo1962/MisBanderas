using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

using System.Linq;

public class Config : MonoBehaviour
{
    static string dir = Application.persistentDataPath;
    //static string dir = Directory.GetCurrentDirectory();
      

    static string file = @"\flags.txt";
    static string path = dir + file;//Application.dataPath + @"\flags.txt";

    private static bool DebugMode = true;
    private static int numberOfScoresRecord;
    public static List<int> ScoreList;

    private static List<int> ContinentsScores = new int[6].ToList();
    private static List<int> ContinetLengts;
    private const int NumberOfContinents = 6;

    //Histori

    private static int Max_History_Records = 6;
    private static char History_Divider = '.';

    public class LastGameResult
    {
        public int correct = 0;
        public int total_flags = 0;
        public string game_mode_name = "";
        public string continent_name = "";
    }

    public static List<LastGameResult> LastGameScores = new List<LastGameResult>();

    public static int GetEuropeScore() { return ContinentsScores[0]; }

    public static int GetAsiaScore() { return ContinentsScores[1]; }

    public static int GetAfricaScore() { return ContinentsScores[2]; }

    public static int GetNorthAmericaScore() { return ContinentsScores[3]; }

    public static int GetSouthAmericaScore() { return ContinentsScores[4]; }

    public static int GetOceaniaScore() { return ContinentsScores[5]; }

    public static void UpdateLastGameScore(GameSettings.EContinentType continent, LastGameResult continent_game_result)
    {
        var continent_name = GameSettings.GetContinentNameTypeFromType(continent);
        LastGameScores.Insert(0, continent_game_result);
        SaveScoreList();

    }

    public static void ClearData()
    {
        for(int i =0; i < numberOfScoresRecord; i++)
        {
            ScoreList[i] = 0;
        }
    }


    public static void CreateScoreFile()
    {
        for (int i = 0; i < NumberOfContinents; i++)
        {
            ContinentsScores[i] = 0;
        }
        
        ContinetLengts = new int[NumberOfContinents]
        {
            GameData.Instance.EuropeanCountryDataSet.Length,
            GameData.Instance.AsiaCountryDataSet.Length,
            GameData.Instance.AfricaCountryDataSet.Length,
            GameData.Instance.NorthAmericaCountryDataSet.Length,
            GameData.Instance.SouthAmericaCountryDataSet.Length,
            GameData.Instance.OceaniaCountryDataSet.Length,
        }.ToList();

        foreach(var continent_length in ContinetLengts)
        {
            numberOfScoresRecord += continent_length;
        }

        numberOfScoresRecord += NumberOfContinents;
        ScoreList = new int[numberOfScoresRecord].ToList();

        if(File.Exists(path) == false)
        {
            ClearData();
            SaveScoreList();
        }

        UpdateScoreList();

    }

    public static void SaveScoreList()
    {
        int current_continent = 0;
        int flag_index = 0;

        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);

        for(int i = 0; i < numberOfScoresRecord; i++)
        {
            if((i == 0) || ((current_continent <= NumberOfContinents) && (flag_index == ContinetLengts[current_continent])))
            {
                if (DebugMode)
                {
                    string DebugContinentName = "";

                    if (i == 0)
                        DebugContinentName = "//EUROPE";
                    else
                    {
                        switch (current_continent)
                        {
                            case 0: DebugContinentName = "//ASIA"; break;
                            case 1: DebugContinentName = "//AFRICA"; break;
                            case 2: DebugContinentName = "//NORTHAMERICA"; break;
                            case 3: DebugContinentName = "//SOUTHAMERICA"; break;
                            case 4: DebugContinentName = "//OCEANIA"; break;

                        }
                    }

                    writer.WriteLine("#." + current_continent.ToString() + DebugContinentName);
                }
                else
                    writer.WriteLine("#." + current_continent.ToString());

                if (i > 0)
                    current_continent++;

                flag_index = 0;
            }
            else
            {
                if (DebugMode)
                {
                    string DebugCountryName = "";
                    switch (current_continent)
                    {
                        case 0: DebugCountryName = GameData.Instance.EuropeanCountryDataSet[flag_index].Name; break;
                        case 1: DebugCountryName = GameData.Instance.AsiaCountryDataSet[flag_index].Name; break;
                        case 2: DebugCountryName = GameData.Instance.AfricaCountryDataSet[flag_index].Name; break;
                        case 3: DebugCountryName = GameData.Instance.NorthAmericaCountryDataSet[flag_index].Name; break;
                        case 4: DebugCountryName = GameData.Instance.SouthAmericaCountryDataSet[flag_index].Name; break;
                        case 5: DebugCountryName = GameData.Instance.OceaniaCountryDataSet[flag_index].Name; break;

                    }

                    writer.WriteLine(i.ToString() + "." + ScoreList[i].ToString() + "D" + "            //" + DebugCountryName);
                }
                else
                {
                    writer.WriteLine(i.ToString() + "." + ScoreList[i].ToString() + "D");
                }
                    
                flag_index++;
            }
        }

        //Wrirte Game History
        int current_record_index = 0;
        foreach(var record in LastGameScores)
        {
            if(current_record_index < Max_History_Records)
            {
                string record_str;
                record_str = "#H" + current_record_index.ToString() + History_Divider
                    + record.game_mode_name + History_Divider
                    + record.continent_name + History_Divider
                    + record.correct.ToString() + History_Divider
                    + record.total_flags.ToString();

                writer.WriteLine(record_str);
            }
            current_record_index++;
        }

        writer.Close();
    }

    public static void UpdateScoreList()
    {
        LastGameScores.Clear();
        ScoreList.Clear();
        StreamReader file = new StreamReader(path);
        string line;
        while((line = file.ReadLine()) != null)
        {

                if (line[0] != '#')
                {
                    string[] line_part = line.Split('.');
                        string[] part_substring = Regex.Split(line_part[1], "D");
                        int score;
                        if (int.TryParse(part_substring[0], out score))
                             ScoreList.Add(score);
                        else
                            ScoreList.Add(0);
                }
                else
                    ScoreList.Add(4);

                //Read History Records
                if(line[0] == '#' && line[1] == 'H')
                {
                    string[] record_line = line.Split(History_Divider);
                    LastGameResult record = new LastGameResult();
                    record.game_mode_name = record_line[1];
                    record.continent_name = record_line[2];
                    if (int.TryParse(record_line[3], out record.correct) == false) record.correct = 0;
                    if (int.TryParse(record_line[4], out record.total_flags) == false) record.total_flags = 0;

                    LastGameScores.Add(record);
                }
        }

        file.Close();
        UpdateContinentScores();
    }

    private static void UpdateContinentScores()
    {
        for(int i = 0; i < NumberOfContinents; i++)
        {
            ContinentsScores[i] = 0;
        }

        int SearchingContinent = 0;
        int lastPosition = FindLastPositionInContinent(SearchingContinent);

        for(int i = 0; i < numberOfScoresRecord; i++)
        {
            if (i <= lastPosition && (ScoreList[i] == 1))
            {
                ContinentsScores[SearchingContinent]++;
            }
            else if(i > lastPosition)
            {
                if (SearchingContinent < 5)
                    SearchingContinent++;
                lastPosition = FindLastPositionInContinent(SearchingContinent);
                lastPosition += SearchingContinent;
                    
            }
        }
    }

    private static int FindLastPositionInContinent(int continent_index)
    {
        int position = 0;
        for(int i = 0; i <=continent_index; i++)
        {
            position += ContinetLengts[i];
        }
        return position;
    }

    public static void SaveScore(int FlagIndex, bool Correct, int ContinentIndex)
    {
        int FirstPosition = FindPositionOfFirsFlagContinent();

        if(ContinentIndex == 1)//Europe
        {
            if (Correct && (ScoreList[FirstPosition + FlagIndex] == 0))
                ScoreList[FirstPosition + FlagIndex] = 1;
        }
        else
        {
            if (Correct && (ScoreList[FirstPosition + (FlagIndex + ContinentIndex)] == 0))
                ScoreList[FirstPosition + (FlagIndex + ContinentIndex)] = 1;
        }
    }

    private static int FindPositionOfFirsFlagContinent()
    {
        int ContinentIndex = (int)GameSettings.Instance.GetContinentType() - 1;
        int position = 0;
        for (int i =0; i < ContinentIndex; i++)
        {
            position += ContinetLengts[i];
        }
        if (ContinentIndex == 0)
            position += 1;
        return position;
       
    }

    public static void ResetGameProgress()
    {
        ClearData();
        SaveScoreList();
        UpdateScoreList();
    }

    public static bool IsFlagGuessed(int flagIndex)
    {
        bool Correct = false;
        int continentIndex = (int) GameSettings.Instance.GetContinentType() - 1;
        int SearchingFlagIndex = FindFirsPositionOfContinent((int)GameSettings.Instance.GetContinentType() - 1);

        if (continentIndex == 0)
            SearchingFlagIndex += flagIndex;
        else
            SearchingFlagIndex += flagIndex + 1;

        if (ScoreList[SearchingFlagIndex] == 1)
            Correct = true;

        return Correct;
    }

    private static int FindFirsPositionOfContinent(int searchingCOntinentIndex)
    {
        int ContinentIndex = searchingCOntinentIndex;
        int position = 0;

        for (int i=0; i< ContinentIndex; i++)
        {
            position += ContinetLengts[i];
            position += 1;
        }
        if (ContinentIndex == 0)
            position += 1;
        return position;
    }
}
