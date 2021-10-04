using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBrain : MonoBehaviour
{
    public RectTransform rt;
    public int currentBrain;
    public int maxBrain = 8;
    // Start is called before the first frame update
    void Start()
    {
        ResetLevel();
    }

    public void IncreaseLevel()
    {
        currentBrain++;
        float scaleLevel = ((currentBrain*1f)/(maxBrain*1f)) * 10;
        rt.localScale = new Vector3(6.3430f, scaleLevel, 1f);
    }
    public void ResetLevel()
    {
        currentBrain = 0;
        rt.localScale = new Vector3(6.3430f, currentBrain, 1f);
    }
}
