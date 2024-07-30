using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Define;
using static UnityEngine.Rendering.DebugUI;
using Slider = UnityEngine.UI.Slider;

public class UI_IngamePopUp : UI_Popup
{
    private Weapon weapon;

    enum Sliders
    {
        PlayerHpSlider,
        BulletCheckSlider
    }

    enum Buttons
    {
        AttackButton,
        ReloadButton,
        OptionButton
    }

    private Slider playerHpSlider;
    private Slider weaponBulletCheck;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        weapon = GameManager.Instance.GetCurrentWeapon();

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon,UIEvent.Pressed);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(onReloadWeapon, UIEvent.Click);
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionPopup);

        playerHpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        if (playerHpSlider != null)
        {
            playerHpSlider.interactable = false;
            playerHpSlider.value = GameManager.Instance.currentHP;
        }

        weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        if (weaponBulletCheck != null)
        {
            weaponBulletCheck.interactable = false;
            weaponBulletCheck.maxValue = weapon.CheckBulletLeft();
            weaponBulletCheck.value = weapon.CheckBulletLeft();
            weapon.OnBulletChanged += UpdateBulletLeft;
        }
        GameManager.Instance.OnHPChanged += UpdatePlayerHpSlider;

        return true;
    }

    void OptionPopup()
    {
        Managers.UI.ShowPopupUI<UI_OptionPopUp>();
    }

    void OnClickShootWeapon()
    {
        weapon.Fire();
    }

    void onReloadWeapon()
    {
        weapon.ReloadButton();
        weaponBulletCheck.value = weapon.CheckBulletLeft();
        Debug.Log("ÀåÀü");
    }

    private void UpdateBulletLeft(int currentBulletsCount)
    {
        weaponBulletCheck.value = currentBulletsCount;
    }


    private void UpdatePlayerHpSlider(int currentHP)
    {
        if (playerHpSlider != null)
        {
            playerHpSlider.value = currentHP;
        }
    }

}