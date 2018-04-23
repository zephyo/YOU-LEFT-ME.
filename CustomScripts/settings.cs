using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.Audio;
using TMPro;

public class settings : MonoBehaviour {

	new public AudioMixer audio;
	public TextMeshProUGUI PresetLabel;

	void Start(){
		PresetLabel.text=QualitySettings.names [QualitySettings.GetQualityLevel ()];
	}

	public void SfxVol(float s){
		audio.SetFloat("SfxVol",s);

	}
	public void MusicVol(float s){
		audio.SetFloat("MusicVol",s);
		
	}

	public void TextSpeed(float x){
		//60-200
		PlayerPrefs.SetFloat("speed",x);

	}

	public void QualityBack(){
		QualitySettings.DecreaseLevel();
		PresetLabel.text = QualitySettings.names [QualitySettings.GetQualityLevel ()];
	}

	public void QualityFo(){
		QualitySettings.IncreaseLevel();

		PresetLabel.text = QualitySettings.names [QualitySettings.GetQualityLevel ()];
	}
	
}
