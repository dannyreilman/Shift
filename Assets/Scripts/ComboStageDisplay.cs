using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboStageDisplay : MonoBehaviour
{
    public GameObject[] objects;
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.OnComboStageChange += OnComboChange;
    }
    
    void OnDestroy()
    {
        ScoreManager.instance.OnComboStageChange -= OnComboChange;
    }

    public void OnComboChange()
    {
        int i;

        for(i = 0; i < ScoreManager.instance.GetComboStage() && i < objects.Length; ++i)
        {
            objects[i].SetActive(true);
            objects[i].GetComponent<Animator>().SetInteger("Stage", ScoreManager.instance.GetComboStage());
        }

        for(; i < objects.Length; ++i)
        {
            objects[i].SetActive(false);
        }
    }
}
