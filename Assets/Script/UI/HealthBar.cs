using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Sprite Health1 = null;
    [SerializeField] Sprite Health2 = null;
    [SerializeField] Sprite Health3 = null;

    Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void UpdateHealth(int value)
    {
        if (value == 1)
            image.sprite = Health1;
        else if (value == 2)
            image.sprite = Health2;
        else if (value == 3)
            image.sprite = Health3;
    }
}
