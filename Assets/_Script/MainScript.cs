using UnityEngine;
using System.Collections;
using SimpleJson;

public class MainScript : MonoBehaviour
{
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        Loadingview_Controller.Instance.Open();
    }
}
