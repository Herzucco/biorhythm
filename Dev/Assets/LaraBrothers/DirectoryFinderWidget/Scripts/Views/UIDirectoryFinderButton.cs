// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;

[RequireComponent(typeof(UIButton))]
[RequireComponent(typeof(UIPlaySound))]
[RequireComponent(typeof(UIKeyNavigation))]

/// <summary>
/// Joins all the buttons actions that has the DirectoryFinder Window.
/// </summary>
public class UIDirectoryFinderButton : MonoBehaviour {
	#region Enumarations

	// Enum that determines the action that the button should do.
	public enum DirectoryFinderButtonType { BACK, OPEN, ACTION };

	#endregion

	#region Component Properties

	/// <summary>
	/// Sets the button behaviour will should do.
	/// </summary>
	public DirectoryFinderButtonType ButtonType = DirectoryFinderButtonType.BACK;

	/// <summary>
	/// The directories finder.
	/// </summary>
	[HideInInspector] public UIDirectoryFinder Finder;

	/// <summary>
	/// Developer's event function. 
	/// The developer has to specifies a function to execute.
	/// </summary>
	[HideInInspector] public string EventFunction;

	#endregion

	#region Class Properties

	/// <summary>
	/// Stores the path, with respect to its action kind 
	/// that will should do, for example:
	/// 
	/// <c>BACK:</c> Goes to a folder level previous.
	/// <c>OPEN:</c> Opens the folder chosen.
	/// <c>CHOOSE:</c> It looking the files that there 
	/// are in that folder, or, load a file, or well, save the path.
	/// </summary>
	private string _path;

	/// <summary>
	/// The ItemF name.
	/// </summary>
	private string _name;

	/// <summary>
	/// To Identify the kind of item.
	/// </summary>
	private bool _kindOfItem;

	private GameObject _directoryFinder;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the path
	/// </summary>
	/// <value>The path of kind <c>string</c>.</value>
	public string PathToSearch {
		get { return _path; }
		set { _path = value; }
	}

	/// <summary>
	/// Gets or sets the name of the item.
	/// </summary>
	/// <value>The name of the item of kind <c>string</c>.</value>
	public string ItemName {
		get { return _name; }
		set { _name = value; }
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="UIuDirFButton"/> is kind of item.
	/// </summary>
	/// <value><c>true</c> if kind of item is folder; otherwise, 
	/// <c>false</c> if kind of the item is file.</value>
	public bool KindOfItem {
		get { return _kindOfItem; }
		set { _kindOfItem = value; }
	}

	public GameObject FinderController {
		get { return _directoryFinder; }
		set { _directoryFinder = value; }
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// When the OnClick event of the UIButton script is caused, 
	/// it executes the corresponding action at button kind that is.
	/// </summary>
	public void ClickItemF() {
		switch (ButtonType) {
		case DirectoryFinderButtonType.BACK: BackClick(); break; // Executes the back button task.
		case DirectoryFinderButtonType.OPEN: OpenClick(); break; // Executes the open button task.
		case DirectoryFinderButtonType.ACTION: ActionClick(); break; // Executes the choose button task.
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Asks for to the UIDirectoryFinder component that looking back 
	/// the folders that are in the previous level.
	/// </summary>
	private void BackClick() {
		try {
			Finder.PathToSearch = Directory.GetParent(PathToSearch.Trim()).FullName.Trim();
			Finder.Search();
		} catch (NullReferenceException) {
			Debug.LogWarning("UIDirectoryFinderButton: PathToSearch is a null reference.");
		} catch (ArgumentNullException) {
			Debug.LogWarning("UIDirectoryFinderButton: PathToSearch is a null argument.");
		} catch (ArgumentException) {
			Debug.LogWarning("UIDirectoryFinderButton: PathToSearch is an empty string or contains spaces.");
		}
	}

	/// <summary>
	/// Asks for to the UIDirectoruFinder component that looking 
	/// the folder that are in the new level.
	/// </summary>
	private void OpenClick() {
		if (KindOfItem) {
			Finder.PathToSearch = PathToSearch.Trim();
			Finder.Search();
		} else {
			Debug.LogWarning("UIDirectoryFinderButton: The \"" + PathToSearch + "\" path is a file, it\'s can\'t open.");
		}
	}

	/// <summary>
	/// Requests the search of the files that want be 
	/// loaded or stored.
	/// </summary>
	private void ActionClick() {
		if (EventFunction == "" || 
		    EventFunction == null) {
			Debug.LogWarning("UIDirectoryFinderButton: The EventFunction propety is not established.");
			return;
		}

		// It executes the developer's code. 
		if (FinderController != null) {
			FinderController.GetComponent<DirectoryFinderController>().ItemPath = PathToSearch;
			FinderController.GetComponent<DirectoryFinderController>().ItemName = ItemName;
			FinderController.SendMessage(EventFunction);
		}
		else {
			gameObject.SendMessage(EventFunction);
		}
	}

	#endregion
}
