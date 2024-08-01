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
    [SerializeField] private BossMonster bossMonster;

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
    private BossHPSlider bossHPSlider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        weapon = GameManager.Instance.GetCurrentWeapon();
        
        bossHPSlider = GetComponentInChildren<BossHPSlider>();
        bossHPSlider.gameObject.SetActive(false);

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

        GameManager.Instance.OnBossMonsterAppear -= OnBossAppear;
        GameManager.Instance.OnBossMonsterAppear += OnBossAppear;

        return true;
    }

    private void OnBossAppear()
    {
        bossMonster = GameManager.Instance.currentBoss;
        bossHPSlider.gameObject.SetActive(true);
        bossMonster.OnHpChanged += bossHPSlider.ChangeSliderValue;
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