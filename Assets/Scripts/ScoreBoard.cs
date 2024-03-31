using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    public ScoreBoardButton ScoreBoardButton;
    public float Offset = 200.0f;
    public Transform StartYPosition;

    void Start()
    {
        Config.UpdateScoreList();
        float current_position = StartYPosition.position.y;

        for(int index = 0; index < Config.LastGameScores.Count; index++)
        {
            var game_object = Instantiate(ScoreBoardButton, this.transform) as ScoreBoardButton;
            game_object.ButtonIndex = index;
            game_object.transform.position = new Vector3(this.transform.position.x, current_position, this.transform.position.z);
            current_position -= Offset;
            current_position -= ScoreBoardButton.GetComponent<Image>().rectTransform.rect.height * ScoreBoardButton.GetComponent<Image>().rectTransform.localScale.y;
        }
    }
}
