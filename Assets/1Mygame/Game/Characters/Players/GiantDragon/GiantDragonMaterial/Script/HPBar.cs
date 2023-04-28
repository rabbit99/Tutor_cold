using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public RectTransform healthBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void UpdateHp(int hp)
    {
        healthBar.sizeDelta = new Vector2(hp*10, healthBar.sizeDelta.y);
    }
}
