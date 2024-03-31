using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{

    public Sprite Check;
    public Sprite Cross;

    private Image m_image;
    private bool m_animationCompleted;
    private float m_FillAmount;

    // Start is called before the first frame update
    void Start()
    {
        m_FillAmount = 0;
        m_animationCompleted = false;
        m_image = gameObject.GetComponentInChildren<Image>();
        m_image.fillAmount = m_FillAmount;
        CustomizeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Correct()
    {
        CustomizeAnimation();
        m_image.sprite = Check;

        StartCoroutine(FillingEffect());
    }

    public void Wrong()
    {
        CustomizeAnimation();
        m_image.sprite = Cross;
        StartCoroutine(FillingEffect());
    }

    public void Clear()
    {
        m_FillAmount = 0;
        m_image.fillAmount = m_FillAmount;
        m_animationCompleted = false;
    }

    public bool AnimationCompleted()
    {
        return m_animationCompleted;
    }

    IEnumerator FillingEffect()
    {
        while(m_FillAmount < 1)
        {
            m_FillAmount += 0.05f;
            m_image.fillAmount = m_FillAmount;

            yield return null;

        }

        m_animationCompleted = true;
    }

    private void CustomizeAnimation()
    {
        int fillMethod = 1;
        int origin = (int)Random.Range(0, 3);

        m_image.fillMethod = (Image.FillMethod)fillMethod;
        m_image.fillOrigin = origin;
    }
}
