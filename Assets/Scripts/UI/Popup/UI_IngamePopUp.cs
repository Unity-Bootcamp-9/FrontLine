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
    private Weapon weapon;

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

        weapon = GameManager.Instance.GetCurrentWeapon();

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon,UIEvent.Pressed);

        playerHpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        if (playerHpSlider != null)
        {
            playerHpSlider.interactable = false;
            playerHpSlider.value = GameManager.Instance.currentHP;
            GameManager.Instance.OnHPChanged += UpdatePlayerHpSlider;
        }

        weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        if (weaponBulletCheck != null)
        {
            weaponBulletCheck.interactable = false;
            weaponBulletCheck.value = weapon.CheckBulletLeft();
            weapon.OnBulletChanged += UpdateBulletLeft;
        }

        return true;
    }


    void OnClickShootWeapon()
    {
        weapon.Fire();
        Debug.Log("น฿ป็");
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