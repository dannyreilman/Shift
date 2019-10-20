using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{
    public Transform elementsTransform;
    public Text title;
    public GameObject difficultySelectElementPrefab;

    DifficultySelectElement[] elements;
    
    int selectedElement = 0;

    public void ElementClicked(DifficultySelectElement element)
    {
        for(int i = 0; i < elements.Length; ++i)
        {
            if(elements[i] == element)
            {
                MapLoader.instance.LoadMap(selectedElement, i);
                MapLoader.instance.LoadGameplayScene();
                return;
            }
        }
    }

    public void SelectSong(int index)
    {
        gameObject.SetActive(index >= 0);
        if(index >= 0)
        {
            title.text = MapLoader.instance.GetSongName(index);
            selectedElement = index;
            MapLoader.DifficultyIndex[] difficulties = MapLoader.instance.GetDifficulties(index);

            while(elementsTransform.childCount < difficulties.Length)
            {
                GameObject.Instantiate(difficultySelectElementPrefab, Vector3.zero, Quaternion.identity, elementsTransform);
            }

            elements = new DifficultySelectElement[difficulties.Length];
            selectedElement = 0;
            int i = 0;
            for(i = 0; i < difficulties.Length; ++i)
            {
                elementsTransform.GetChild(i).gameObject.SetActive(true);
                elements[i] = elementsTransform.GetChild(i).GetComponent<DifficultySelectElement>();
                elements[i].Setup(difficulties[i]);
            }

            while(i < elementsTransform.childCount)
            {
                elementsTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
