using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Define;
using Slider = UnityEngine.UI.Slider;

public class UI_IngamePopUp : UI_Popup
{
    public Weapon weapon;

    enum Sliders
    {
        PlayerHpSlider,
        BulletCheckSlider
    }

    enum Buttons
    {
        AttackButton,
        ReloadButton
    }

    private Slider playerHpSlider;
    private Slider weaponBulletCheck;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon,UIEvent.Pressed);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(OnClickReloadWeapon);

        playerHpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        if (playerHpSlider != null)
        {
            playerHpSlider.interactable = false;
            playerHpSlider.value = GameManager.Instance.currentHP;
            GameManager.Instance.OnHPChanged += UpdatePlayerHpSlider;
        }

        weapon = GameManager.Instance.GetCurrentWeapon();

        if (weapon == null)
        {
            Debug.LogError("Weapon 컴포넌트를 찾을 수 없습니다.");
        }

        return true;
    }

    void OnClickShootWeapon()
    {
        weapon.Fire();
        Debug.Log("발사");
    }

    void OnClickReloadWeapon()
    {

    }

    private void UpdatePlayerHpSlider(int currentHP)
    {
        if (playerHpSlider != null)
        {
            playerHpSlider.value = currentHP;
        }
    }

    //public void RefreshUI()
    //{
    //    if (_init == false)
    //        return;

    //    int value = Utils.GetStatValue(_statType);

    //    GetText((int)Texts.TitleText).text = Managers.GetText(_statData.nameID);
    //    GetText((int)Texts.ChangeText).text = $"{value} → {GetIncreasedValue()}";
    //    GetText((int)Texts.MoneyText).text = Utils.GetMoneyString(_statData.price);

    //    if (_statType == StatType.Luck)
    //        GetText((int)Texts.ChangeText).text = $"{Managers.Game.Luck}";

    //    if (CanUpgrade())
    //        GetButton((int)Buttons.UpgradeButton).interactable = true;
    //    else
    //        GetButton((int)Buttons.UpgradeButton).interactable = false;

    //    if (_statType == StatType.Luck)
    //        GetButton((int)Buttons.UpgradeButton).gameObject.SetActive(false);

    //    GetText((int)Texts.DiffText).gameObject.SetActive(false);
    //}

}