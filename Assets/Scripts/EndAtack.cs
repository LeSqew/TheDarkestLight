using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAtack : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public void endAtack()
    {
        animator.SetTrigger("EndAtack");
    }
}
