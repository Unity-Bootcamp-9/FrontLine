using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_StageSelectPopUp : UI_Popup
{
    List<UI_StageItem> stageItems = new List<UI_StageItem>();

    enum Buttons
    {
        OptionButton,
        BackButton,
    }

    enum GameObjects
    {
        StageContent
    }

    private Buttons _stage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.BackButton).gameObject.BindEvent(() => GoBack());
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(() => OptionPopup());

        PopulateStage();
        return true;
    }

    void GoBack()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_StartPopUp>();
    }

    void OptionPopup()
    {
        Managers.UI.ShowPopupUI<UI_OptionPopUp>();
    }

    void PopulateStage()
    {
        stageItems.Clear();

        var parent = GetObject((int)GameObjects.StageContent);
        
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        foreach (var stageData in Managers.DataManager.stageDatas)
        {
            UI_StageItem item = Managers.UI.MakeSubItem<UI_StageItem>(parent.transform);
            item.Init();
            item.SetInfo(stageData);

            stageItems.Add(item);
        }

        UI_StageItem nullItem = Managers.UI.MakeSubItem<UI_StageItem>(parent.transform);
        nullItem.Init();
        nullItem.SetInfo(null);

        stageItems.Add(nullItem);

    }
}
