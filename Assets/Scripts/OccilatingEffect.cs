using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccilatingEffect : MonoBehaviour
{
    Vector3 originalPos;
    float timer = 0;
    public float speed;
    public float scale; 

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        Debug.Log(originalPos);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = originalPos.y + oscillate();
        transform.position = pos;

 
      
    }

 
    float oscillate()
    {
        return Mathf.Sin(timer * speed / Mathf.PI) * scale;
    }
}
