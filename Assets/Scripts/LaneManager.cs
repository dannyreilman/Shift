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
    }

    public float GetHeight(float progress)
    {
        return (scrollFactor * spawnHeight) + progress * (timingHeight - (scrollFactor * spawnHeight));    
    }
}