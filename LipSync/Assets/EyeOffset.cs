using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOffset : MonoBehaviour
{
    const float offset = 0.0005f;
    private Vector3 originalPos;
    private Vector3 newPos;

    bool newPosing = false;
    bool oldPosing = true;

    void Start()
    {
        Random.InitState(3);

        originalPos = transform.position;  
        newPos.x = originalPos.x += Random.Range(-offset, offset);
        newPos.y = originalPos.y += Random.Range(-offset, offset);
        newPos.z = originalPos.z;
    }

    void Update()
    {
        if (newPosing)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, 180f * Time.deltaTime);
            if (transform.position == newPos)
            {
                newPos.x = originalPos.x += Random.Range(-offset, offset);
                newPos.y = originalPos.y += Random.Range(-offset, offset);
                newPosing = false;
                oldPosing = true;
            }
        }
        if (oldPosing)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos, 180f * Time.deltaTime);
            if (transform.position == originalPos)
            {
                newPosing = true;
                oldPosing = false;
            }
        }
    }
}
