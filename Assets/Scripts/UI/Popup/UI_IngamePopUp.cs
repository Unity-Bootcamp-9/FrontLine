using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_IngamePopUp : UI_Popup
{
    enum sliders
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
        BindSlider(typeof(sliders));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClickShootWeapon);
        GetButton((int)Buttons.ReloadButton).gameObject.BindEvent(OnClieckReloadWeapon);
        GetSlider((int)sliders.PlayerHpSlider).gameObject.BindEvent(PlayerHPSlider);
        GetSlider((int)sliders.BulletCheckSlider).gameObject.BindEvent(BulletCheckSlider);

        return true;
    }

    void OnClickShootWeapon()
    {
        
    }

    void OnClieckReloadWeapon()
    {

    }

    void PlayerHPSlider()
    {

    }
    
    void BulletCheckSlider()
    {

    }

}