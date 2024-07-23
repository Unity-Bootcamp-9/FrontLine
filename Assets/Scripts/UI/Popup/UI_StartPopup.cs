using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIStartPopUp : UI_Popup
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

        Button gameStartButton = GetButton((int)Buttons.GameStartButton);
        if (gameStartButton == null)
        {
            Debug.LogError("GameStartButton이 바인딩되지 않았습니다.");
            return false;
        }

        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnClickStartButton);

        return true;
    }

    void OnClickStartButton()
    {
        Managers.UI.ClosePopupUI(this); // UI_TitlePopup
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}