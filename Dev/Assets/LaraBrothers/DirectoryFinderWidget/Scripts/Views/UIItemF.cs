// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(UIWidget))]
[RequireComponent(typeof(UIToggle))]
[RequireComponent(typeof(UIButtonColor))]
[RequireComponent(typeof(UIDragScrollView))]
[RequireComponent(typeof(UICenterOnClick))]
[RequireComponent(typeof(UIKeyNavigation))]

/// <summary>
/// Saves the folder or file path and its name, 
/// and shows its name in a kind UILabel sprite of NGUI.
/// </summary>
public class UIItemF : MonoBehaviour {
	#region Component Properties

	/// <summary>
	/// The UIToggle checkmark that, here is a selecction element.
	/// </summary>
	[HideInInspector] public GameObject Checkmark;

	/// <summary>
	/// The folder icon to identify if it's a folder.
	/// </summary>
	[HideInInspector] public GameObject FolderIcon;
	
	/// <summary>
	/// The file icon to identify if it's a file.
	/// </summary>
	[HideInInspector] public GameObject FileIcon;

	/// <summary>
	/// The UILabel component that will display the name of the folder or file.
	/// </summary>
	[HideInInspector] public UILabel Label;

	/// <summary>
	/// The UILabel component of an external Game Object 
	/// that serves to display the folder name chosen by player.
	/// </summary>
	[HideInInspector] public UILabel ItemSelected;

	/// <summary>
	/// Button that allows open a folder.
	/// </summary>
	[HideInInspector] public UIDirectoryFinderButton OpenButton;

	/// <summary>
	/// Button that allows choose the file or folder for make an any operation.
	/// </summary>
	[HideInInspector] public UIDirectoryFinderButton ActionButton;

	#endregion

	#region Class Properties

	/// <summary>
	/// Saves the absolute path of the folder or file.
	/// </summary>
	private string _itemPath;

	/// <summary>
	/// Saves the names of the folder or file.
	/// </summary>
	private string _itemName;

	/// <summary>
	/// If it's a folder, then shown the folder icon, or else,
	/// then shown the file icon.
	/// </summary>
	private bool _isFolder;

	/// <summary>
	/// Serves to get the UICenterOnChild component of its parent, and center the item.
	/// </summary>
	//private UICenterOnChild _center;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the folder or file path.
	/// </summary>
	/// <value>Folder or file path.</value>
	public string ItemPath {
		get { return _itemPath; }
		set { _itemPath = value; }
	}

	/// <summary>
	/// Gets or sets the folder or file name. At establish, 
	/// it shows the text in the UIlabel element.
	/// </summary>
	/// <value>Tha folder name of kind <c>string</c>.</value>
	public string ItemName {
		get { return _itemName; }
		set {
			_itemName = value;
			Label.text = ItemName;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="UIItemF"/> is a kind of 'folder' or 'file.
	/// </summary>
	/// <value><c>true</c> if kind of 'folder'; otherwise, <c>false</c> if kind of 'file'.</value>
	public bool KindOf {
		get { return _isFolder; }
		set {
			_isFolder = value;

			if (KindOf) {
				FolderIcon.SetActive(true);
				FileIcon.SetActive(false);
			} else {
				FolderIcon.SetActive(false);
				FileIcon.SetActive(true);
			}
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// When the OnValueChange event of the UIToggle script is caused, 
	/// will send the information to the following UI elements:
	/// 
	/// <c>FolderSelected:</c> Sends the folder or file name to the FolderSelected (Label) game object.
	/// <c>ButtonOpen:</c> Sends the folder or file path to the ButtonOpen game object.
	/// <c>ButtonChoose:</c> Sends the folder or file path to the ButtonChoose game object.
	/// </summary>
	public void ValueChange() {
		if (gameObject.GetComponent<UIToggle>().value) {
			ItemSelected.text = ItemName;
			OpenButton.PathToSearch = ItemPath;
			OpenButton.KindOfItem = KindOf;
			ActionButton.PathToSearch = ItemPath;
			ActionButton.ItemName = ItemName;
			ActionButton.KindOfItem = KindOf;
		}
	}

	#endregion

	#region Unity Methods

	/// <summary>
	/// Awake searching the internal elements necessary.
	/// </summary>
	public void Awake() {
		Checkmark = transform.Find("Checkmark").gameObject;
		FolderIcon = transform.Find("FolderIcon[Sprite]").gameObject;
		FileIcon = transform.Find("FileIcon[Sprite]").gameObject;
		Label = transform.Find("ItemName[Label]").gameObject.GetComponent<UILabel>();
	}

	/// <summary>
	/// This method correct the selection through keyboard 
	/// or game control.
	/// </summary>
	public void Update() {
		// Check of alpha prevents that UIScrollView constantly be located.
		// Caused by NGUI 3.5.4
		if (UICamera.selectedObject == gameObject && 
		    transform.FindChild("Hover").GetComponent<UIWidget>().color.a < 1) {
			// Was added from the 3.5.9 version of NGUI.
			UICenterOnChild center = NGUITools.FindInParents<UICenterOnChild>(gameObject);
			UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(gameObject);

			Transform panelTrans = scrollView.panel.cachedTransform;

			Vector3 objectPoint = panelTrans.InverseTransformPoint(gameObject.transform.position);
			Vector3 panelPoint = panelTrans.InverseTransformPoint(
				(scrollView.panel.worldCorners[2] + scrollView.panel.worldCorners[0]) * 0.5f);
			Vector3 offset = objectPoint - panelPoint;

			if (!scrollView.canMoveVertically) offset.y = 0f;
			offset.z = 0f;

			SpringPanel.Begin(
				scrollView.panel.cachedGameObject, 
				panelTrans.localPosition - offset, 
				8f);

			// backward compatibility with v3.5.5 -> v3.5.8 NGUI.
			center.CenterOn(transform);
		}
	}

	#endregion
}
