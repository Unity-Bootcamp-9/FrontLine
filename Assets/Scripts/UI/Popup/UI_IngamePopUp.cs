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
            playerHpSlider.interactable = false; // 슬라이더의 상호작용 비활성화
            playerHpSlider.value = 1f; // 초기값 설정 (플레이어의 체력으로 대체)
        }

        weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        if (weaponBulletCheck != null)
        {
            weaponBulletCheck.interactable = false; // 슬라이더의 상호작용 비활성화
            weaponBulletCheck.value = 1f; // 초기값 설정 (무기 잔탄으로 대체)
        }

        weapon = FindObjectOfType<Weapon>();

        if (weapon == null)
        {
            Debug.LogError("Weapon 컴포넌트를 찾을 수 없습니다.");
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


    public void UpdatePlayerHpSlider(float hpRatio) //hpRatio는 플레이어의 현재체력 / 플레이어의 최대체력
    {
        Slider playerhpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        playerhpSlider.value = hpRatio;

    }

    public void UpdateWeaponBulletCheck(int bulletcheck) //weaponBulletCheck는 무기의 현재잔탄 / 무기의 최대장전 수
    {
        Slider weaponBulletCheck = GetSlider((int)Sliders.BulletCheckSlider);
        weaponBulletCheck.value = bulletcheck;

    }

}