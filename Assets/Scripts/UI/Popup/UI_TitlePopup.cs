using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_TitlePopup : UI_Popup
{

	enum Buttons
	{
		StartButton
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		BindButton(typeof(Buttons)); 

		GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
		
		return true;
	}

	void OnClickStartButton()
	{
		Debug.Log("OnClickStartButton");
		
		Managers.UI.ClosePopupUI(this);
		
	}
}
