using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_StartPopUp : UI_Popup
{

    enum Buttons
    {
        GameStartButton
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.GameStartButton).gameObject.SetActive(false);

        return true;
    }

    void OnClickStartButton()
    {
        Managers.UI.ClosePopupUI(this); // UI_TitlePopup
        Managers.UI.ShowPopupUI<UI_StageSelectPopUp>();
        Managers.SoundManager.Play(Sound.UIEffect, "Sound/Button3");
    }

    public void ActiveStartButton()
    {
        GetButton((int)Buttons.GameStartButton).gameObject.SetActive(true);
    }
}