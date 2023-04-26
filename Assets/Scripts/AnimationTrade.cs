using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    Animator bargainPhaseAnimation;

    private void Awake()
    {
        bargainPhaseAnimation = GetComponent<Animator>();
    }

    public void BargainPhaseSetActive()
    {
        bargainPhaseAnimation.SetBool("IsActive", true);
    }
    
    public void BargainPhaseSetInactive()
    {
        bargainPhaseAnimation.SetBool("IsActive", false);
    }

}
