using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MenuButton : MonoBehaviour {

	public string levelName;
	public AudioSource source;
	public AudioManager manager;
	private List<AudioClip> clips;
	private string[] gamepadList;
	// Use this for initialization
	void Start () {
		if(manager.clips.Count <= 0){
			clips = new List<AudioClip>();
			manager.clips = clips;
			GetMusics();
		}
	}
	
	// Update is called once per frame
	void Update () {
		gamepadList = Input.GetJoystickNames();
		if (gamepadList.Length > 0)
		{
			this.GetComponentInChildren<UILabel>().text = "Press Start";
			if (Input.GetButton("Start"))
			{
				manager.ChangeMusic();
				Application.LoadLevel(levelName);
			}
		}
		if (gamepadList.Length == 0)
		{
			this.GetComponentInChildren<UILabel>().text = "Start";
		}
	}

	void OnClick() {
		//manager.clips = clips;
		manager.ChangeMusic();
		Application.LoadLevel(levelName);
	}

	public void GetMusics(){
		string path = Application.dataPath;
		DirectoryInfo infos = new DirectoryInfo(path);
		DirectoryInfo[] list = infos.GetDirectories();
		bool found = false;
		DirectoryInfo playlist = new DirectoryInfo(path);
		foreach(DirectoryInfo dir in list){
			if(dir.Name == "Playlist"){
				found = true;
				playlist = dir;
			}
		}
		if(!found){
			playlist = infos.CreateSubdirectory("Playlist");
		}
		FileInfo[] files = playlist.GetFiles();
		foreach(FileInfo file in files){
			if(file.Extension == ".wav" ||
			   file.Extension == ".ogg" ||
			   file.Extension == ".flac"){
				StartCoroutine(LoadFile(file.FullName));
			}
		}

		//Debug.Log(path);
	}

	IEnumerator LoadFile(string path)
	{
		WWW www = new WWW("file://" + path);
		Debug.Log("loading " + path);
		
		AudioClip clip = www.GetAudioClip(false);
		while(!clip.isReadyToPlay)
			yield return www;
		
		Debug.Log("done loading");

		clip.name = Path.GetFileName(path);
		clips.Add(clip);
		Debug.Log(clips.Count);
	}
}
