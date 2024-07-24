using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using static Define;

public class UI_OptionPopUp : UI_Popup
{
    enum Buttons
    {
        BackButton,
        GameEndButton
    }

    enum Sliders
    {
        SoundSlider,
        BrightSlider
    }

    private Slider soundSlider;
    private Slider brightslider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.BackButton).gameObject.BindEvent(BackStage);
        GetButton((int)Buttons.GameEndButton).gameObject.BindEvent(GameEnd);

        soundSlider = GetSlider((int)Sliders.SoundSlider);

        if (soundSlider != null)
        {
            soundSlider.interactable = false; // �����̴��� ��ȣ�ۿ� ��Ȱ��ȭ
            soundSlider.value = 1f; // �ʱⰪ ���� (���� ����)
            soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        }


        return true;
    }

    void GameEnd()
    {
        //���� ���� ����
    }

    void BackStage()
    {
        //���� �� ui�� ���� ����
    }

    void OnSoundSliderValueChanged(float value)
    {
        Debug.Log("SoundSlider �� �����: " + value);
        // ���⼭ ���� ������ �����ϴ� �ڵ带 �߰��մϴ�.
    }

    
    // �ܺο��� ���� �����̴� ���� �����ϴ� �޼���
    public void SetSoundSliderValue(float value)
    {
        if (soundSlider != null)
        {
            soundSlider.value = value;
        }
    }

}

