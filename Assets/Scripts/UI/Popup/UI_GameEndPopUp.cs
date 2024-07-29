using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameEndPopUp : UI_Popup
{
    enum Texts
    {
        ClearCheckText,
        StageNameText,
        CurrentStageGoldHoldText,
        TotalGoldHoldText
    }

    enum Buttons
    {
        RePlayButton,
        GameEndButton2
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetText((int)Texts.ClearCheckText).text = GameManager.Instance.gameClear ? "Clear" : "Defeat";
        GetText((int)Texts.StageNameText).text = GameManager.Instance.currentStage.stage.ToString() + "Stage";
        GetText((int)Texts.CurrentStageGoldHoldText).text = "Gold : " + GameManager.Instance.currentStageGold.ToString();
        GetText((int)Texts.TotalGoldHoldText).text = "Total Gold : " + (Managers.DataManager.goldData.gold + GameManager.Instance.currentStageGold).ToString();

        GetButton((int)Buttons.RePlayButton).gameObject.BindEvent(ReplayGame);
        GetButton((int)Buttons.GameEndButton2).gameObject.BindEvent(GoStageSelectPopupUI);

        Managers.DataManager.SaveGold(GameManager.Instance.currentStageGold);

        return true;
    }

    void ReplayGame()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_IngamePopUp>();
        GameManager.Instance.Initialize(GameManager.Instance.currentStage);
    }

    void GoStageSelectPopupUI()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_StageSelectPopUp>();
    }

}
