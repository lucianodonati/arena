using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string p1Name { get; set; }

    public string p2Name { get; set; }

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void GTFO()
    {
        Application.Quit();
    }
}