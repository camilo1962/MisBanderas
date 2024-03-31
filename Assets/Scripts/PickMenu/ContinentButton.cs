using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ContinentButton : MonoBehaviour
{

    public string Name;

    public void OnClick()
    {
        GameSettings.Instance.SetContinentName(Name);
    }


}
