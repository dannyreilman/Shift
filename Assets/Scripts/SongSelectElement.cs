using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class SongSelectElement : MonoBehaviour
{
    public Text titleDisplay;
    public Animator animator;

    int savedIndex = 0;

    SongSelect parent;

    void Awake()
    {
        parent = GetComponentInParent<SongSelect>();
    }

    public void SetupElement(int index)
    {
        titleDisplay.text = MapLoader.instance.GetSongName(index);
        savedIndex = index;
    }

    public void Select()
    {
        animator.SetBool("Selected", true);
    }

    public void Deselect()
    {
        animator.SetBool("Selected", false);
    }

    public void OnClick()
    {
        parent.SelectSong(savedIndex);
    }
}
