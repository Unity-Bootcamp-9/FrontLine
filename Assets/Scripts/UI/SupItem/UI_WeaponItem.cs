using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UI_WeaponItem : UI_Base
{
    enum Buttons
    {
        SelectButton
    }

    enum Texts
    {
        WeaponNameText,
        SelectText,
        WeaponInfoText
    }

    enum GameObjects
    {
        Icon
    }

    WeaponData weaponData;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.SelectButton).gameObject.BindEvent(OnClickButton);

        return true;
    }

    public void SetInfo(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        RefreshUI();
    }

    void RefreshUI()
    {
        if (_init == false)
            return;

        GetObject((int)GameObjects.Icon).GetOrAddComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/" + weaponData.weaponName);
        GetText((int)Texts.WeaponNameText).text = weaponData.weaponName;
        GetText((int)Texts.WeaponInfoText).text = weaponData.info;
        GetText((int)Texts.SelectText).text = "Select";

    }

    void OnClickButton()
    {
        Managers.UI.FindPopup<UI_WeaponSelectPopUp>().SetWeapon(weaponData);
    }
}
