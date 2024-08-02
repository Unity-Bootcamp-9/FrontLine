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

    enum Toggles
    {
        ControllerVibration
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        

        Time.timeScale = 0f;

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));
        BindToggle(typeof(Toggles));

        GetButton((int)Buttons.BackButton).gameObject.BindEvent(GoBack);
        GetButton((int)Buttons.GameEndButton).gameObject.BindEvent(GoStageSelectPopupUI);

        GetSlider((int)Sliders.SoundSlider);

        if (GetSlider((int)Sliders.SoundSlider) != null)
        {
            GetSlider((int)Sliders.SoundSlider).interactable = true; // 슬라이더의 상호작용 비활성화
            GetSlider((int)Sliders.SoundSlider).value = AudioListener.volume; // 초기값 설정 (게임 사운드)
            GetSlider((int)Sliders.SoundSlider).onValueChanged.AddListener(SetSoundSliderValue);
        }

        GetToggle((int)Toggles.ControllerVibration);

        if (GetToggle((int)Toggles.ControllerVibration) != null)
        {
            GetToggle((int)Toggles.ControllerVibration).isOn = Managers.muteVibration;
            GetToggle((int)Toggles.ControllerVibration).onValueChanged.AddListener(SetVibrationONOFF);
        }


        return true;
    }

    void SetVibrationONOFF(bool value)
    {
        Managers.muteVibration = value;
    }

    void GoStageSelectPopupUI()
    {
        Time.timeScale = 1f;
        Managers.UI.ClosePopupUI(this);
        GameManager.Instance.ResetGame();
        Managers.UI.ShowPopupUI<UI_StageSelectPopUp>();
        Managers.SoundManager.Play(Sound.Bgm, "Sound/BackGroundMusic");
    }

    void GoBack()
    {
        Time.timeScale = 1f;
        Managers.UI.ClosePopupUI(this);
    }

    void SetSoundSliderValue(float value)
    {
        Managers.SoundManager.ChangedAudioListener(value);
    }

    
}

