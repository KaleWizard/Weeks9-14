using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarMovementManager : MonoBehaviour
{
    public CarVectorLerp car;

    public Button increaseX;
    public Button decreaseX;
    public TextMeshProUGUI textX;

    public Button increaseY;
    public Button decreaseY;
    public TextMeshProUGUI textY;

    public Button startCarButton;

    Vector2Int velocityChange = Vector2Int.zero;

    // Start is called before the first frame update
    void Start()
    {
        increaseX.onClick.AddListener(() => { ChangeX(1); });
        decreaseX.onClick.AddListener(() => { ChangeX(-1); });
        increaseY.onClick.AddListener(() => { ChangeY(1); });
        decreaseY.onClick.AddListener(() => { ChangeY(-1); });
        startCarButton.onClick.AddListener(RunCar);
        UpdateText();
    }

    public void RunCar()
    {
        car.RunTurn();
        velocityChange = Vector2Int.zero;
    }

    public void ChangeX(int value)
    {
        if (Mathf.Abs(velocityChange.x + value) <= 1)
        {
            velocityChange.x += value;
            car.velocity += Vector2.right * value;
            UpdateText();
        }
    }

    public void ChangeY(int value)
    {
        if (Mathf.Abs(velocityChange.y + value) <= 1)
        {
            velocityChange.y += value;
            car.velocity += Vector2.up * value;
            UpdateText();
        }
    }

    void UpdateText()
    {
        textX.text = "X: " + car.velocity.x.ToString();
        textY.text = "Y: " + car.velocity.y.ToString();
    }
}
