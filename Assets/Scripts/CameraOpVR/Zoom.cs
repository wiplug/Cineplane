﻿using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {
	
	//public bool on = false;
	ViveInput viveInput;
	bool zoomEnabled = true;
	CpPlayback cpPlayback;
	float fov = 63.25f;
	float focalLengthInMm = 16;
	float sensorHeightInMm = (35f * (9f/16f));
	TextMesh focalLengthText;
	Camera cameraMain;
    CpMainCamBehavior cpMainCamBehavior;


	// Use this for initialization
	void Start () {
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
		cpPlayback = GameObject.Find ("CameraPlayback").GetComponent<CpPlayback> ();
		focalLengthText = GameObject.Find ("FocalLengthText").GetComponent<TextMesh> ();
		cameraMain = GameObject.Find ("CameraMain").GetComponent<Camera>();
        cpMainCamBehavior = GameObject.Find("cpMainCamBehavior").GetComponent<CpMainCamBehavior>();
	}
	
	void Update() {
		if (zoomEnabled) {
			ChangeZoom ();
		}
	}

	void ChangeZoom() {
		if (Mathf.Abs (viveInput.right.touchPad.y) > 0.1 && viveInput.right.touchPad.pressed) {
			fov -= viveInput.right.touchPad.y;
			fov = Mathf.Clamp (fov, 10, 80);
		}

		if (!cpPlayback.playbackOn) {
			cameraMain.fieldOfView = fov;
		}

		ChangeFocalLengthMonitorDisplay ();
	}

	void ChangeFocalLengthMonitorDisplay() {
		if (zoomEnabled) {
			float zoomFocalLengthInMm = sensorHeightInMm / (2.0f * Mathf.Tan (0.5f * fov * Mathf.Deg2Rad));
			focalLengthText.text = "Z" + zoomFocalLengthInMm.ToString ("f1") + "mm";
		} else {
			focalLengthText.text = focalLengthInMm.ToString() + "mm";		            
		}
	}
	
	public void SetSensorHeightInMm(float newSensorHeightInMm) {
		sensorHeightInMm = newSensorHeightInMm;
		fov = Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorHeightInMm / (2.0f * focalLengthInMm));
	}
}