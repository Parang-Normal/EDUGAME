using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    public CameraMovement Camera;
    public float NewMinY = 0.0f;
    public bool Triggered = false;

    private void Start()
    {
        if(Camera == null)
        {
            Camera = GameMode.Instance.Elements.MainCamera.GetComponent<CameraMovement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)// && !Triggered)
        {
            GameMode.Instance.CameraShiftList.Add(this);
            Triggered = true;
            StartCoroutine(Shift());
        }
    }

    IEnumerator Shift()
    {
        float elapsedTime = 0.0f;
        float waitTime = 0.5f;

        Vector2 newMin = new Vector2(0, 0);
        Vector2 cameraPos = Camera.transform.position;
        Vector2 MinShift = new Vector2(0, NewMinY);

        while (elapsedTime < waitTime)
        {
            newMin = Vector2.Lerp(cameraPos, MinShift, elapsedTime / waitTime);

            Camera.MinY = newMin.y;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}
