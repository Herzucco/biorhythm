// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIGrid))]
[RequireComponent(typeof(UICenterOnChild))]

/// <summary>
/// The UI behaviour that show the folders or files of the path specified.
/// </summary>
public class UIDirectoryFinder : MonoBehaviour {
	#region Component Properties

	/// <summary>
	/// <c>To the ItemF0 object</c>
	/// 
	/// The scroll view to drag the ItemF0 object.
	/// </summary>
	[HideInInspector] public UIScrollView ScrollView;

	/// <summary>
	/// <c>To the ItemF0 object</c>
	/// 
	/// The UILabel component that show the folder or file chosen by player.
	/// </summary>
	[HideInInspector] public UILabel ItemSelected;

	/// <summary>
	/// The Back Button, to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject BackButton;

	/// <summary>
	/// <c>To the ItemF0 object</c>
	/// 
	/// The Open Button, to give it to ItemF0 object, and too, 
	/// to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject OpenButton;

	/// <summary>
	/// <c>To the ItemF0 object</c>
	/// 
	/// The Choose Button, to give it to ItemF0 object, and too, 
	/// to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject ActionButton;

	/// <summary>
	/// The UILabel component, to show all current path.
	/// </summary>
	[HideInInspector] public UILabel PathLabel;

	/// <summary>
	/// Shows the path on the interface.
	/// </summary>
	[HideInInspector] public bool ShowPath;

	/// <summary>
	/// The UIScrollBar component, to setting to the zero value.
	/// </summary>
	[HideInInspector] public UIScrollBar ScrollBar;

	/// <summary>
	/// The drive.
	/// </summary>
	[HideInInspector] public GameObject Drive;

	/// <summary>
	/// The ItemF0 prefab that represents to the folders or files that are found.
	/// </summary>
	[HideInInspector] public GameObject ItemF;

	/// <summary>
	/// If it wants looking for the files.
	/// </summary>
	[HideInInspector] public bool FindFiles;

	#endregion

	#region Class Properties

	/// <summary>
	/// Stores the path on the which have to looking more folders or files.
	/// </summary>
	private string _path;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the path to search.
	/// </summary>
	/// <value>Path.</value>
	public string PathToSearch {
		get { return _path; }
		set { _path = value; }
	}

	#endregion

	#region Unity Methods

