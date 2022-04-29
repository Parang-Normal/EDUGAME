using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject Camera;
    public float parallaxEffect;
    public bool MoveY = false;

    private float length, startPos, fixedY, cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        fixedY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void Update()
    {
        float temp = (Camera.transform.position.x * (1 - parallaxEffect));
        float dist = ((Camera.transform.position.x - cameraPos) * parallaxEffect);

        if(MoveY)
            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(startPos + dist, fixedY, transform.position.z);

        /*
        if (temp > startPos + length) 
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
        */
        
    }

    public void ActivateParallax(float StartingX)
    {
        this.enabled = true;
        cameraPos = StartingX;
    }
}
