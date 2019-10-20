using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerFilling : MonoBehaviour
{
    public int maxStars;
    public GameObject starPrefab;

    public Color easyColor;
    public Color hardColor;

    Image[] images;

    float fillAmountInternal;
    public float fillAmount
    {
        get
        {
            return fillAmountInternal;
        }

        set
        {
            fillAmountInternal = value;
            UpdateFill();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        images = new Image[maxStars];
        while(transform.childCount < maxStars)
        {
            GameObject.Instantiate(starPrefab, Vector3.zero, Quaternion.identity, transform);
        }

        for(int i = 0; i < maxStars; ++i)
        {
            images[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    void UpdateFill()
    {
        float actualFill = Mathf.Min(fillAmountInternal, maxStars);
        int roundedDown = Mathf.FloorToInt(actualFill);

        Color interpColor = Color.Lerp(easyColor, hardColor, actualFill/maxStars);

        int i = 0;
        for(i = 0; i < roundedDown; ++i)
        {
            images[i].fillAmount = 1;
            images[i].color = interpColor;
        }

        if(i < maxStars)
        {
            images[i].fillAmount =  actualFill - roundedDown;
            images[i].color = interpColor;

            ++i;
            while(i < maxStars)
            {
                images[i].fillAmount = 0;
                ++i;
            }
        }
    }
}
