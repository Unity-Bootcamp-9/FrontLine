using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageItem : UI_Base
{
    enum Buttons
    {
        StageSelectButton
    }

    enum Texts
    {
        StageText
    }

    private StageData stageData;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.StageSelectButton).gameObject.BindEvent(OnClickSelect);
        return true;
    }

    private void OnClickSelect()
    {
        GameManager.Instance.SelectStage(stageData);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_WeaponSelectPopUp>();
    }

    public void SetInfo(StageData? stageData)
    {
        if (stageData != null)
        {
            this.stageData = (StageData)stageData;
            GetText((int)Texts.StageText).text = "Stage " + this.stageData.stage.ToString();
            GetButton((int)Buttons.StageSelectButton).image.sprite = Managers.Resource.Load<Sprite>("Stage/Stage" + this.stageData.stage.ToString());
        }
        else
        {
            GetText((int)Texts.StageText).text = "Developing";
            GetButton((int)Buttons.StageSelectButton).interactable = false;
            GetButton((int)Buttons.StageSelectButton).image.raycastTarget = false;
            GetButton((int)Buttons.StageSelectButton).image.sprite = Managers.Resource.Load<Sprite>("Stage/StageNull");
        }
    }

}
