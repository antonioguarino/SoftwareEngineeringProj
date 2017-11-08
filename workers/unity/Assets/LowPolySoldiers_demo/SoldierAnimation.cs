using UnityEngine;
using System.Collections;

public class SoldierAnimation : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        //Get input from controls
        float z = Input.GetAxisRaw("Horizontal");
        float x = -(Input.GetAxisRaw("Vertical"));

        //Apply inputs to animator
        //animator.SetFloat("Input X", z);
        //animator.SetFloat("Input Z", -(x));

        if (x != 0 || z != 0)  //if there is some input
        {
            //set that character is moving
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }
        else
        {
            //character is not moving
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }
    }
}