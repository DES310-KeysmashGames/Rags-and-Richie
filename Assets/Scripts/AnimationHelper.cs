using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    private bool test;

    // Start is called before the first frame update
    void Start()
    {
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBool()
    {
        test = true;
    }

    public bool GetBool()
    {
        return test;
    }
}
