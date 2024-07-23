using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_StageSelectPopUp : UI_Popup
{
    enum Buttons
    {
        OptionButton,
        BackButton,
        StageButton1,
        StageButton2,
        StageButton3
    }

    private Buttons _stage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        GetButton((int)Buttons.StageButton1).gameObject.BindEvent(() => SelectStage(Buttons.StageButton1));
        GetButton((int)Buttons.StageButton2).gameObject.BindEvent(() => SelectStage(Buttons.StageButton2));
        GetButton((int)Buttons.StageButton3).gameObject.BindEvent(() => SelectStage(Buttons.StageButton3));

        return true;
    }

    void SelectStage(Buttons stage)
    {
        _stage = stage;
        StageToWeapon();
    }

    void StageToWeapon()
    {
        switch (_stage)
        {
            case Buttons.StageButton1:
                Debug.Log("Stage 1 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_WeaponSelectPopUp>();
                break;
            case Buttons.StageButton2:
                Debug.Log("Stage 2 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_WeaponSelectPopUp>();
                break;
            case Buttons.StageButton3:
                Debug.Log("Stage 3 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_WeaponSelectPopUp>();
                break;
            default:
                Debug.LogWarning("Invalid Stage Selected");
                break;
        }
    }
}
