using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectElement : MonoBehaviour
{
    DifficultySelect parent;
    public Text title;
    public LowerFilling stars;

    void Awake()
    {
        parent = GetComponentInParent<DifficultySelect>();
    }

    public void Setup(MapLoader.DifficultyIndex difficulty)
    {
        title.text = difficulty.displayedName;
        stars.fillAmount = difficulty.stars;
    }

    public void OnClick()
    {
        parent.ElementClicked(this);
    }
}
