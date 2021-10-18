using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    [SerializeField] Text headline;

    [SerializeField] Animator animator1; // Main
    [SerializeField] Animator animator2; // Hud

    public void animateMenu()
    {
        animator1.SetInteger("mainState", 1);
        animator2.SetInteger("hudState", 1);
    }

    public void SetLevelText(string name)
    {
        headline.text = "LVL " + name;
    }
}

