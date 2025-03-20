using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurnManager : MonoBehaviour
{
    public BulletFire leftBullet;
    public BulletFire rightBullet;

    bool leftButtonPressed = false;
    bool rightButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        leftBullet.fireButton.interactable = true;
        rightBullet.fireButton.interactable = false;
        StartCoroutine(RunTurns());
    }

    IEnumerator RunTurns()
    {
        while (true)
        {
            leftButtonPressed = false;
            leftBullet.fireButton.interactable = true;
            while (true)
            {
                if (leftButtonPressed) break;
                yield return null;
            }
            yield return StartCoroutine(leftBullet.FireBulletCoroutine());

            rightButtonPressed = false;
            rightBullet.fireButton.interactable = true;
            while (true)
            {
                if (rightButtonPressed) break;
                yield return null;
            }
            yield return StartCoroutine(rightBullet.FireBulletCoroutine());
        }
    }

    public void LeftButtonPressed()
    {
        leftButtonPressed = true;
    }

    public void RightButtonPressed()
    {
        rightButtonPressed = true;
    }
}
