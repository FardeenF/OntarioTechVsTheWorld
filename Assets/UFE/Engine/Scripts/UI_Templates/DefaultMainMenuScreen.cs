using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UFE3D;
using System;

public class DefaultMainMenuScreen : MainMenuScreen{
	#region public instance fields
	public AudioClip onLoadSound;
	public AudioClip music;
	public AudioClip selectSound;
	public AudioClip cancelSound;
	public AudioClip moveCursorSound;
	public bool stopPreviousSoundEffectsOnLoad = false;
	public float delayBeforePlayingMusic = 0.1f;

	public Button buttonNetwork;
	public Button buttonBluetooth;

    GameObject camera;

    public static event Action userClick;
    public static event System.Action<string> onlineClick;
    #endregion

    #region public override methods
    public override void DoFixedUpdate(
		IDictionary<InputReferences, InputEvents> player1PreviousInputs,
		IDictionary<InputReferences, InputEvents> player1CurrentInputs,
		IDictionary<InputReferences, InputEvents> player2PreviousInputs,
		IDictionary<InputReferences, InputEvents> player2CurrentInputs
	){
		base.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

		this.DefaultNavigationSystem(
			player1PreviousInputs,
			player1CurrentInputs,
			player2PreviousInputs,
			player2CurrentInputs,
			this.moveCursorSound,
			this.selectSound,
			this.cancelSound
		);
	}

	public override void OnShow (){
		base.OnShow ();
		this.HighlightOption(this.FindFirstSelectable());

		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.stopPreviousSoundEffectsOnLoad){
			UFE.StopSounds();
		}
		
		if (this.onLoadSound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.onLoadSound);}, this.delayBeforePlayingMusic);
		}

		if (buttonNetwork != null) {
			buttonNetwork.interactable = UFE.isNetworkAddonInstalled || UFE.isBluetoothAddonInstalled;
		}

		if (buttonBluetooth != null){
            buttonBluetooth.interactable = UFE.isBluetoothAddonInstalled;
        }
	}
    #endregion

    private void Start()
    {
        camera = UnityEngine.GameObject.Find("Camera");
        camera.GetComponent<AudioSource>().playOnAwake = true;
        camera.GetComponent<AudioSource>().enabled = true;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            #region observer
            userClick?.Invoke();
            #endregion
        }
    }

    public void disabledOnlineClick()
    {
        if(!buttonNetwork.interactable)
            onlineClick?.Invoke("Enable networking to play online.");
    }
}
