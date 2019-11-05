using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTint : MonoBehaviour
{
    public Image toTint;
    [Range(0,1)]
    public float maxTint;
    [Range(0,1)]
    public float minTint;
    // Update is called once per frame
    void Update()
    {
        toTint.color = new Color(toTint.color.r, 
                                 toTint.color.g,
                                 toTint.color.b,
                                 ConvertToAlpha(1 - (DeathHandler.instance.health / DeathHandler.instance.maxHealth)));
    }

    float ConvertToAlpha(float percent)
    {
        if(percent == 0 || PauseManager.currentState != PauseManager.State.Gameplay)
            return 0;
        else
            return minTint + (maxTint - minTint) * percent;
    }
}
