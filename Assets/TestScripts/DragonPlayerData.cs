using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPlayerData : MonoBehaviour
{
    public CharacterBaseData basedata;
    public CharacterStatusData statusData;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBlue" || other.gameObject.tag == "PlayerRed" || other.gameObject.tag == "WildMonster") //���a�B�Ǫ�����I��������Areturn
        {
            return;
        }

        if (other.gameObject == this.gameObject)
        {
            return;
        }

        Debug.Log("other=" + other.name);
        SkillData skill = other.gameObject.GetComponent<SkillData>();

        //觸發技能改變屬性的功能
        if (skill.basedata.freeze)
        {
            //這個技能會把玩家冰凍
            basedata.playertype = CharacterBaseData.PlayerType.water;
        }

        //觸發技能改變屬性的功能
        if (skill.basedata.poision)
        {
            //這個技能會把玩家中毒
            statusData.Poision = true;
            //    statusData.Poisioned();
        }

        //計算傷害
        if (skill != null)
        {
            switch (skill.basedata.skilltype)
            {
                case SkillBaseData.SkillType.water:
                    //水屬性
                    float _value = basedata.playertype == CharacterBaseData.PlayerType.water ? 0.5f : 1;
                    basedata.currentHp -= other.gameObject.GetComponent<SkillData>().trueMagicdamage() * _value;
                    basedata.currentHp += other.gameObject.GetComponent<SkillData>().basedata.heal;
                    break;
            }

        }

    }
}


