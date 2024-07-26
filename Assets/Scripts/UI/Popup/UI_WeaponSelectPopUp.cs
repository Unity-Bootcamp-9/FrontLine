using System;
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
        GetButton((int)Buttons.Weapon1Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon1Button, 0));
        GetButton((int)Buttons.Weapon2Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon2Button, 1));
        GetButton((int)Buttons.Weapon3Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon3Button, 2));
        GetButton((int)Buttons.Weapon4Button).gameObject.BindEvent(() => SelectWeapon(Buttons.Weapon4Button, 3));

        return true;
    }

    void SelectWeapon(Buttons weapon, int weaponId)
    {
        _selectedWeapon = weapon;
        GameStart(weaponId);
    }

    void GameStart(int weaponId)
    {
        GameObject game = GameObject.Find("Game");

        if(game == null)
        {
            game = new GameObject("Game");
        }

        if(game.transform.childCount == 0)
        {
            GameObject arScene = Managers.Resource.Instantiate("AR/Game");
            arScene.transform.parent = game.transform;
        }

        game.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.player = GameObject.FindWithTag("Player").transform;

        WeaponData weaponData = Managers.DataManager.weaponDatas[weaponId];
        GameManager.Instance.SetWeapon(weaponData);
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_IngamePopUp>();
        GameManager.Instance.Initialize(Managers.DataManager.stageDatas[0]);
    }

}

