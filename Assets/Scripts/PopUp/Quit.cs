using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Quit : MonoBehaviour
{
	void Start()
    {
		Action action = () => {
			Application.Quit();
		};

		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => {
			Popup popup = UIController.Instance.CreatePopup();
			popup.Init(UIController.Instance.MainCanvas,
				"Inspector Cat",
				"Are you sure you want to quit?",
				"Stay here",
				"Bye Bye",
				action
				);
		});
	}
}
