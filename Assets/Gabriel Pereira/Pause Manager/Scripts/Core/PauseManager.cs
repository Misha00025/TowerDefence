using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if PAUSE_MANAGER_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
#if PAUSE_MANAGER_REWIRED
using Rewired;
#endif

namespace PauseManagement.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class PauseManager : MonoBehaviour
	{
		public delegate void PauseDelegateAction(bool paused);

		public static event PauseDelegateAction PauseAction;

		/// <summary>
		/// Use Unity's timeScale to stop time when paused ?
		/// </summary>
		public bool useTimeScale = true;

		/// <summary>
		/// Use Unity's Input Manager button to pause ?
		/// </summary>
		[SerializeField]
		private bool useUnityInputManager = true;

		/// <summary>
		/// The list of buttons to pause/resume.
		/// Can be used for local multiplayer (eg. Player 1 Pause, Player 2 Pause, etc).
		/// Default is one entry with "Cancel" value.
		/// </summary>
		[SerializeField]
		private string[] m_ButtonsList = null;

		/// <summary>
		/// Use Unity's Input System
		/// </summary>
		public bool useUnityInputSystem = false;

#if PAUSE_MANAGER_INPUT_SYSTEM
		/// <summary>
		/// The pause's input action
		/// </summary>
		public InputAction pauseAction = null;

		/// <summary>
		/// Use Input Action Asset's reference ?
		/// </summary>
		public bool useActionReference = false;

		/// <summary>
		/// The Input Action Asset's reference to apply to pauseInputAction
		/// </summary>
		public InputActionReference pauseActionReference = null;
#endif

		/// <summary>
		/// Use Rewired
		/// </summary>
		public bool useRewired = false;

#if PAUSE_MANAGER_REWIRED
		/// <summary>
		/// Pause when controller is disconnected.
		/// </summary>
		[SerializeField]
		private bool m_PauseOnControllerDisconnect = true;

		/// <summary>
		/// Resume when controller is connected.
		/// </summary>
		[SerializeField]
		private bool m_ResumeOnControllerConnect = true;

		/// <summary>
		/// Check all players for input.
		/// </summary>
		[SerializeField]
		private bool m_CheckAllPlayers = true;

		/// <summary>
		/// Optionally include the System Player ?
		/// </summary>
		[SerializeField]
		private bool m_IncludeSystemPlayer = false;

		/// <summary>
		/// The ID of players used to check for input.
		/// </summary>
		[SerializeField]
		private int[] m_PlayerIds = null;

		/// <summary>
		/// The name of the actions that represent Pause/Resume
		/// </summary>
		[SerializeField]
		private string[] m_ActionNames = null;

		/// <summary>
		/// The list of players acquired from Rewired
		/// </summary>
		private IList<Player> m_Players = null;
#endif

		/// <summary>
		/// Assign custom pause button from PlayerPrefs
		/// </summary>
		[SerializeField]
		private bool assignKeyFromPrefs = false;

		/// <summary>
		/// The list of property's name from PlayerPrefs to pause/resume.
		/// Can be used for local multiplayer (eg. Player 1 Pause, Player 2 Pause, etc).
		/// Default is one entry with 'Cancel' value.
		/// </summary>
		[SerializeField]
		private string[] m_PropertiesList = null;

		/// <summary>
		/// Custom keys for pausing, if you don't use Unity's Input Manager.
		/// </summary>
		[SerializeField]
		private KeyCode[] m_PauseKeys = null;

		/// <summary>
		/// Events triggered when paused
		/// </summary>
		[SerializeField]
		private UnityEvent pauseEvent = null;

		/// <summary>
		/// Events triggered when resumed
		/// </summary>
		[SerializeField]
		private UnityEvent resumeEvent = null;

		/// <summary>
		/// 
		/// </summary>
		private bool m_ExecuteEvents = true;

		/// <summary>
		/// 
		/// </summary>
		private bool m_ExecuteDelegateActions = true;

		// Reset to default values
		void Reset()
		{
			m_ButtonsList = new string[]
			{
				"Cancel"
			};
			m_PropertiesList = new string[]
			{
				"Pause"
			};
			m_PauseKeys = new KeyCode[]
			{
				KeyCode.Escape
			};

#if PAUSE_MANAGER_REWIRED
			m_PlayerIds = new int[] { 0 };
			m_ActionNames = new string[0];
#endif
		}

		// Awake is called before Start function
		void Awake()
		{
#if PAUSE_MANAGER_INPUT_SYSTEM
			if (useUnityInputSystem)
			{
				if (useActionReference && pauseActionReference)
					pauseAction = pauseActionReference.action;

				pauseAction.performed += _ => TogglePause();
			}
#else
			useUnityInputSystem = false;
#endif

#if PAUSE_MANAGER_REWIRED
			if (useRewired)
			{
				if (ReInput.isReady && ReInput.players != null)
				{
					if (m_CheckAllPlayers)
						m_Players = ReInput.players.GetPlayers(m_IncludeSystemPlayer);
				}
			}
#else
			useRewired = false;
#endif

			if (assignKeyFromPrefs)
			{
				m_PauseKeys = new KeyCode[m_PropertiesList.Length];
				for (int i = 0; i < m_PropertiesList.Length; i++)
				{
					m_PauseKeys[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(m_PropertiesList[i], "Escape"));

					SavePauseKeyOnPrefs(m_PropertiesList[i], m_PauseKeys[i]);
				}
			}

			IsPaused = false;
		}

		// This function is called when the object becomes enabled and active
		void OnEnable()
		{
#if PAUSE_MANAGER_INPUT_SYSTEM
			m_PauseAction.Enable();
#endif
#if PAUSE_MANAGER_REWIRED
			ReInput.ControllerConnectedEvent += OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
#endif
		}

		// This function is called when the behaviour becomes disabled.
		void OnDisable()
		{
#if PAUSE_MANAGER_INPUT_SYSTEM
			m_PauseAction.Disable();
#endif
#if PAUSE_MANAGER_REWIRED
			ReInput.ControllerConnectedEvent -= OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= OnControllerDisconnected;
#endif
		}

#if PAUSE_MANAGER_REWIRED
		// This function will be called when a controller is connected
		// You can get information about the controller that was connected via the args parameter
		void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			if (m_ResumeOnControllerConnect)
				Resume();
		}

		// This function will be called when a controller is fully disconnected
		// You can get information about the controller that was disconnected via the args parameter
		void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (m_PauseOnControllerDisconnect)
				Pause();
		}
