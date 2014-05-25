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
/// Gets all the folders or files that exist in the specified
/// directory. This class allows at player find and
/// choose a folder or file.
/// </summary>
public class DirectoryFinder {
	#region Class Properties

	/// <summary>
	/// The current directory where to look.
	/// </summary>
	private string _directory;

	/// <summary>
	/// Stores the paths of the folders found in the specified directory.
	/// </summary>
	private ArrayList _foldersPath;

	/// <summary>
	/// Stores the names of the folders found.
	/// </summary>
	private string[] _foldersName;

	/// <summary>
	/// Stores the paths of the files found in the specified directory.
	/// </summary>
	private ArrayList _filesPath;

	/// <summary>
	/// Stores the paths of the files found.
	/// </summary>
	private string[] _filesName;

	/// <summary>
	/// Specifies if the files too should be looked.
	/// </summary>
	private bool _findFiles;

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the current directory where to look.
	/// </summary>
	/// <value>Current Directory of kind <c>string</c>.</value>
	public string CurrentDirectory {
		get { return _directory; }
		set { _directory = value; }
	}

	/// <summary>
	/// Gets the paths of the folders found in the specified directory.
	/// </summary>
	/// <value>Array of the paths of the folders of kind <c>string</c>.</value>
	public string[] FoldersPathFound {
		get {
			string[] folders = new string[_foldersPath.Count];
			_foldersPath.CopyTo(folders);

			return folders;
		}
	}

	/// <summary>
	/// Gets the folders name found.
	/// </summary>
	/// <value>Array of the names of the folders or files of kind <c>string</c>.</value>
	public string[] FoldersNameFound {
		get { return _foldersName; }
	}

	/// <summary>
	/// Gets the paths of the files found in the specified directory.
	/// </summary>
	/// <value>Array of the paths of the files of kind <c>string</c>.</value>
	public string[] FilesPathFound {
		get {
			string[] files = new string[_filesPath.Count];
			_filesPath.CopyTo(files);

			return files;
		}
	}

	/// <summary>
	/// Gets the files name found.
	/// </summary>
	/// <value>The files name found.</value>
	public string[] FilesNameFound {
		get { return _filesName; }
	}

	/// <summary>
	/// Gets or sets a value indicating if it wants looking for the files.
	/// </summary>
	/// <value><c>true</c> if file search; otherwise, <c>false</c>.</value>
	public bool FindFiles {
		get { return _findFiles; }
		set { _findFiles = value; }
	}

	#endregion

	#region Constructs

	/// <summary>
	/// Initializes a new instance of the <see cref="DirectoryFinder"/> class, 
	/// without parameters.
	/// </summary>
	public DirectoryFinder() {
		_foldersPath = new ArrayList();
		_filesPath = new ArrayList();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DirectoryFinder"/> class.
	/// </summary>
	/// <param name="directory">Directory where it want to look.</param>
	public DirectoryFinder(string directory) {
		_foldersPath = new ArrayList();
		_filesPath = new ArrayList();
		CurrentDirectory = directory;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// It browses all the folders that exist in the specified directory.
	/// </summary>
	public void Find() {
		if (CurrentDirectory != "" || CurrentDirectory != null)
			ProcessDirectory(CurrentDirectory);
		else
			Debug.LogWarning("DirectoryFinder: The directory is not established");
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// It process the directory content, the paths and 
	/// the names of the folders or files that were extracted.
	/// </summary>
	/// <param name="target">Directory where it want to look.</param>
	private void ProcessDirectory(string target) {
		try {
			// It deletes all trail of the previous search.
			_foldersPath.Clear();
			_foldersName = null;
			_filesPath.Clear();
			_filesName = null;

			// #### Folders ####

			// It gets the paths of the folders.
			string[] subdirectoryEntries = Directory.GetDirectories(target);
			foreach (string subdirectory in subdirectoryEntries)
				_foldersPath.Add(subdirectory);
			
			// It gets the names of the folders.
			// First it has to copy the paths to a simple array of kind string.
			_foldersName = new string[_foldersPath.Count];
			_foldersPath.CopyTo(_foldersName);
			
			// Now, the new array is traversed.
			for (int i = 0; i < _foldersName.Length; i++) {
				// The start and end spaces are deleted, and the index 
				// of the last occurrence of the separator is saved.
				int endDirSeparator = _foldersName[i].Trim().LastIndexOf(Path.DirectorySeparatorChar.ToString());
				// It gets the string, and the start and end spaces are removed, 
				// and it finish with save the new string.
				_foldersName[i] = _foldersName[i].Substring(endDirSeparator + 1).Trim();
			}

			// #### Files ####

			if (FindFiles) {
				// It gets the paths of the files.
				string[] fileEntries = Directory.GetFiles(target);
				foreach (string file in fileEntries)
					_filesPath.Add(file);
				
				// It gets the names of the files.
				// First it has to copy the paths to a simple array of kind string.
				_filesName = new string[_filesPath.Count];
				_filesPath.CopyTo(_filesName);
				
				// Now, the new array is traversed.
				for (int i = 0; i < _filesName.Length; i++) {
					// The start and end spaces are deleted, and the index
					// of the last occurrence of the separator is saved.
					int endDirSeparator = _filesName[i].Trim().LastIndexOf(Path.DirectorySeparatorChar.ToString());
					// It gets the string, and the start and end sapces are removed,
					// and it finish with save the new string.
					_filesName[i] = _filesName[i].Substring(endDirSeparator + 1).Trim();
				}
			}
		} catch (UnauthorizedAccessException) {
			// It launch where a folder or files is denied.
			Debug.LogWarning("DirectoryFinder: Access to the path \"" + target + "\" is denied.");
		} catch (IOException) {
			// It launch where occurs an error of E/S.
			Debug.LogWarning("DirectoryFinder: E/S Error, this Path \"" + target + "\" is posible that is not a hard disk.");
		}
	}

	#endregion
}
