// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;

[AddComponentMenu("Directory Finder/Controller")]

/// <summary>
/// The UI controller that show the folders or files of the path specified.
/// </summary>
public class DirectoryFinderController : MonoBehaviour {
	#region Component Properties

	/// <summary>
	/// The Back Button, to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject BackButton;
	
	/// <summary>
	/// The Open Button, to give it to ItemF0 object, and too, 
	/// to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject OpenButton;
	
	/// <summary>
	/// The Choose Button, to give it to ItemF0 object, and too, 
	/// to enable/disable when it do the search.
	/// </summary>
	[HideInInspector] public GameObject ActionButton;

	/// <summary>
	/// The UILabel component that show the folder or file chosen by player.
	/// </summary>
	[HideInInspector] public UILabel ItemSelected;
	
	/// <summary>
	/// The UILabel component, to show all current path.
	/// </summary>
	[HideInInspector] public UILabel PathLabel;
	
	/// <summary>
	/// If it wants looking for the files.
	/// </summary>
	public bool FindFiles;

	/// <summary>
	/// Shows the path on the interface.
	/// </summary>
	public bool ShowPath;

	/// <summary>
	/// The event function, to execute the function of your script.
	/// </summary>
	public string EventFunction;
	
	#endregion

	#region Class Properties

	/// <summary>
	/// The user interface grid, who has the UIDirectoryFinder component.
	/// </summary>
	private GameObject _grid;

	/// <summary>
	/// The drive, for the UIDirectoryFinder component.
	/// </summary>
	private GameObject _drive;

	/// <summary>
	/// The UIScrollBar component, for the UIDirectoryFinder component.
	/// </summary>
	private UIScrollBar _scrollbar;

	/// <summary>
	/// The scroll view to drag the ItemF0 object.
	/// </summary>
	private UIScrollView _scrollview;

	/// <summary>
	/// The contanier to resize.
	/// </summary>
	private GameObject _container;

	/// <summary>
	/// Stores the folder or file path recovered.
	/// </summary>
	private string _itemPath;

	/// <summary>
	/// Stores the folder or path name recovered.
	/// </summary>
	private string _itemName;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the item path.
	/// </summary>
	/// <value>The item path.</value>
	public string ItemPath {
		get { return _itemPath; }
		set { _itemPath = value; }
	}

	/// <summary>
	/// Gets or sets the name of the item.
	/// </summary>
	/// <value>The name of the item.</value>
	public string ItemName {
		get { return _itemName; }
		set { _itemName = value; }
	}

	#endregion

