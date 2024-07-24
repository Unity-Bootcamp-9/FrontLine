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

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(OnClickReloadWeapon);

        playerHpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        if (playerHpSlider != null)
        {
            playerHpSlider.interactable = false; // �����̴��� ��ȣ�ۿ� ��Ȱ��ȭ
            playerHpSlider.value = 1f; // �ʱⰪ ���� (�÷��̾��� ü������ ��ü)
        }

        weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        if (weaponBulletCheck != null)
        {
            weaponBulletCheck.interactable = false; // �����̴��� ��ȣ�ۿ� ��Ȱ��ȭ
            weaponBulletCheck.value = 1f; // �ʱⰪ ���� (���� ��ź���� ��ü)
        }

        weapon = FindObjectOfType<Weapon>();

        if (weapon == null)
        {
            Debug.LogError("Weapon ������Ʈ�� ã�� �� �����ϴ�.");
        }

        return true;
    }

    void OnClickShootWeapon()
    {
        weapon.Fire();
    }

    void OnClickReloadWeapon()
    {

    }


    public void UpdatePlayerHpSlider(float hpRatio) //hpRatio�� �÷��̾��� ����ü�� / �÷��̾��� �ִ�ü��
    {
        Slider playerhpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        playerhpSlider.value = hpRatio;

    }

    public void UpdateWeaponBulletCheck(int bulletcheck) //weaponBulletCheck�� ������ ������ź / ������ �ִ����� ��
    {
        Slider weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        weaponBulletCheck.value = bulletcheck;

    }

}