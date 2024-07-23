using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Define;
using Slider = UnityEngine.UI.Slider;

public class UI_IngamePopUp : UI_Popup
{
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

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(OnClickReloadWeapon);

        Slider playerhpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        if (playerhpSlider != null)
        {
            playerhpSlider.value = 1f; //이값을 플레이어의 체력을 받아와서
            //체력을 바꾸는 메서드
        }


        return true;
    }

    void OnClickShootWeapon()
    {
        
    }

    void OnClickReloadWeapon()
    {

    }

    public void UpdatePlayerHpSlider(float hpRatio) //hpRatio는 플레이어의 현재체력 / 플레이어의 최대체력
    {
        Slider playerhpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        playerhpSlider.value = hpRatio;
    }

}