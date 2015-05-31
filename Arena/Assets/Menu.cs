using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public int m_nSelection;
    private Texture2D m_tSelectedButton;
    private Texture2D m_tDefaultButton;
    public string hover;

    // Use this for initialization
    private void Start()
    {
        m_nSelection = 0;
        m_tSelectedButton = (Texture2D)Resources.Load("buttonActive");
        m_tDefaultButton = (Texture2D)Resources.Load("button");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_nSelection++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_nSelection--;
        }
        if (m_nSelection < 0)
        {
            m_nSelection = 2;
        }
        if (m_nSelection > 2)
        {
            m_nSelection = 0;
        }
    }

    private void OnGUI()
    {
        string[] options = new string[3];
        options[0] = "PLAY";
        options[1] = "CREDITS";
        options[2] = "EXIT";

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.font = (Font)Resources.Load("Faith Collapsing");
        style.fontSize = 32;

        for (int i = 0; i < 3; i++)
        {
            if (i == m_nSelection)
            {
                style.normal.background = m_tSelectedButton;
            }
            else
            {
                style.normal.background = m_tDefaultButton;
            }
            style.hover.background = m_tSelectedButton;
            if (GUI.Button(new Rect(Screen.width / 2.0f - 250, 200 + 100 * i, 500, 50.0f), new GUIContent(options[i], options[i]), style))
            {
                if (m_nSelection == 0)
                {
                    Application.LoadLevel(1);
                }
                else if (m_nSelection == 1)
                {
                    Application.LoadLevel(2);
                }
                else if (m_nSelection == 2)
                {
                    Application.Quit();
                }
            }
            hover = GUI.tooltip;

            for (int j = 0; j < 3; j++)
            {
                if (hover == options[j])
                {
                    m_nSelection = j;
                }
            }
        }
    }
}