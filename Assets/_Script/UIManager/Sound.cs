using UnityEngine;
using System.Collections;
using DateDeclare;

public class Sound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void onCound()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
}
