using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UI_WeaponItem : UI_Base
{
    enum Buttons
    {
        SelectButton,
        BuyButton
    }

    enum Texts
    {
        WeaponNameText,
        WeaponInfoText,
        SelectText,
        BuyText
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

        GetButton((int)Buttons.SelectButton).gameObject.BindEvent(OnClickSelectButton);
        
        GetButton((int)Buttons.BuyButton).gameObject.BindEvent(OnClickBuyButton);

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
        GetText((int)Texts.BuyText).text = "Buy";

        GetButton((int)Buttons.SelectButton).gameObject.SetActive(weaponData.isOwn);
        GetButton((int)Buttons.BuyButton).gameObject .SetActive(!weaponData.isOwn);
    }

    void OnClickSelectButton()
    {
        Managers.UI.FindPopup<UI_WeaponSelectPopUp>().SetWeapon(weaponData);
    }

    void OnClickBuyButton()
    {
        if(Managers.DataManager.goldData.gold >= weaponData.price)
        {
            // 구매
            Managers.DataManager.ExportGold(-weaponData.price);
            Managers.UI.FindPopup<UI_WeaponSelectPopUp>().RefreshGold();
            weaponData.isOwn = true;
            RefreshUI();
        }
        else
        {
            // 구매 불가

        }
    }
}
