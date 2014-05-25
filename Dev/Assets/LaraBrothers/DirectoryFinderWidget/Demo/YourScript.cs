// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Your script gets the name and the path of the item selected by player,
/// through the DirectoryFinderController component.
/// </summary>
public class YourScript : MonoBehaviour {

	/// <summary>
	/// Your event function.
	/// </summary>
	public void MyEventFunction() {
		// To get the name and the path of the item selected by player, only you have to make this:
		string name = gameObject.GetComponent<DirectoryFinderController>().ItemName;
		string thePath = gameObject.GetComponent<DirectoryFinderController>().ItemPath;

		// I show in console the item information.
		Debug.Log("You select the \"" + name + "\" item, is in the \"" + thePath + "\" path.");
	}
}