#endif

		// Update is called once per frame
		void Update()
		{
			if (useUnityInputSystem) return;

#if PAUSE_MANAGER_REWIRED
			if (useRewired)
			{
				if (!ReInput.isReady || ReInput.players == null) return;

				if (m_CheckAllPlayers)
				{
					if (m_Players == null)
						m_Players = ReInput.players.GetPlayers(m_IncludeSystemPlayer);

					foreach (var player in m_Players)
					{
						foreach (var actionName in m_ActionNames)
						{
							if (player.GetButtonDown(actionName))
								TogglePause();
						}
					}
				}
				else
				{
					foreach (var playerId in m_PlayerIds)
					{
						var player = ReInput.players.GetPlayer(playerId);
						if (player != null)
						{
							foreach (var actionName in m_ActionNames)
							{
								if (player.GetButtonDown(actionName))
									TogglePause();
							}
						}
					}
				}

				return;
			}
#endif

			if (useUnityInputManager)
			{
				foreach (var buttonName in m_ButtonsList)
					if (Input.GetButtonDown(buttonName))
						TogglePause();
			}
			else
			{
				foreach (var key in m_PauseKeys)
					if (Input.GetKeyDown(key))
						TogglePause();
			}
		}

		void OnApplicationPause(bool pause)
		{
			if (pause && !IsPaused)
				Pause();
		}

		public void TogglePause()
		{
			if (!IsPaused)
				Pause();
			else
				Resume();
		}

		public void Pause()
		{
			if (useTimeScale)
				StopTime();

			IsPaused = true;

			if (m_ExecuteEvents)
				pauseEvent.Invoke();

			if (m_ExecuteDelegateActions && PauseAction != null)
				PauseAction.Invoke(IsPaused);
		}

		public void Resume()
		{
			ResetTime();

			IsPaused = false;

			if (m_ExecuteEvents)
				resumeEvent.Invoke();

			if (m_ExecuteDelegateActions && PauseAction != null)
				PauseAction.Invoke(IsPaused);
		}

		public void StopTimeDelayed(float time)
		{
			Invoke(nameof(StopTime), time);
		}

		public void StopTime()
		{
			Time.timeScale = 0;
		}

		public void ResetTimeDelayed(float time)
		{
			Invoke(nameof(ResetTime), time);
		}

		public void ResetTime()
		{
			Time.timeScale = 1;
		}

		public void SavePauseKeyOnPrefs(string key, KeyCode keyCode)
		{
			PlayerPrefs.SetString(key, keyCode.ToString());
		}

		public static bool IsPaused { get; set; }

		public bool ExecuteEvents
		{
			set
			{
				m_ExecuteEvents = value;
			}
		}

		public bool ExecuteDelegateActions
		{
			set
			{
				m_ExecuteDelegateActions = value;
			}
		}
	}
}