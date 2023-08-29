using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    private bool test;
    private bool wobbleBool;
    private bool smooshBool;
    private bool wooshBool;
    [SerializeField] Animator Portrait;

    // Start is called before the first frame update
    void Start()
    {
        test = false;
        wobbleBool = false;
        smooshBool = false;
        wooshBool = false;
        Portrait = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SkipAnimation();
        }
    }

    public void SkipAnimation()
    {
        Portrait.speed = 15;
        Debug.Log("Animation Skipping");
    }




    public void SetBool()
    {
        test = true;
    }

    public bool GetBool()
    {
        return test;
    }

    public void SetWobbleBool()
    {
        wobbleBool = true;
    }
    public bool GetWoobleBool()
    {
        return wobbleBool;
    }

    public void SetSmooshBool()
    {
        smooshBool = true;
    }

    public bool GetSmooshBool()
    {
        return smooshBool;
    }

    public void SetWooshBool()
    {
        wooshBool = true;
    }

    public bool GetWooshBool()
    {
        return wooshBool;
    }

    public void ResetToFalse()
    {
        wooshBool = false;
        wobbleBool = false;
        smooshBool = false;
    }
}
