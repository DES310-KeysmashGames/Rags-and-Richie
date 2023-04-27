using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    [SerializeField] Animator bargainPhaseAnimation;
    [SerializeField] Animator priceConfirmAnimation;
    [SerializeField] Animator patienceMeterAnimation;
    [SerializeField] Animator customerSpeakingAnimation;

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

    public void PriceConfirmSetActive()
    {
        priceConfirmAnimation.SetBool("IsActive", true);
    }

    public void PriceConfirmSetInactive()
    {
        priceConfirmAnimation.SetBool("IsActive", false);
    }

    public void PatienceMeterActive()
    {
        patienceMeterAnimation.SetBool("IsActive", true);
    }

     public void PatienceMeterInActive()

    {
       patienceMeterAnimation.SetBool("IsActive", false);
    }

    public void CustomerSpeakingActive()
    {
        customerSpeakingAnimation.SetBool("IsActive", true);
    }

    public void CustomerSpeakingIsActive()

    {
        customerSpeakingAnimation.SetBool("IsActive", false);
    }






}


