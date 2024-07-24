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
            soundSlider.interactable = false; // 슬라이더의 상호작용 비활성화
            soundSlider.value = 1f; // 초기값 설정 (게임 사운드)
            soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        }


        return true;
    }

    void GameEnd()
    {
        //게임 종료 로직
    }

    void BackStage()
    {
        //실행 전 ui로 변경 로직
    }

    void OnSoundSliderValueChanged(float value)
    {
        Debug.Log("SoundSlider 값 변경됨: " + value);
        // 여기서 사운드 볼륨을 조절하는 코드를 추가합니다.
    }

    
    // 외부에서 사운드 슬라이더 값을 설정하는 메서드
    public void SetSoundSliderValue(float value)
    {
        if (soundSlider != null)
        {
            soundSlider.value = value;
        }
    }

}

