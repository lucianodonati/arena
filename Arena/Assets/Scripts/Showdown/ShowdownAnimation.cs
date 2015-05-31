using System.Collections;
using UnityEngine;

public class ShowdownAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator1 = null;

    [SerializeField]
    private Animator animator2 = null;

    [SerializeField]
    private Animator animator3 = null;

    public void StartShowdown()
    {
        animator1.SetTrigger("ShowdownStart");
        animator2.SetTrigger("ShowdownStart");
        animator3.SetTrigger("ShowdownStart");
    }
}