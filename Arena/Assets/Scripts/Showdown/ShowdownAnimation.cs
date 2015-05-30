using UnityEngine;
using System.Collections;

public class ShowdownAnimation : MonoBehaviour 
{
    [SerializeField]
    private Animator animator1;

    [SerializeField]
    private Animator animator2;

    [SerializeField]
    private Animator animator3;

    public void StartShowdown()
    {
        animator1.SetTrigger("ShowdownStart");
        animator2.SetTrigger("ShowdownStart");
        animator3.SetTrigger("ShowdownStart");
    }
}
