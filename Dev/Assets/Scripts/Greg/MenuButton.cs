using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using NAudio;
using NAudio.Wave;
public class MenuButton : MonoBehaviour {

	public string levelName;
	public AudioManager manager;
	public List<AudioClip> clips;
	private string[] gamepadList;
	private IWavePlayer mWaveOutDevice;
	private WaveStream mMainOutputStream;
	private WaveChannel32 mVolumeStream;
	// Use this for initialization
	void Start () {
		GameObject otherManager = GameObject.FindGameObjectWithTag("AudioManager");
		if(!otherManager){
			otherManager =  (GameObject)Instantiate(manager.gameObject);
			clips = new List<AudioClip>();
			manager = otherManager.GetComponent<AudioManager>();
			manager.clips = clips;
			GetMusics();
		} else{
			manager = otherManager.GetComponent<AudioManager>();
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
		AudioClip clip;
		clip = www.GetAudioClip(false);

		while(!clip.isReadyToPlay)
			yield return www;
		
		clip.name = Path.GetFileName(path);
		clips.Add(clip);
	}

//	IEnumerator LoadMP3(string path)
//	{
//		WWW www = new WWW("file://" + path);
//		Debug.Log("loading " + path);
//
//		byte[] imageData = www.bytes;
//		LoadAudioFromData(imageData);
//		
//		mWaveOutDevice.Play();
//		yield return www;
//	}
//
//	private bool LoadAudioFromData(byte[] data)
//	{
//		try
//		{
//			MemoryStream tmpStr = new MemoryStream(data);
//			mMainOutputStream = new Mp3FileReader(tmpStr);
//			mVolumeStream = new WaveChannel32(mMainOutputStream);
//			
//			mWaveOutDevice = new WaveOut();
//			mWaveOutDevice.Init(mVolumeStream);
//			
//			return true;
//		}
//		catch (System.Exception ex)
//		{
//			Debug.LogWarning("Error! " + ex.Message);
//		}
//		
//		return false;
//	}
}
