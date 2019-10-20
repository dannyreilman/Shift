using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SongSelect))]
public class SongSelectScroll : SongSelectController
{
    const float DEADZONE = 0.0001f;
    public float speed;
    SongSelect songSelect;
    void Awake()
    {
        songSelect = GetComponent<SongSelect>();
    }

    // Start is called before the first frame update
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(scroll) > DEADZONE)
            songSelect.Move(-1 * speed * scroll * Time.deltaTime);
    }
}
