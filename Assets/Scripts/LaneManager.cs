using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
	public static LaneManager instance = null;
	void Awake()
	{
		if(instance == null || instance.Equals(null))
		{
			instance = this;
            lanePositions = new float[transform.childCount];
            for(int i = 0; i < transform.childCount; ++i)
            {
                lanePositions[i] = transform.GetChild(i).GetComponent<RectTransform>().position.x;
            }
		}
		else
		{
			Destroy(this);
		}
	}

    public float[] lanePositions;

    public RectTransform timingLine;
    [HideInInspector()]
	private float timingHeight;
	public RectTransform spawnLine;
    [HideInInspector()]
    public float spawnHeight;
    public float scrollFactor = 1.0f;

    void Start()
    {
        timingHeight = timingLine.position.y;
        spawnHeight = spawnLine.position.y;
        KeybindManager.acceptAlways[KeybindManager.InputAction.decreaseSpeed] += DecreaseSpeed;
        KeybindManager.acceptAlways[KeybindManager.InputAction.increaseSpeed] += IncreaseSpeed;
    }

    void OnDestroy()
    {
        KeybindManager.acceptAlways[KeybindManager.InputAction.decreaseSpeed] -= DecreaseSpeed;
        KeybindManager.acceptAlways[KeybindManager.InputAction.increaseSpeed] -= IncreaseSpeed;
    }

    public float GetHeight(float progress)
    {
        return (scrollFactor * spawnHeight) + progress * (timingHeight - (scrollFactor * spawnHeight));    
    }

    public void IncreaseSpeed()
    {
        scrollFactor += 1;
    }

    public void DecreaseSpeed()
    {
        scrollFactor -= 1;
        if(scrollFactor < 1)
            scrollFactor = 1;
    }
}