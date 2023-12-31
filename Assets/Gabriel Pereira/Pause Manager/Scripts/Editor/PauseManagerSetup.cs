﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace PauseManagement.Editor
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public class PauseManagerSetup
	{
		#region Scripting Define Symbols

		/// <summary>
		/// Scripting Define Symbol for Unity's Input System
		/// </summary>
		private const string INPUT_SYSTEM_DEFINE = "PAUSE_MANAGER_INPUT_SYSTEM";

		/// <summary>
		/// Scripting Define Symbol for Rewired's Input System
		/// </summary>
		private const string REWIRED_DEFINE = "PAUSE_MANAGER_REWIRED";

		#endregion

		#region Types

		/// <summary>
		/// Rewired type
		/// </summary>
		private const string REWIRED_TYPE_NAME = "Rewired.ReInput, Rewired_Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

		#endregion

		private static List<string> Symbols = new List<string>();

		// Minimum version of Unity's Input System required is 0.2.10-preview
		private const int MINIMUM_RELEASE_VERSION = 0;
		private const int MINIMUM_MAJOR_VERSION = 2;
		private const int MINIMUM_MINOR_VERSION = 10;

		/// <summary>
		/// Eveytime Unity loads, the static constructor is executed
		/// </summary>
#if UNITY_EDITOR
		static PauseManagerSetup()
		{
			AcquireDefineSymbols();

			HandlePackagePresency("com.unity.inputsystem", INPUT_SYSTEM_DEFINE);

			HandleTypePresency(REWIRED_TYPE_NAME, REWIRED_DEFINE);

			SaveDefineSymbols();
		}
#endif

		/// <summary>
		/// 
		/// </summary>
		private static void AcquireDefineSymbols()
		{
			var group = EditorUserBuildSettings.selectedBuildTargetGroup;

			var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);

			Symbols = definesString.Split(';').ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="packageNameOrId"></param>
		/// <param name="define"></param>
		private static void HandlePackagePresency(string packageNameOrId, string define)
		{
			// Getting package's full path
			string path = Path.GetFullPath(string.Format("Packages/{0}", packageNameOrId));

			// Checking if Input System is installed by searching for the package in 'Packages' directory
			bool isPresent = Directory.Exists(path);

			if (isPresent)
			{
				string file = Path.GetFileName(path);
				string version = file.Split('@')[1];
				int release = int.Parse(version.Split('.')[0]);
				int major = int.Parse(version.Split('.')[1]);
				int minor = int.Parse(version.Split('.')[2].Split('-')[0]);

				if (release > MINIMUM_RELEASE_VERSION)
					AddDefines(define);
				else if (release == MINIMUM_RELEASE_VERSION)
					if (major > MINIMUM_MAJOR_VERSION)
						AddDefines(define);
					else if (major == MINIMUM_MAJOR_VERSION)
						if (minor >= MINIMUM_MINOR_VERSION)
							AddDefines(define);
						else
							RemoveDefines(define);
					else
						RemoveDefines(define);
				else
					RemoveDefines(define);
			}
			else
			{
				RemoveDefines(define);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="defines"></param>
		private static void HandleTypePresency(string typeName, params string[] defines)
		{
			if (IsTypePresent(typeName))
				foreach (var define in defines)
					AddDefines(define);
			else
				RemoveDefines(defines);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		private static bool IsTypePresent(string typeName)
		{
			return Type.GetType(typeName, false) != null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="defines"></param>
		private static void RemoveDefines(params string[] defines)
		{
			foreach (var define in defines)
				Symbols.Remove(define);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="defines"></param>
		private static void AddDefines(params string[] defines)
		{
			foreach (var define in defines)
			{
				if (IsDefineAdded(define)) return;

				Symbols.Add(define);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="define"></param>
		/// <returns></returns>
		private static bool IsDefineAdded(string define)
		{
			return Symbols.Contains(define);
		}

		/// <summary>
		/// 
		/// </summary>
		private static void SaveDefineSymbols()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(
					 EditorUserBuildSettings.selectedBuildTargetGroup,
					 string.Join(";", Symbols.ToArray())
			);
		}
	}
}