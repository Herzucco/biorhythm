// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(UIPopupList))]
[RequireComponent(typeof(UIButton))]
[RequireComponent(typeof(UIKeyNavigation))]

/// <summary>
/// The behaviour of the UI that looking the logical volumes or drives.
/// </summary>
public class UIDrive : MonoBehaviour {
	#region Component Properies

	/// <summary>
	/// The directories finder.
	/// </summary>
	[HideInInspector] public UIDirectoryFinder Finder;

	/// <summary>
	/// The UILabel component that shows the folder or file chosen by player.
	/// </summary>
	[HideInInspector] public UILabel FolderSelected;

	/// <summary>
	/// The Back Button, to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject BackButton;
	
	/// <summary>
	/// The Open Button, to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject OpenButton;
	
	/// <summary>
	/// The Choose Button, to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject ActionButton;

	#endregion

	#region Class Properties

	/// <summary>
	/// Saves the path of the drives, that serves to 
	/// identify which drive was chosen by player.
	/// </summary>
	private List<string> _drivesPath = new List<string>();

	/// <summary>
	/// Saves the name of the drives, that it will sends 
	/// at items property of UIPopupList component.
	/// </summary>
	private List<string> _drivesName = new List<string>();

	#endregion

	#region Setters & Getters
	
	/// <summary>
	/// Gets the paths list of the drives that are found.
	/// </summary>
	/// <value>List of the drives of kind <c>string</c>.</value>
	public string[] DrivesPath {
		get { return _drivesPath.ToArray(); }
	}
	
	/// <summary>
	/// Gets the list of the names of the drives that are found.
	/// </summary>
	/// <value>List of the names of the drives of kind <c>string</c>.</value>
	public string[] DrivesName {
		get { return _drivesName.ToArray(); }
	}
	
	#endregion

	#region Unity Methods

	private void Awake() {
		// Compatibility with NGUI v3.4.9 until NGUI v3.5.3
		if (GetComponent<UIButtonKeys>())
			GetComponent<UIButtonKeys>().startsSelected = true;
		else // Compatibility with NGUI 3.5.4 of higher
			GetComponent<UIKeyNavigation>().startsSelected = true;
	}

	/// <summary>
	/// When starting out, looking drives.
	/// </summary>
	private void Start() {
		Search();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// When the OnChangeValue event of the UIPopupList script is caused, 
	/// it will be sent to the UIDirectoryFinder script (i.e. the Finder variable), 
	/// the path with the should be begin to looking for.
	/// </summary>
	public void ItemSelected() {
		// It gets the option index chosen.
		int index = _drivesName.IndexOf(gameObject.GetComponent<UIPopupList>().value);
		// To avoid errors, mainly when starting out the UIPopupList component.
		if (index > -1) {
			// In the following buttons are sent the path chosen.
			OpenButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = DrivesPath[index];
			ActionButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = DrivesPath[index];
			// Too are sent the names.
			OpenButton.GetComponent<UIDirectoryFinderButton>().ItemName = DrivesName[index];
			ActionButton.GetComponent<UIDirectoryFinderButton>().ItemName = DrivesName[index];
			// It shows the drive that was chosen.
			FolderSelected.text = gameObject.GetComponent<UIPopupList>().value;
			// It assigns the path and their directories are searched.
			Finder.PathToSearch = DrivesPath[index].Trim();
			Finder.Search();
		}
	}

	/// <summary>
	/// It examines the drives that there are in the machine.
	/// </summary>
	public void Search() {
		if (Finder == null)
			Debug.LogWarning("UIDrive: The property Finder is not established.");
		else
			StartCoroutine("SearchDrives");
	}

	#endregion

	#region Coroutines

	/// <summary>
	/// This coroutine looking all drives.
	/// </summary>
	/// <returns>Drive.</returns>
	private IEnumerator SearchDrives() {
		yield return 0;

		// The buttons are disabled, to avoid errors that the player could do.
		BackButton.GetComponent<UIButton>().isEnabled = false;
		OpenButton.GetComponent<UIButton>().isEnabled = false;
		ActionButton.GetComponent<UIButton>().isEnabled = false;

		// It deletes all trail of the previous search.
		_drivesPath.Clear();
		_drivesName.Clear();

		// The drives are searched.
		LogicalVolume drive = new LogicalVolume();
		drive.Find();

		// The drives found are extracted.
		foreach (string path in drive.DrivesPath)
			_drivesPath.Add(path);
		foreach (string name in drive.DrivesName)
			_drivesName.Add(name);

		// to the UIPopupList component the drives as selection option are assigned.
		gameObject.GetComponent<UIPopupList>().items = _drivesName;
		gameObject.GetComponent<UIPopupList>().value = DrivesName[0];

		// It saves the path in the buttons.
		OpenButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = DrivesPath[0];
		ActionButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = DrivesPath[0];

		// It shows the drive name chosen.
		FolderSelected.text = gameObject.GetComponent<UIPopupList>().value;

		// The buttons are enabled.
		BackButton.GetComponent<UIButton>().isEnabled = true;
		OpenButton.GetComponent<UIButton>().isEnabled = true;
		ActionButton.GetComponent<UIButton>().isEnabled = true;
	}

	#endregion
}
