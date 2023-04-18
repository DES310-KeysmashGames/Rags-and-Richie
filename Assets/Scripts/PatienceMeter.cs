using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceMeter : MonoBehaviour
{
    [SerializeField] private Image patienceArrow;
    private const float MAX_PATIENCE_ANGLE = -90;
    private const float MIN_PATIENCE_ANGLE = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float GetRotation(float patience)
    {
        float totalAngleSize = MIN_PATIENCE_ANGLE - MAX_PATIENCE_ANGLE;
        float patienceNormalized = patience / 100;

        return MAX_PATIENCE_ANGLE + (patienceNormalized * totalAngleSize);
    }

    public void SetRotation(float patience)
    {
        patienceArrow.enabled = true;
        patienceArrow.transform.eulerAngles = new Vector3(0,0,GetRotation(patience));
    }

    public void SetInactive()
    {
        patienceArrow.enabled = false;
    }
}
