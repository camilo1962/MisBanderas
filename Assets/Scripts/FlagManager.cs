using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlagManager : MonoBehaviour
{

    public GameObject[] FlagsObjects;
    public TextMeshProUGUI DisplayText;
    private int NumberOfFlagObjects = 0;

    private CurrentGameData m_GameData;

    private bool IsFirsRun = false;


    // Start is called before the first frame update
    void Start()
    {
        NumberOfFlagObjects = FlagsObjects.Length;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        IsFirsRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFirsRun) FirstRun();
    }

    void FirstRun()
    {
        AssignFlags();
        StartCoroutine(LoopRotation(90f));
        UpdateDisplayText();
        IsFirsRun = false;
    }

    public void LoadNextGame()
    {
        StartCoroutine(LoopRotation(90f));
        m_GameData.GetNewCountries();
        UpdateDisplayText();

    }

    public void AssignFlags()
    {
        int FinalFlagPosition = (int)Random.Range(0, NumberOfFlagObjects);

        switch (FinalFlagPosition)
        {
            case 0:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                break;
            case 1:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                break;
            case 2:
                FlagsObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());

                FlagsObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagsObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagsObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                break;

        }
    }

    IEnumerator LoopRotation(float angle)
    {
        float dir = 1f;
        float rotSpeed = 90.0f;
        float startAngle = angle;
        bool assigned = false;

        while(angle > 0)
        {
            float step = Time.deltaTime * rotSpeed;
            FlagsObjects[0].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            FlagsObjects[1].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            FlagsObjects[2].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);

            if(angle <= (startAngle / 3) && assigned == false)
            {
                AssignFlags();
                assigned = true;

            }

            angle -= 2;

            yield return null;
        }
        FlagsObjects[0].GetComponent<Transform>().rotation = Quaternion.identity;
        FlagsObjects[1].GetComponent<Transform>().rotation = Quaternion.identity;
        FlagsObjects[2].GetComponent<Transform>().rotation = Quaternion.identity;

    }

    void UpdateDisplayText()
    {
        DisplayText.text = m_GameData.GetFlagName(m_GameData.GetFinalFlagIndex());
    }
}
