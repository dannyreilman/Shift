using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShake : MonoBehaviour
{
    public float minPeriod;
    public float amplitude;
    public float shakeTime;

    float passedTime;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.OnComboChange += Shake;
    }

    void OnDestroy()
    {
        ScoreManager.instance.OnComboChange -= Shake;  
    }

    void Shake()
    {
        if(passedTime > minPeriod && ScoreManager.instance.combo == 0)
        {
            StartCoroutine(ShakeRoutine());

            passedTime = 0;
        }
    }

    IEnumerator ShakeRoutine()
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        transform.localPosition = amplitude * new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        
        yield return new WaitForSeconds(shakeTime);

        transform.localPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
    }
}