	public void Awake() {
		ItemF = transform.parent.FindChild("ItemF0").gameObject;
		ItemF.SetActive(false);
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// It examines all the folders and files, and are shown in the Grid game object, 
	/// or anyone who has to this component.
	/// </summary>
	public void Search() {
		// By any problem that might have, 
		// it relocate on the platform drive root.
		if (PathToSearch.Trim().Length <= 0)
			Relocate();
		else
			StartCoroutine("SearchDirectory"); // or else, it looking in the folder chosen.
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Relocate on the platform root path.
	/// </summary>
	private void Relocate() {
		// It checks if the game is executed on Windows.
		if (Application.platform == RuntimePlatform.WindowsPlayer || 
		    Application.platform == RuntimePlatform.WindowsEditor) {
			// It establish the main drive root.
			PathToSearch = @"C:\";
			Search();
		// It checks if the game is executed on Mac OS X.
		} else if (Application.platform == RuntimePlatform.OSXPlayer || 
		           Application.platform == RuntimePlatform.OSXEditor) {
			// It establish the main drive root.
			PathToSearch = "/";
			Search();
		// It checks if the game is executed on Linux.
		} else if (Application.platform == RuntimePlatform.LinuxPlayer) {
			// It establish the main drive root.
			PathToSearch = "/";
			Search();
		}

		ItemSelected.text = PathToSearch.Trim();
		OpenButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = PathToSearch.Trim();
		ActionButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = PathToSearch.Trim();
	}

	#endregion

	#region Coroutines

	/// <summary>
	/// This coroutine looking the subdirectories of the path specified, 
	/// and create the ItemF0 prefabs.
	/// </summary>
	/// <returns>Directory.</returns>
	private IEnumerator SearchDirectory() {
		yield return 0;

		// All buttons are disabled, to avoid errors that the player could do.
		BackButton.GetComponent<UIButton>().isEnabled = false;
		OpenButton.GetComponent<UIButton>().isEnabled = false;
		ActionButton.GetComponent<UIButton>().isEnabled = false;
		
		// All the ItemF0 prefabs are deleted, if there are.
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		
		// If there are no children, then are created.
		if (transform.childCount == 0) {
			// It repositions the UIGrid content to that the ItemF0 elements 
			// are in the order with their respective space.
			GetComponent<UIGrid>().repositionNow = true;
			GetComponent<UIGrid>().Reposition(); // Was added from the 3.5.0 version of NGUI.
			ScrollView.UpdateScrollbars(true); // Was added from the 3.5.0 version of NGUI.

			// It looking the subdirectories and files.
			DirectoryFinder finder = new DirectoryFinder(PathToSearch.Trim());
			finder.FindFiles = FindFiles;
			finder.Find();

			// If it found, albeit a folder, are created.
			if (finder.FoldersPathFound.Length > 0) {
				// The array is traversed and create the UI.
				for (int i = 0; i < finder.FoldersPathFound.Length; i++) {
					GameObject newItemFolder = (GameObject) Instantiate(ItemF, new Vector3(0, 0, 0), Quaternion.identity);
					// To drag the elements.
					newItemFolder.GetComponent<UIDragScrollView>().scrollView = ScrollView;
					// Sets the new Item Folder settings.
					newItemFolder.GetComponent<UIItemF>().ItemSelected = ItemSelected;
					newItemFolder.GetComponent<UIItemF>().OpenButton = OpenButton.GetComponent<UIDirectoryFinderButton>();
					newItemFolder.GetComponent<UIItemF>().ActionButton = ActionButton.GetComponent<UIDirectoryFinderButton>();
					newItemFolder.GetComponent<UIItemF>().ItemPath = finder.FoldersPathFound[i].Trim();
					newItemFolder.GetComponent<UIItemF>().ItemName = finder.FoldersNameFound[i].Trim();
					newItemFolder.GetComponent<UIItemF>().KindOf = true;
					newItemFolder.name = "ItemF" + i;
					newItemFolder.transform.parent = transform;
					newItemFolder.transform.localScale = new Vector3(1, 1, 1);
					newItemFolder.SetActive(true);
				}
			}

			// If it found, albeit a files, are created.
			if (finder.FilesPathFound.Length > 0) {
				// The array is traversed and create the UI.
				for (int i = 0; i < finder.FilesPathFound.Length; i++) {
					GameObject newItemFile = (GameObject) Instantiate(ItemF, new Vector3(0, 0, 0), Quaternion.identity);
					// To frag the elements.
					newItemFile.GetComponent<UIDragScrollView>().scrollView = ScrollView;
					// Sets the new Item File settings.
					newItemFile.GetComponent<UIItemF>().ItemSelected = ItemSelected;
					newItemFile.GetComponent<UIItemF>().OpenButton = OpenButton.GetComponent<UIDirectoryFinderButton>();
					newItemFile.GetComponent<UIItemF>().ActionButton = ActionButton.GetComponent<UIDirectoryFinderButton>();
					newItemFile.GetComponent<UIItemF>().ItemPath = finder.FilesPathFound[i].Trim();
					newItemFile.GetComponent<UIItemF>().ItemName = finder.FilesNameFound[i].Trim();
					newItemFile.GetComponent<UIItemF>().KindOf = false;
					newItemFile.name = "ItemF" + (i + finder.FoldersPathFound.Length);
					newItemFile.transform.parent = transform;
					newItemFile.transform.localScale = new Vector3(1, 1, 1);
					newItemFile.SetActive(true);
				}
			}

			// Was added from the 3.5.0 version of NGUI.
			// Locates the new list in the zero position.
			if (ScrollView.gameObject.GetComponent<SpringPanel>()) {
				ScrollView.gameObject.GetComponent<SpringPanel>().StopAllCoroutines();
				ScrollView.gameObject.GetComponent<SpringPanel>().target = Vector3.zero;
				ScrollView.gameObject.GetComponent<SpringPanel>().enabled = true;
			} else {
				// Was added from the 3.5.9 version of NGUI.
				ScrollView.gameObject.AddComponent<SpringPanel>();
				ScrollView.gameObject.GetComponent<SpringPanel>().target = Vector3.zero;
			}

			// It repositions the UIGrid content to that the ItemF0 elements 
			// are in the order with their respective space.
			GetComponent<UIGrid>().repositionNow = true;
			GetComponent<UIGrid>().Reposition(); // Was added from the 3.5.0 version of NGUI.
			ScrollView.UpdateScrollbars(true); // Was added from the 3.5.0 version of NGUI.
			
			// It verifies if is possible assign a path of keyboard or game control to the items.
			if (transform.childCount > 0) {
				// It gets the UIButtonKeys componet of the children.

				// Compatibility with NGUI 3.5.4 of higher
				//UIKeyNavigation[] items = gameObject.GetComponentsInChildren<UIKeyNavigation>();
				
				Drive.GetComponent<UIKeyNavigation>().onDown = transform.GetChild(0).gameObject;
				
				BackButton.GetComponent<UIKeyNavigation>().onUp = transform.GetChild(transform.childCount - 1).gameObject;
				OpenButton.GetComponent<UIKeyNavigation>().onUp = transform.GetChild(transform.childCount - 1).gameObject;
				ActionButton.GetComponent<UIKeyNavigation>().onUp = transform.GetChild(transform.childCount - 1).gameObject;
				
				BackButton.GetComponent<UIKeyNavigation>().onDown = Drive;
				OpenButton.GetComponent<UIKeyNavigation>().onDown = Drive;
				ActionButton.GetComponent<UIKeyNavigation>().onDown = Drive;
				
				for (int i = 0; i < transform.childCount; i++) {
					if (i == 0) {
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onUp = Drive;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onDown = (transform.childCount == 1) ? BackButton : transform.GetChild(i + 1).gameObject;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onClick = OpenButton;
					} else if (i < transform.childCount - 1) {
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onUp = transform.GetChild(i - 1).gameObject;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onDown = transform.GetChild(i + 1).gameObject;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onClick = OpenButton;
					} else {
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onUp = transform.GetChild(i - 1).gameObject;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onDown = BackButton;
						transform.GetChild(i).GetComponent<UIKeyNavigation>().onClick = OpenButton;
					}
				}
			} else { // or else, then, it browses between buttons.
				// Compatibility with NGUI 3.5.4 of higher
				Drive.GetComponent<UIKeyNavigation>().onDown = BackButton;
				
				BackButton.GetComponent<UIKeyNavigation>().onUp = Drive;
				OpenButton.GetComponent<UIKeyNavigation>().onUp = Drive;
				ActionButton.GetComponent<UIKeyNavigation>().onUp = Drive;
				
				BackButton.GetComponent<UIKeyNavigation>().onDown = Drive;
				OpenButton.GetComponent<UIKeyNavigation>().onDown = Drive;
				ActionButton.GetComponent<UIKeyNavigation>().onDown = Drive;
			}

			// Compatibility with NGUI 3.5.4 of higher
			Drive.GetComponent<UIKeyNavigation>().onUp = BackButton;
			
			BackButton.GetComponent<UIKeyNavigation>().onLeft = ActionButton;
			BackButton.GetComponent<UIKeyNavigation>().onRight = OpenButton;
			
			OpenButton.GetComponent<UIKeyNavigation>().onLeft = BackButton;
			OpenButton.GetComponent<UIKeyNavigation>().onRight = ActionButton;
			
			ActionButton.GetComponent<UIKeyNavigation>().onLeft = OpenButton;
			ActionButton.GetComponent<UIKeyNavigation>().onRight = BackButton;

			// It show the current path.
			PathLabel.gameObject.SetActive(ShowPath);
			if (ShowPath) PathLabel.text = PathToSearch.Trim();
			// It asks for to the Back Button that save this path.
			BackButton.GetComponent<UIDirectoryFinderButton>().PathToSearch = PathToSearch.Trim();

			// The buttons are enabled.
			BackButton.GetComponent<UIButton>().isEnabled = true;
			OpenButton.GetComponent<UIButton>().isEnabled = true;
			ActionButton.GetComponent<UIButton>().isEnabled = true;
		} else {
			// It stops the coroutine, and it runs again, 
			// this is for the delay of the GameObjects destruction.
			StopCoroutine("SearchDirectory");
			StartCoroutine("SearchDirectory");
		}
	}

	#endregion
}
