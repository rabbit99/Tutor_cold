using System;
using System.Drawing;
using UnityEngine;



public class SkillData : MonoBehaviour
{
    public SkillBaseData basedata;

    public int trueMagicdamage()
    {
        float result = 0;
        result = basedata.damage + (float)playermagicattack * basedata.magicMultiplier;
        return Convert.ToInt32(result); // ����float�覡�p��X�ˮ`�A�A�ഫ�ƭȦ�int��damage

    }
    public int playermagicattack = 0;



    void Start()
    {
        Destroy(gameObject, basedata.lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * basedata.speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) // �I����P����bool
    {
        if (basedata.isDestroyOnCollision)
        {
            Destroy(gameObject);
        }
    }
}


