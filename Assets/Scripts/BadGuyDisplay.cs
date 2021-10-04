using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyDisplay : MonoBehaviour
{
    public Sprite neutralBad;
    public Sprite badGuyDamage;
    public Sprite badLaser;
    public Sprite badRocketPunch;
    public Sprite badSpecial;
    public Sprite badTie;

    public void UpdateSprite(int roboAction)
    {
        switch (roboAction)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = badLaser;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = badRocketPunch;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = badSpecial;
                break;
        }
        
    }
    public void DamagePose()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = badGuyDamage;
    }
    public void TiePose()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = badTie;
    }
}
