using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    [SerializeField] Animator bargainPhaseAnimation;

    private void Awake()
    {
        
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
