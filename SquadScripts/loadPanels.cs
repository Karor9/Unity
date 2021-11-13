using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadPanels : MonoBehaviour
{
    public Transform panel;
    public GameObject playerPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("Logo");
        LoadSprite other = (LoadSprite)go.GetComponent(typeof(LoadSprite));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
