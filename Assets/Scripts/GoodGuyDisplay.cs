using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuyDisplay : MonoBehaviour
{
    public Sprite neutralGood;
    public Sprite goodGuyDamage;
    public Sprite goodLaser;
    public Sprite goodRocketPunch;
    public Sprite goodSpecial;
    public Sprite goodTie;

    public void UpdateSprite(int roboAction)
    {
        switch (roboAction)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = goodLaser;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = goodRocketPunch;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = goodSpecial;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = neutralGood;
                break;
        }
        
    }
    public void DamagePose()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = goodGuyDamage;
    }
    public void TiePose()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = goodTie;
    }
}
