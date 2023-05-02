using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    [SerializeField] Animator bargainPhaseAnimation;
    [SerializeField] Animator priceConfirmAnimation;
    [SerializeField] Animator patienceMeterAnimation;
    [SerializeField] Animator customerAnimations;
    [SerializeField] Animator speechBubble;
    [SerializeField] Animator blinkingMoney;
    [SerializeField] Animator blinkingEmoticon;
    [SerializeField] Animator shelfLock;
    [SerializeField] Animator tutorialTravelText;


    private void Awake()
    {

    }

    // Tutorial Text Animations

    public void TutorialText()
    {
        tutorialTravelText.SetTrigger("Tutorial");
    }

    public void NoneTutorialText()
    {
        tutorialTravelText.SetTrigger("NoTutorial");
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
        patienceMeterAnimation.SetTrigger("Drop");
    }

     public void PatienceMeterInActive()

    {
        patienceMeterAnimation.SetTrigger("Raise");
    }

    // Customer Bobbing / Speaking Animation / Arrive / Leave
    public void CustomerSpeakingActive()
    {
        customerAnimations.SetTrigger("Speaking");
    }

    public void CustomerSpeakingArrive()
    {
        customerAnimations.SetTrigger("Arrive");
    }

    public void CustomerSpeakingLeave()
    {
        customerAnimations.SetTrigger("Leave");
    }


    // Speech Bubble

    public void SpeechBubble()
    {
        speechBubble.SetTrigger("Speaking");
    }


    // Blinking Currency Animation
    public void BlinkingCurrencyActive()
    {
        blinkingMoney.SetTrigger("Blink");
    }

    // Blinking Emoticon Animation

    public void BlinkingEmoticonActive()
    {
        blinkingEmoticon.SetTrigger("Blink");
    }

    public void ShelfClose()
    {
        shelfLock.SetTrigger("Close");
    }

    public void ShelfOpen()
    {
        shelfLock.SetTrigger("Open");
    }


}


