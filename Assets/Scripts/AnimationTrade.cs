using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    [SerializeField] Animator bargainPhaseAnimation;
    [SerializeField] Animator priceConfirmAnimation;
    [SerializeField] Animator patienceMeterAnimation;
    [SerializeField] Animator customerSpeakingAnimation;
    [SerializeField] Animator blinkingMoney;
    [SerializeField] Animator blinkingEmoticon;

    private void Awake()
    {

    }


    // Initial Price Animations
    public void BargainPhaseSetActive()
    {
        bargainPhaseAnimation.SetBool("IsActive", true);
    }

    public void BargainPhaseSetInactive()
    {
        bargainPhaseAnimation.SetBool("IsActive", false);
    }


    // Price Adjuster Animation drop down

    public void PriceConfirmSetActive()
    {
        priceConfirmAnimation.SetBool("IsActive", true);
    }

    public void PriceConfirmSetInactive()
    {
        priceConfirmAnimation.SetBool("IsActive", false);
    }


    // Patience Meter Animation
    public void PatienceMeterActive()
    {
        patienceMeterAnimation.SetBool("IsActive", true);
    }

     public void PatienceMeterInActive()

    {
       patienceMeterAnimation.SetBool("IsActive", false);
    }

    // Customer Bobbing / Speaking Animation / Arrive / Leave
    public void CustomerSpeakingActive()
    {
        customerSpeakingAnimation.SetBool("IsActive", true);
        Invoke("CustomerSpeakingInActive", 3);
    }

    public void CustomerSpeakingInActive()

    {
        customerSpeakingAnimation.SetBool("IsActive", false);
    }

    public void CustomerSpeakingArrive()
    {
        customerSpeakingAnimation.SetTrigger("Arrive");
    }

    public void CustomerSpeakingLeave()
    {
        customerSpeakingAnimation.SetTrigger("Leave");
    }


    // Blinking Currency Animation
    public void BlinkingCurrencyActive()
    {
        blinkingMoney.SetBool("IsActive", true);
        Invoke("BlinkingCurrencyInActive", 3);
    }

    public void BlinkingCurrencyInActive()

    {
        blinkingMoney.SetBool("IsActive", false);
    }

    // Blinking Emoticon Animation

    public void BlinkingEmoticonActive()
    {
        blinkingEmoticon.SetBool("IsActive", true);
        Invoke("BlinkingEmoticonInActive", 2);
    }

    public void BlinkingEmoticonInActive()

    {
        blinkingEmoticon.SetBool("IsActive", false);
    }



}


