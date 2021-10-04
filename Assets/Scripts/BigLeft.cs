using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLeft : MonoBehaviour
{
    public int currentLeft;
    public int maxLeft = 3;
    public FightView fv;
    // Start is called before the first frame update
    void Start()
    {
        ResetLevel();
    }

    public void IncreaseLevel()
    {
        currentLeft++;
        fv.LeftArmCharge++;
    }
    public void ResetLevel()
    {
        currentLeft = 0;
        fv.LeftArmCharge = 0;
    }
}
