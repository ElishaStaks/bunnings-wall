using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : MonoBehaviour
{
    private Animator m_Anim;

    private void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        transform.parent.position += m_Anim.deltaPosition;
    }
}
