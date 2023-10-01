using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrade : MonoBehaviour
{
    //Trade Scene Animations
    [Header("Trade Scene Animations")]
    [SerializeField] Animator bargainPhaseAnimation;
    [SerializeField] Animator priceConfirmAnimation;
    [SerializeField] Animator PlayerUserInterface;
    [SerializeField] Animator customerAnimations;
    [SerializeField] Animator speechBubble;
    [SerializeField] Animator blinkingMoney;
    [SerializeField] Animator blinkingEmoticon;
    [SerializeField] Animator shelfLock;
    [SerializeField] Animator OpeningDay;
    [SerializeField] Animator DayEndingClose;
    [SerializeField] Animator richieReactions;
    [SerializeField] Animator itemcardAppear;
    [SerializeField] Animator customerSales;

    //Travel Scene Animations
    [Header("Travel Scene Animations")]
    [SerializeField] Animator CityPulse;
    [SerializeField] Animator truckVroom;
    [SerializeField] Animator tutorialTravelText;
    [SerializeField] Animator travelopenShutter;
    [SerializeField] Animator travelcloseShutter;

    // End of Day Animation
    [Header("EOD Animations")]
    [SerializeField] Animator FanFarePortraits;

    // Main Menu
    [Header("Main Menu Animations")]
    [SerializeField] Animator mainmenutransition;

    GameManager gameManager;




    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    // Truck Animation Travel Scene




    public void MainMenuTransition()
    {
        mainmenutransition.SetTrigger("Close");
    }


    public void TravelOpenShutter()
    {
        travelopenShutter.SetTrigger("Open");
    }

    public void TravelCloseShutter()
    {
        travelcloseShutter.SetTrigger("Close");
    }
    //RICHIE ANIMATION REACTIONS
    public void RichieGood()
    {
        richieReactions.SetTrigger("Good");
    }

    public void RichiePerfect()
    {
        richieReactions.SetTrigger("Perfect");
    }

    public void RichieBad()
    {
        richieReactions.SetTrigger("Bad");
    }

    public void RichieIdle()
    {
        richieReactions.SetTrigger("Idle");
    }



    public void TruckDrive()
    {
        truckVroom.SetTrigger("Vroom");
    }

    // City Breathing
    public void CityButtons()
    {
        CityPulse.SetTrigger("Pulse"); // Makes Icon Pulsate
    }

    public void CityButtonsReset()
    {
        CityPulse.SetTrigger("Stop"); // Makes Icon Stop Pulsing
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


    //ItemCard Hover Animations

    public void ItemHoverEnter()
    {
        itemcardAppear.SetTrigger("Hover");
    }

    public void ItemHoverClose()
    {
        itemcardAppear.SetTrigger("NoHover");
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


    // UserInterface Animations
    public void DropUI()
    {
        PlayerUserInterface.SetTrigger("DropUI");
    }

     public void RaiseUI()

    {
        PlayerUserInterface.SetTrigger("RaiseUI");
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


    public void DayStarting()
    {
        OpeningDay.SetTrigger("Opening");
    }


    public void DayEnding()
    {
        DayEndingClose.SetTrigger("Closing");
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

    public void HappyCustomerSale()
    {
        customerSales.SetTrigger("Happy");
    }

    public void AngryCustomerSale()
    {
        customerSales.SetTrigger("Angry");
    }

    public void TippedCustomerSale()
    {
        customerSales.SetTrigger("Tipped");
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


    public void PortraitAnimation()
    {
        FanFarePortraits.SetTrigger("Play");
    }

    public void PortraitsClose()
    {
        FanFarePortraits.SetTrigger("Close");
    }


    public void CloseEndTradeOverview()
    {
        FanFarePortraits.SetTrigger("Close");
    }


    public void ReshuffleItems()
    {
        shelfLock.SetTrigger("Shuffle");
    }

}


