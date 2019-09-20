using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    public int lane;
    public Image upImage;
    public Image normalImage;
    public Image downImage;

    void Start()
    {
        PauseManager.PausableUpdate += PausableUpdate;
    }

    void OnDestroy()
    {
        PauseManager.PausableUpdate -= PausableUpdate;
    }

    // Update is called once per frame
    void PausableUpdate()
    {
        upImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHitAction(lane, NoteType.UpHit));
        normalImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHitAction(lane, NoteType.Hit));
        downImage.enabled = KeybindManager.GetDown(KeybindManager.GetRowHitAction(lane, NoteType.DownHit));
    }
}
