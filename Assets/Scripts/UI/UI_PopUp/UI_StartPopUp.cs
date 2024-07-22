using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPopUp : UI_Popup
{

    enum Buttons
    {
        StartButton
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        
        return true;
    }

    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        if (true)
        {
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_StageSelectPopUp>();
           
        }
    }

}
