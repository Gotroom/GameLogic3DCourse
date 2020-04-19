using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenu : BaseMenu
{
	private void LoadVideoOptions()
	{
		Interface.Execute(InterfaceObject.VideoOptions);
		
	}
	private void LoadSoundOptions()
	{
		Interface.Execute(InterfaceObject.AudioOptions);
	}
	private void LoadGameOptions()
	{
		Interface.Execute(InterfaceObject.GameOptions);
	}
	private void Back()
	{
		Interface.Execute(InterfaceObject.MainMenu);
	}
	public override void Hide()
	{
		if (!IsShow) return;
		IsShow = false;
	}
	public override void Show()
	{
		if (IsShow) return;
		IsShow = true;
	}
}
