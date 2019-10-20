using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour
{
    const float SMALLEST_MOVE = 0.0001f;
    public int poolSize;
    public float rowHeight;
    public GameObject prefab;

    public float correctDelay = 0.1f;
    public float correctSpeed = 1.0f;

    public DifficultySelect difficultySelect;

    public System.Action<int, bool> ActiveSongChanged;

    public string splashScene;

    SongSelectElement[] pool;

    List<SongSelectController> controllers;
    int selectedSongInternal = -1;
    int selectedSong
    {
        get
        {
            return selectedSongInternal;
        }

        set
        {
            if(selectedSongInternal >= 0 && value < 0)
            {
                foreach(SongSelectController controller in controllers)
                {
                    controller.enabled = true;
                }
            }
            else if(selectedSongInternal < 0 && value >= 0)
            {
                foreach(SongSelectController controller in controllers)
                {
                    controller.enabled = false;
                }
            }
            selectedSongInternal = value;
        }
    }

    int index = 0;

    public int GetIndex()
    {
        return index;
    }

    float offset = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        while(transform.childCount < poolSize)
        {
            GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        }

        pool = new SongSelectElement[poolSize];
        for(int i = 0; i < poolSize; ++i)
        {
            pool[i] = transform.GetChild(i).GetComponentInChildren<SongSelectElement>();
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(0, rowHeight * poolSize);
        controllers = new List<SongSelectController>(GetComponents<SongSelectController>());

    }

    void SelectCurrentSong()
    {
        SelectSong(GetActiveIndex());
    }

    void OnMenuBack()
    {
        if(selectedSong < 0)
            SceneManager.LoadScene(splashScene);
        else
            selectedSong = -1;
    }

    void Start()
    {
        UpdateElements();
        pool[GetActiveIndex()].Select();

        KeybindManager.acceptAlways[KeybindManager.InputAction.menuEnter] += SelectCurrentSong;
        KeybindManager.acceptAlways[KeybindManager.InputAction.menuBack] += OnMenuBack;
    }

    void OnDestroy()
    {
        KeybindManager.acceptAlways[KeybindManager.InputAction.menuEnter] -= SelectCurrentSong;
        KeybindManager.acceptAlways[KeybindManager.InputAction.menuBack] -= OnMenuBack;
    }

    float timeSinceMove = 0.0f;
    void Update()
    {
        timeSinceMove += Time.deltaTime;

        if(timeSinceMove > correctDelay && offset != 0)
        {
            //Debug.Log("Correcting");
            if(offset > 0.0f)
            {
                offset = Mathf.Max(offset - correctSpeed * Time.deltaTime, 0.0f);
            }
            else if(offset < 0.0f)
            {
                offset = Mathf.Min(offset + correctSpeed * Time.deltaTime, 0.0f);
            }
            UpdateElements();
        }
    }

    int GetRealIndex(int elementNum)
    {
        return (elementNum + index) % MapLoader.instance.GetMapCount();
    }

    int GetActiveIndex()
    {
        return poolSize / 2;
    }

    void UpdateElements()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, offset * rowHeight);
        for(int i = 0; i < poolSize; ++i)
        {
            pool[i].SetupElement(GetRealIndex(i));
        }
    }

    public void SelectSong(int realIndex)
    {
        selectedSong = realIndex;
        difficultySelect.SelectSong(realIndex);
    }

    public void Move(float amount)
    {
        timeSinceMove = 0.0f;
        offset += amount;
        while(offset > 0.5f)
        {
            index += 1;
            offset -= 1.0f;
            index = index % poolSize;

            if(ActiveSongChanged != null)
                ActiveSongChanged(GetRealIndex(index), true);
        }

        while(offset < -0.5f)
        {
            index -= 1;
            offset += 1.0f;
            while(index < 0)
            {
                index += poolSize;
            }

            if(ActiveSongChanged != null)
                ActiveSongChanged(GetRealIndex(index), false);

        }

        UpdateElements();
    }
}
