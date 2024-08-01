using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using static Define;

public class UI_WeaponSelectPopUp : UI_Popup
{
    List<UI_WeaponItem> weaponItems = new List<UI_WeaponItem>();

    enum Buttons
    {
        OptionButton,
        BackButton,
        NextButton
    }

    enum GameObjects
    {
        WeaponContent,
        WeaponInfoPanel,
        WeaponIcon
    }

    enum Texts
    {
        WeaponNameText,
        DamageText,
        RangeText,
        BulletCountText,
        FireDelayText,
        GoldText
    }

    private Buttons _selectedWeapon;

    private WeaponData currentWeapon;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetButton((int)Buttons.NextButton).gameObject.BindEvent(() => GameStart());
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(() => OptionPopup());
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(() => GoBack());


        SetWeapon(Managers.DataManager.weaponDatas[0]);
        PopulateWeapon();
        RefreshGold();

        return true;
    }

    void GoBack()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_StageSelectPopUp>();
        Managers.SoundManager.Play(Sound.UIEffect, "Sound/Button9");
    }

    void OptionPopup()
    {
        Managers.UI.ShowPopupUI<UI_OptionPopUp>();
        Managers.SoundManager.Play(Sound.UIEffect, "Sound/Button3");
    }

    public void PopulateWeapon()
    {
        weaponItems.Clear();

        var parent = GetObject((int)GameObjects.WeaponContent);

        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        foreach (WeaponData weaponData in Managers.DataManager.weaponDatas)
        {
            UI_WeaponItem item = Managers.UI.MakeSubItem<UI_WeaponItem>(parent.transform);
            item.Init();
            item.SetInfo(weaponData);

            weaponItems.Add(item);
        }
    }

    public void SetWeapon(WeaponData weaponData)
    {
        currentWeapon = weaponData;
        RefreshWeapon();
    }

    public void RefreshGold()
    {
        GetText((int)Texts.GoldText).text = Managers.DataManager.goldData.gold.ToString();
    }

    private void RefreshWeapon()
    {
        GetObject((int)GameObjects.WeaponIcon).GetOrAddComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/" + currentWeapon.weaponName);
        GetText((int)Texts.WeaponNameText).text = currentWeapon.weaponName;
        GetText((int)Texts.DamageText).text = "Damage : " + currentWeapon.attackDamage.ToString();
        GetText((int)Texts.RangeText).text = "Range : " + currentWeapon.range.ToString();
        GetText((int)Texts.BulletCountText).text = "BulletCount : " + currentWeapon.bulletCount.ToString();
        GetText((int)Texts.FireDelayText).text = "FireDelay : " + currentWeapon.fireDelay.ToString();

    }

    void GameStart()
    {

        if (Managers.Instance.game == null)
        {
            GameObject _game = GameObject.Find("Game");
            if (_game == null)
            {
                _game = new GameObject("Game");
                Managers.Instance.game = _game;
            }
        }

        if (Managers.Instance.game.transform.childCount == 0)
        {
            GameObject arScene = Managers.Resource.Instantiate("AR/Game");
            arScene.transform.parent = Managers.Instance.game.transform;

            ARCameraManager arCameraManager = arScene.GetComponentInChildren<ARCameraManager>();
            ARSession arSession = arScene.GetComponentInChildren<ARSession>();
            ARInputManager arInputManager = arScene.GetComponentInChildren<ARInputManager>();
            Managers.ARManager.GetARFunction(arCameraManager, arSession, arInputManager);
        }

        Managers.ARManager.SetARActive(true);

        GameManager.Instance.player = Camera.main.transform.Find("playerOffset");

        GameManager.Instance.SetWeapon(currentWeapon);
        Managers.UI.ClosePopupUI(this);
        GameManager.Instance.Initialize();
        Managers.UI.ShowPopupUI<UI_IngamePopUp>();
        Managers.SoundManager.Play(Sound.Bgm, "Sound/IngameBackGroundSound");
        Managers.SoundManager.Play(Sound.UIEffect, "Sound/Button6");
    }

}

