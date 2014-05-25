// ------------------------------------------------
//         Directory Finder Widget for NGUI
//                       by 
//                  Lara Brothers
//           (Roman Lara & Humberto Lara)
// ------------------------------------------------

using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// It browses all the logical volumes or drives 
/// that exist in the machine. 
/// </summary>
public class LogicalVolume {
	#region Class Properties

	/// <summary>
	/// Save the paths of the drives.
	/// </summary>
	private ArrayList _drivesPath;

	/// <summary>
	/// Save the names of the drives.
	/// </summary>
	private ArrayList _drivesName;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets the list of the paths of the drives that were found.
	/// </summary>
	/// <value>List of the drives that are found of kind <c>string</c>.</value>
	public string[] DrivesPath {
		get {
			string[] drives = new string[_drivesPath.Count];
			_drivesPath.CopyTo(drives);

			return drives;
		}
	}

	/// <summary>
	/// Gets the list of the names of the drives that were found.
	/// </summary>
	/// <value>List of the names of the drives of kind <c>string</c>.</value>
	public string[] DrivesName {
		get {
			string[] drives = new string[_drivesName.Count];
			_drivesName.CopyTo(drives);
			
			return drives;
		}
	}

	#endregion

	#region Constructs

	/// <summary>
	/// Initializes a new instance of the <see cref="LogicalVolume"/> class.
	/// </summary>
	public LogicalVolume() {
		_drivesPath = new ArrayList();
		_drivesName = new ArrayList();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Examines the logical volumes or drives.
	/// </summary>
	public void Find() {
		ProcessDrives();
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// It process the search of the drives. The paths and names are saved.
	/// </summary>
	private void ProcessDrives() {
		// It delete all trail of the previous search.
		_drivesPath.Clear();
		_drivesName.Clear();

		// It checks if the game is executed on Windows.
		if (Application.platform == RuntimePlatform.WindowsPlayer || 
		    Application.platform == RuntimePlatform.WindowsEditor) {
			// It gets all the Windows drives.
			string[] allDrives = Environment.GetLogicalDrives();

			// The array is traversed on search of the necessary.
			foreach (string drive in allDrives) {
				_drivesPath.Add(drive.Trim());
				_drivesName.Add(drive.Trim());
			}
		// It checks if the game is executed on Mac OS X.
		} else if (Application.platform == RuntimePlatform.OSXPlayer || 
		           Application.platform == RuntimePlatform.OSXEditor) {
			// Drives as subdirectories of "/Volumes" are wanted.
			DirectoryFinder finder = new DirectoryFinder("/Volumes".Trim());
			finder.Find();

			// It gets the directories found.
			foreach (string path in finder.FoldersPathFound)
				_drivesPath.Add(path);
			foreach (string name in finder.FoldersNameFound)
				_drivesName.Add(name);
		// It checks if the game is executed on Linux.
		} else if (Application.platform == RuntimePlatform.LinuxPlayer) {
			/*
			// It gets all the Linux drives.
			DriveInfo[] allDrives = DriveInfo.GetDrives();

			// The array is traversed on search of the necessary.
			foreach (DriveInfo drive in allDrives) {
				// It checks that can work with the drive.
				if (drive.IsReady) {
					// It checks that the drive is not a CDRom or 
					// other inadvisable drive kind.
					if (drive.DriveType == DriveType.Fixed || 
					    drive.DriveType == DriveType.Removable) {
						_drivesPath.Add(drive.Name.Trim()); // drive.RootDirectory.FullName.Trim());
						_drivesName.Add(drive.Name.Trim());
					}
				}
			}
			*/
			// Drives as subdirectories of "/media" are wanted.
			DirectoryFinder finder = new DirectoryFinder("/media".Trim());
			finder.Find();

			// It gets the directories found.
			foreach (string path in finder.FoldersPathFound)
				_drivesPath.Add(path);
			foreach (string name in finder.FoldersNameFound)
				_drivesName.Add(name);
		} else {
			// It launch when the platform is not supported.
			Debug.LogWarning("LogicalVolume: This platform is not supported.");
		}
	}

	#endregion
}
