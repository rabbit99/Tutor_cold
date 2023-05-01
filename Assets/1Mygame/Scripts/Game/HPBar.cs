using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public RectTransform healthBar;
    public Transform followTransform;

    private float _xOffset = 0;
    private float _yOffset = 0;
    private float _zOffset = 0;
    private RectTransform healthBarObj;
    // Start is called before the first frame update
    void Start()
    {
        healthBarObj = GetComponent<RectTransform>();
        _xOffset = healthBarObj.position.x;
        _yOffset = healthBarObj.position.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y + _yOffset, followTransform.position.z);
    }

    public void UpdateHp(int hp)
    {
        healthBar.sizeDelta = new Vector2(hp * 10, healthBar.sizeDelta.y);
    }
}
