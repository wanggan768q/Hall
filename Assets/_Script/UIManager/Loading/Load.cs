using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Load : MonoBehaviour {

    public GameObject sprotain;
    private int x = 0;
    // Use this for initialization
    void Start () {
        Invoke("Scence2Click", 2.5f);
        sprotain = GameObject.Find("JZBG");
    }
    // Update is called once per frame
    void Update()
    {
        x -= 10;
        sprotain.transform.Rotate(new Vector3(0, 0, x), 3.5f);
    }
    void Scence2Click()
    {
       
        if (ConfigGet.ConfigGetFlag)
        { Application.LoadLevel(1); }
       
    }
}
