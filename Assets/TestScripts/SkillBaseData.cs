using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "New SkillBaseData", menuName = "CreatGameData/SkillBaseData")]

public class SkillBaseData : ScriptableObject
{
    public enum SkillType
    {
        water,
        // ���ݩ�,
        // ���ݩ�,
        // ���ݩ�,
        // �a�ݩ�,
    }



    [SerializeField]
    [Header("�ޯ൥��")]
    public int skilllevel = 0;

    [SerializeField]
    [Header("�ޯ��ݩ�")]
    public SkillType skilltype = SkillType.water;

    [SerializeField]
    [Header("�ȮɥΪ��ˮ`")]
    public int damage = 100;

    [SerializeField]
    [Header("�ȮɥΪ��v¡�q")]
    public int heal = 100;

    [SerializeField]
    [Header("��¦�ˮ`")]
    public int baseDamage = 100;

    [SerializeField]
    [Header("�]�k�ޯ୿�v")]
    public float magicMultiplier = 0.5f; // �]�k�ޯ୿�v

    [SerializeField]
    [Header("���z�ޯ୿�v")]
    public float physicalMultiplier = 0.5f; // ���z�ޯ୿�v

    [SerializeField]
    [Header("����sp�q")]
    public int skillsp = 20;

    [SerializeField]
    [Header("�ޯ୸��t��")]
    public float speed = 40f;


    [SerializeField]
    [Header("�ޯ୸�����ɶ�")]
    public float lifetime = 5f;

    [SerializeField]
    [Header("�O�_�z��")]
    public bool isCriticalAttack = true;



    [SerializeField]
    [Header("�I�������")]
    public bool isDestroyOnCollision = true;



    public bool freeze = true;
    public bool poision = true;

}