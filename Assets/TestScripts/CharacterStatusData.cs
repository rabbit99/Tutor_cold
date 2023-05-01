using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusData : MonoBehaviour
{
    public bool freeze = false;
    public bool Poision
    {
        get { return _poision; }
        set
        {
            _poision = value;
        }
    }
    private bool _poision = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Poisioned(float keepingTime)
    {
        StartCoroutine(KeepPoisionedStatus(keepingTime));
    }

    IEnumerator KeepPoisionedStatus(float keepingTime)
    {
        float timer = 0;
        while (timer < keepingTime)
        {
            yield return new WaitForSeconds(1f);
            // health -= 10; // 扣血
            timer += 1f; // 更新計時器
        }
        // poision = false;
    }
}
