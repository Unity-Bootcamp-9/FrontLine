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
            playerhpSlider.value = 1f; //�̰��� �÷��̾��� ü���� �޾ƿͼ�
            //ü���� �ٲٴ� �޼���
        }


        return true;
    }

    void OnClickShootWeapon()
    {
        
    }

    void OnClickReloadWeapon()
    {

    }

    public void UpdatePlayerHpSlider(float hpRatio) //hpRatio�� �÷��̾��� ����ü�� / �÷��̾��� �ִ�ü��
    {
        Slider playerhpSlider = GetSlider((int)Sliders.PlayerHpSlider);
        playerhpSlider.value = hpRatio;
    }

}