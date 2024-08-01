using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BossHPSlider : MonoBehaviour
{
    [SerializeField] private Slider valueSlider;
    [SerializeField] private Slider followSlider;
    [SerializeField] private float lerpDuration = 5f;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI remaingHpText;
    [SerializeField] private int bossHPsegment;

    [SerializeField] private Image backGroundImage;
    [SerializeField] private Image valueSliderImage;
    [SerializeField] private Image followSliderImage;


    [SerializeField] private float maxValue = 100f;
    [SerializeField] private float currentValue; 
    [SerializeField] private List<Color> backgroundColors;

    private float elapsedTime = 0;
    private bool isLerping = false;

    public int backGroundIndex = 1;
    //private void Start()
    //{
    //    backGroundIndex = 1;

    //    valueSlider.maxValue = 100;
    //    followSlider.maxValue = 100;
    //    valueSlider.value = 100;
    //    followSlider.value = 100;

    //    backGroundImage.color = backgroundColors[backGroundIndex];
    //    valueSliderImage.color = backgroundColors[backGroundIndex - 1];
    //    Color followColor = backgroundColors[backGroundIndex - 1];
    //    followColor.a = 0.5f;
    //    followSliderImage.color = followColor;

    //    Color color = followSliderImage.color;
    //    color.a = 0.5f;
    //}

    public void OnEnable()
    {
        backGroundIndex = 1;

        nameText.text = GameManager.Instance.currentBoss.bossData.name;
        bossHPsegment = GameManager.Instance.currentBoss.bossData.hp / 100;
        remaingHpText.text = $"X{bossHPsegment}";

        valueSlider.maxValue = 100;
        followSlider.maxValue = 100;
        valueSlider.value = 100;
        followSlider.value = 100;

        backGroundImage.color = backgroundColors[backGroundIndex];
        valueSliderImage.color = backgroundColors[backGroundIndex - 1];
        
        Color followColor = backgroundColors[backGroundIndex - 1];
        followColor.a = 0.5f;
        followSliderImage.color = followColor;

        Color color = followSliderImage.color;
        color.a = 0.5f;
    }

    private void ChangeSlider()
    {
        if(valueSlider.value <= 0.1f)
        {
            if (backGroundIndex <= backgroundColors.Count - 2)
            {
                backGroundIndex++;
            }
            else
            {
                backGroundIndex = 1;
            }

            backGroundImage.color = backgroundColors[backGroundIndex];
            valueSliderImage.color = backgroundColors[backGroundIndex - 1]; 
            Color followColor = backgroundColors[backGroundIndex - 1];
            followColor.a = 0.5f;
            followSliderImage.color = followColor;

            valueSlider.maxValue = 100;
            followSlider.maxValue = 100;
            valueSlider.value = 100;
            followSlider.value = 100;

            bossHPsegment--;
            remaingHpText.text = $"X{bossHPsegment}";
        }
    }

    private void Update()
    {
        float difference = Mathf.Abs(valueSlider.value - followSlider.value);

        if (difference > 20f)
        {
            isLerping = true;
        }

        if (isLerping)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / lerpDuration;

            followSlider.value = Mathf.Lerp(followSlider.value, valueSlider.value, t);

            if (t >= 1f || difference < 0.5f)
            {
                isLerping = false;
                elapsedTime = 0f;
            }
        }

        ChangeSlider();
    }

    public void ChangeSliderValue(int value)
    {
        valueSlider.value -= value;
    }
}
