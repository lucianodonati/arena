using UnityEngine;
using System.Collections;

public class ToggleFullscreen : MonoBehaviour
{
    // Use this for initialization
    public void ToggleFS()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}