using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SongSelect))]
public class SongSelectDrag : SongSelectController
{
    const float DEADZONE = 0.0001f;
    public float speed;
    SongSelect songSelect;
    void Awake()
    {
        songSelect = GetComponent<SongSelect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwipeManager.instance.OnSwipe += OnSwipe;
    }

    void OnDestroy()
    {
        SwipeManager.instance.OnSwipe -= OnSwipe;
    }

    public void OnSwipe(Vector2 direction)
    {
        if(Mathf.Abs(direction.y) > DEADZONE)
            songSelect.Move(direction.y * speed * Time.deltaTime);
    }
}
