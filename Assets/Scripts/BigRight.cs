using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRight : MonoBehaviour
{
    public int currentRight;
    public int maxRight = 2;
    public FightView fv;
    // Start is called before the first frame update
    void Start()
    {
        ResetLevel();
    }

    public void IncreaseLevel()
    {
        currentRight++;
        fv.RightArmCharge++;
    }
    public void ResetLevel()
    {
        currentRight = 0;
        fv.RightArmCharge = 0;
    }
}
