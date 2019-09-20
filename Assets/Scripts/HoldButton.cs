﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    public int lane;
    public Image upImage;
    public Image normalImage;
    public Image downImage;

    // Update is called once per frame
    void Update()
    {
        upImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHit(lane, NoteType.UpHit));
        normalImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHit(lane, NoteType.Hit));
        downImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHit(lane, NoteType.DownHit));
    }
}
