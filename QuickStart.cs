using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickStart
{
	[KSPAddon (KSPAddon.Startup.MainMenu, false)]
	public class QuickStart : MonoBehaviour
	{
		private struct SaveInfo
		{
			public SaveInfo(String name, String path)
			{
				this.name = name;
				this.path = path;
			}

			public String name;
			public String path;
		}

		public void Awake()
		{
			Debug.Log ("Searching for saves.");
			foreach (String directory in Directory.GetDirectories(KSPUtil.ApplicationRootPath + "/saves")) {
				Debug.Log ("Found save directory: " + directory);
				String saveFileName = directory + "/persistent.sfs";
				Debug.Log ("Checking save file: " + saveFileName);
				if (File.Exists (saveFileName)) {
					String saveName = new DirectoryInfo (directory).Name;
					Debug.Log ("Save file found: " + saveName);
					saveFiles.Add (new SaveInfo (saveName, saveFileName));
				}
			}
		}

		public void OnGUI()
		{
			GUILayout.Window(1, new Rect(Screen.width - 200, 0, 200, 300), DrawWindow, "Quick Start",
				HighLogic.Skin.window);
		}

		private void DrawWindow(int id)
		{
			GUILayout.BeginVertical ();
			foreach (SaveInfo saveInfo in saveFiles) {
				if (GUILayout.Button(saveInfo.name)) {
					StartGame (saveInfo.path);
				}
			}
			GUILayout.EndVertical ();
		}

		private void StartGame(String path)
		{
			Debug.Log ("Save file: " + path);
		}

		private List<SaveInfo> saveFiles = new List<SaveInfo>();
	}
}