	#region Unity Methods

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake() {
		// It assigns the internal objects necessary.
		_grid = (transform.Find("Window[Panel]/Scroll View[Panel]/Grid") == null) ? 
			null : 
			transform.Find("Window[Panel]/Scroll View[Panel]/Grid").gameObject;
		_drive = (transform.Find("Window[Panel]/Drives[Panel]/Drives UI/Drives[Popup List]") == null) ? 
			null : 
			transform.Find("Window[Panel]/Drives[Panel]/Drives UI/Drives[Popup List]").gameObject;
		_scrollbar = (transform.Find("Window[Panel]/Scroll Bar[Panel]") == null) ? 
			null : 
			transform.Find("Window[Panel]/Scroll Bar[Panel]").GetComponent<UIScrollBar>();
		_scrollview = (transform.Find("Window[Panel]/Scroll View[Panel]") == null) ? 
			null : 
			transform.Find("Window[Panel]/Scroll View[Panel]").GetComponent<UIScrollView>();

		// It assigns the external objects necessary.
		BackButton = 
			(transform.Find("Window[Panel]/BackButton") == null) ? 
			null : 
			transform.Find("Window[Panel]/BackButton").gameObject;
		OpenButton = 
			(transform.Find("Window[Panel]/OpenButton") == null) ? 
			null : 
			transform.Find("Window[Panel]/OpenButton").gameObject;
		ActionButton = 
			(transform.Find("Window[Panel]/ActionButton") == null) ? 
			null : 
			transform.Find("Window[Panel]/ActionButton").gameObject;

		ItemSelected = 
			(transform.Find("Window[Panel]/ItemSelected[Label]") == null) ? 
			null : 
			transform.Find("Window[Panel]/ItemSelected[Label]").GetComponent<UILabel>();
		PathLabel = 
			(transform.Find("Window[Panel]/Footer/Path[Label]") == null) ? 
				(transform.Find("Window[Panel]/Path[Label]") == null) ? 
				null : 
				transform.Find("Window[Panel]/Path[Label]").GetComponent<UILabel>() :
			transform.Find("Window[Panel]/Footer/Path[Label]").GetComponent<UILabel>();
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	public void Start() {
		bool isNotEstablished = false;

		// It checks the internal objects necessary.
		if (_grid == null) {
			isNotEstablished = true;
			WarningMessage("The Grid was not found in the \"Window[Panel]/Scroll View[Panel]/Grid\" hierarchy.");
		}

		if (_drive == null) {
			isNotEstablished = true;
			WarningMessage("The Drives[Popup List] was not found in the \"Window[Panel]/Drives[Panel]/Drives UI/Drives[Popup List]\" hierarchy.");
		}

		if (_scrollbar == null) {
			isNotEstablished = true;
			WarningMessage("The Scroll Bar[Panel] was not found.");
		}

		if (_scrollview == null) {
			isNotEstablished = true;
			WarningMessage("The Scroll View was not found.");
		}

		// It checks the external objects necessary.
		if (BackButton == null) {
			isNotEstablished = true;
			WarningMessage("The Back Button property is not established.");
		}

		if (OpenButton == null) {
			isNotEstablished = true;
			WarningMessage("The Open Button property is not established.");
		}

		if (ActionButton == null) {
			isNotEstablished = true;
			WarningMessage("The Action Button property is not established.");
		}

		if (ItemSelected == null) {
			isNotEstablished = true;
			WarningMessage("The Item Selected property is not established.");
		}

		if (ShowPath) {
			if (PathLabel == null) {
				isNotEstablished = true;
				WarningMessage("The Path Label property is not established.");
			}
		}

		if (EventFunction == null) {
			isNotEstablished = true;
			WarningMessage("The Event Function property is not established.");
		}

		if (!isNotEstablished) Reset();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Sets the elements necessary for serve.
	/// </summary>
	public void Reset() {
		_grid.GetComponent<UIDirectoryFinder>().Drive = _drive;
		_grid.GetComponent<UIDirectoryFinder>().ScrollBar = _scrollbar;
		_grid.GetComponent<UIDirectoryFinder>().ScrollView = _scrollview;

		_grid.GetComponent<UIDirectoryFinder>().BackButton = BackButton;
		_grid.GetComponent<UIDirectoryFinder>().OpenButton = OpenButton;
		_grid.GetComponent<UIDirectoryFinder>().ActionButton = ActionButton;

		_grid.GetComponent<UIDirectoryFinder>().ItemSelected = ItemSelected;
		_grid.GetComponent<UIDirectoryFinder>().PathLabel = PathLabel;
		_grid.GetComponent<UIDirectoryFinder>().ShowPath = ShowPath;

		_grid.GetComponent<UIDirectoryFinder>().FindFiles = FindFiles;

		ActionButton.GetComponent<UIDirectoryFinderButton>().Finder = _grid.GetComponent<UIDirectoryFinder>();
		ActionButton.GetComponent<UIDirectoryFinderButton>().FinderController = gameObject;
		ActionButton.GetComponent<UIDirectoryFinderButton>().EventFunction = EventFunction;

		OpenButton.GetComponent<UIDirectoryFinderButton>().Finder = _grid.GetComponent<UIDirectoryFinder>();
		BackButton.GetComponent<UIDirectoryFinderButton>().Finder = _grid.GetComponent<UIDirectoryFinder>();
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Warning messages.
	/// </summary>
	/// <param name="msg">The message.</param>
	private void WarningMessage(string msg) {
		Debug.LogWarning("DirectoryFinderController: " + msg);
	}

	#endregion
}
