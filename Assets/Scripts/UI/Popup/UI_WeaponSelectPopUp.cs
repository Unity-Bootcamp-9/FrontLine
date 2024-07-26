using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_WeaponSelectPopUp : UI_Popup
{
    List<UI_WeaponItem> weaponItems = new List<UI_WeaponItem>();

    enum Buttons
    {
        OptionButton,
        BackButton,
        NextButton,
        Weapon1Button,
        Weapon2Button,
        Weapon3Button,
        Weapon4Button
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
        FireDelayText
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

        SetWeapon(Managers.DataManager.weaponDatas[0]);
        PopulateWeapon();

        return true;
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
        GameObject game = GameObject.Find("Game");

        if (game.transform.childCount == 0)
        {
            GameObject arScene = Managers.Resource.Instantiate("AR/Game");
            arScene.transform.parent = game.transform;
        }

        game.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.player = GameObject.FindWithTag("Player").transform;
        //GameObject go = Managers.Resource.Instantiate("AR/Game");
        //go.transform.SetParent(transform);
        //GameManager.Instance.player = Camera.main.transform.GetChild(0).gameObject.transform;

        GameManager.Instance.SetWeapon(currentWeapon);
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_IngamePopUp>();
        GameManager.Instance.Initialize(Managers.DataManager.stageDatas[0]);
    }

}

