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

    enum Texts
    {
        PlayerHPText,
        BulletCheckText
    }

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
        BindText(typeof(Texts));

        GetText((int)Texts.PlayerHPText).text = $"{GameManager.Instance.currentHP}";
        GetText((int)Texts.BulletCheckText).text = $"{GameManager.Instance.GetCurrentWeapon().CheckBulletLeft()}";

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon,UIEvent.Pressed);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(onReloadWeapon, UIEvent.Click);
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionPopup);

        GetSlider((int)Sliders.PlayerHpSlider);

        if (GetSlider((int)Sliders.PlayerHpSlider) != null)
        {
            GetSlider((int)Sliders.PlayerHpSlider).interactable = false;
            GetSlider((int)Sliders.PlayerHpSlider).value = GameManager.Instance.currentHP;
        }

        GetSlider((int)Sliders.BulletCheckSlider);

        if (GetSlider((int)Sliders.BulletCheckSlider) != null)
        {
            GetSlider((int)Sliders.BulletCheckSlider).interactable = false;
            GetSlider((int)Sliders.BulletCheckSlider).maxValue = weapon.CheckBulletLeft();
            GetSlider((int)Sliders.BulletCheckSlider).value = weapon.CheckBulletLeft();
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
        GetSlider((int)Sliders.BulletCheckSlider).value = weapon.CheckBulletLeft();
    }

    private void UpdateBulletLeft(int currentBulletsCount)
    {
        GetSlider((int)Sliders.BulletCheckSlider).value = currentBulletsCount;
        GetText((int)Texts.BulletCheckText).text = $"{currentBulletsCount}";
    }

    private void UpdatePlayerHpSlider(int currentHP)
    {
        if (GetSlider((int)Sliders.PlayerHpSlider) != null)
        {
            GetSlider((int)Sliders.PlayerHpSlider).value = currentHP;
            GetText((int)Texts.PlayerHPText).text = $"{currentHP}";
        }
    }

}