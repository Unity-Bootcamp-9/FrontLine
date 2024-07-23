using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_WeaponSelectPopUp : UI_Popup
{
    enum Buttons
    {
        OptionButton,
        BackButton,
        Weapon1Button,
        Weapon2Button,
        Weapon3Button,
        Weapon4Button
    }

    private Buttons _selectedWeapon;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        GetButton((int)Buttons.Weapon1Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon1Button));
        GetButton((int)Buttons.Weapon2Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon2Button));
        GetButton((int)Buttons.Weapon3Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon3Button));
        GetButton((int)Buttons.Weapon4Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon4Button));

        return true;
    }

    void SelectWeapon(Buttons weapon)
    {
        _selectedWeapon = weapon;
        GameStart();
    }

    void GameStart()
    {
        switch (_selectedWeapon)
        {
            case Buttons.Weapon1Button:
                Debug.Log("Weapon 1 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_IngamePopUp>();
                break;
            case Buttons.Weapon2Button:
                Debug.Log("Weapon 2 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_IngamePopUp>();
                break;
            case Buttons.Weapon3Button:
                Debug.Log("Weapon 3 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_IngamePopUp>();
                break;
            case Buttons.Weapon4Button:
                Debug.Log("Weapon 4 Selected");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_IngamePopUp>();
                break;
            default:
                Debug.LogWarning("Invalid Weapon Selected");
                break;
        }
    }
}
