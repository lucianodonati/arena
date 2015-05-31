using System.Collections;
using UnityEngine;

public class PlayerShowdownScript : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetColor(Color _color)
    {
        GetComponent<SpriteRenderer>().color = _color;
    }
}