using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGameProgressButton : MonoBehaviour
{
    public Sprite DoneIcon;
    public GameObject CentreTargetImage;

    public Transform LoadingBar;
    public Transform TextIndicator;

    private float TargetAmount = 100;
    private float CurrentAmount = 0.0f;

    private float Speed = 50;
    private bool Clicked = false;

    void Start()
    {
        Clicked = false;
        TextIndicator.GetComponent<Text>().text = ((int)CurrentAmount).ToString() + "%";
        LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
        TextIndicator.gameObject.SetActive(false);
    }

  
    void Update()
    {
        if (Clicked)
        {
            if(CurrentAmount < TargetAmount)
            {
                CurrentAmount += Speed * Time.deltaTime;
                TextIndicator.GetComponent<Text>().text = ((int)CurrentAmount).ToString() + "%";
                LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
            }
            else
            {
                TextIndicator.gameObject.SetActive(false);
                LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
                CentreTargetImage.GetComponent<Image>().sprite = DoneIcon;
                Clicked = false;
            }
        }

    }

    public void OnClicked()
    {
        Clicked = true;
        TextIndicator.gameObject.SetActive(true);
    }
}
