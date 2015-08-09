using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowName : MonoBehaviour {
	
	public Text infoNameText;

	void Start(){

	}

	void Update () {
		infoNameText.text = Camera.main.GetComponent<CameraFollow>().targetName;
	}
}
