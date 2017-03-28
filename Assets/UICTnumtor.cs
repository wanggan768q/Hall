using UnityEngine;
using System.Collections;

public class UICTnumtor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(startSendTest());
	}
    IEnumerator startSendTest()
    {

        WWWForm wf = new WWWForm();
        //wf.AddField(ss);
        WWW www = new WWW("106.15.39.211/examples");
        yield return www;
        Debug.Log(www.text);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
