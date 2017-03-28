using UnityEngine;
using System.Collections;

public class ScrollRectScript : MonoBehaviour {
    public GameObject _Button;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            _Button.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _Button.SetActive(false);
        }
    }
}
