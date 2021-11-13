using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScene : MonoBehaviour
{
    public Button button;
    public int sceneNumber;
    void Start()
    {
        button.onClick.AddListener(change);
    }

    public void change()
    {
        SceneManager.LoadScene(sceneNumber);
    }

}
