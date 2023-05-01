using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "New CharacterBaseData", menuName = "CreatGameData/CharacterBaseData")]
public class CharacterBaseData : ScriptableObject
{
    public new string name;

    [Header("�򥻯���")]
    [Header("================================")]
    [SerializeField]
    [Header("�}�ⵥ��")]
    public int level = 1;




    public enum PlayerType
    {
        water,
        // ���ݩ�,
        // ���ݩ�,
        // ���ݩ�,
        // �a�ݩ�,
    }
    [SerializeField]
    [Header("�}���ݩ�")]
    public PlayerType playertype = PlayerType.water;

    // public enum PlayerRace
    // {
    //     �s��,
    //     A��,
    //     B��,
    //     C��,
    //     D��,
    // }
    // [SerializeField]
    // [Header("�}��ر�")]
    // public PlayerRace playerrace = PlayerRace.�s��;

    // public enum PlayerSize
    // {
    //     �j�髬,
    //     ���髬,
    //     �p�髬,

    // }
    // [SerializeField]
    // [Header("�}���髬")]
    // public PlayerSize playersize = PlayerSize.�j�髬;

    // public enum PlayerKind
    // {
    //     ���a,
    //     Boss,
    //     ����,

    // }
    // [SerializeField]
    // [Header("�}�����")]
    // public PlayerKind playerkind = PlayerKind.���a;

    [SerializeField]
    [Header("�}�Ⲿ�ʳt��")]
    public float movespeed = 35f;

    [SerializeField]
    [Header("�̤j��q")]
    public int maxHp = 500;
    [SerializeField]
    [Header("�ثe��q")]
    public float currentHp = 500f;
    [SerializeField]
    [Header("�C����q��_")]
    public float autoHprecover = 50f;

    [SerializeField]
    [Header("�̤j�]�q")]
    public int maxSP;
    [SerializeField]
    [Header("�ثe�]�q")]
    public int currentSP;
    [SerializeField]
    [Header("�C���]�q��_")]
    public float autoSprecover = 1;

    [SerializeField]
    [Header("�ޯੵ��")]
    public int Skillcooldown = 1;




    [Space(50)]
    [Header("��������")]
    [Header("================================")]

    [SerializeField]
    [Header("���z�����O")]
    public int attack;
    [SerializeField]
    [Header("�]�k�����O")]
    public int magicattack;

    [SerializeField]
    [Header("�����t��")]
    public int aspd;

    [SerializeField]
    [Header("�z���v")]
    public int critical;

    [SerializeField]
    [Header("�z���ˮ`")]
    public int criticaldamage;


    [Space(50)]
    [Header("���z�����[��")]
    [Header("================================")]


    [SerializeField]
    [Header("���z�����O�W�[%")]
    public int atk_bonus;

    [SerializeField]
    [Header("�s�ڪ��z�W��")]
    public int dragon_atkbonus;
    [SerializeField]
    [Header("�ر�2���z�W��")]
    public int race_atkbonus;
    [SerializeField]
    [Header("�ر�3���z�W��")]
    public int race3_atkbonus;

    [SerializeField]
    [Header("�p�髬���z�W��")]
    public int littlesize_atkbonus;
    [SerializeField]
    [Header("���髬���z�W��")]
    public int middlesize_atkbonus;
    [SerializeField]
    [Header("�j�髬���z�W��")]
    public int largesize_atkbonus;


    [Space(50)]
    [Header("�]�k�����[��")]
    [Header("================================")]

    [SerializeField]
    [Header("�]�k�����O�W�[%")]
    public int matk_bonus;


    [SerializeField]
    [Header("���ݩ��]�k�W��%")]
    public int watermatk_bonus;
    [SerializeField]
    [Header("���ݩ��]�k�W��%")]
    public int windmatk_bonus;
    [SerializeField]
    [Header("�a�ݩ��]�k�W��%")]
    public int earthmatk_bonus;
    [SerializeField]
    [Header("���ݩ��]�k�W��%")]
    public int firematk_bonus;


    [SerializeField]
    [Header("�s���]�k�W��")]
    public int dragon_matkbonus;
    [SerializeField]
    [Header("�ر�2�]�k�W��")]
    public int race_matkbonus;
    [SerializeField]
    [Header("�ر�3�]�k�W��")]
    public int race3_matkbonus;

    [SerializeField]
    [Header("�p�髬�]�k�W��")]
    public int littlesize_matkbonus;
    [SerializeField]
    [Header("���髬�]�k�W��")]
    public int middlesize_matkbonus;
    [SerializeField]
    [Header("�j�髬�]�k�W��")]
    public int largesize_matkbonus;


    [Space(50)]
    [Header("���m����")]
    [Header("================================")]
    [SerializeField]
    [Header("���z���m�v")]
    public int defense;
    [SerializeField]
    [Header("�]�k���m�v")]
    public int magicdefense;

    [SerializeField]
    [Header("�s���]�k���")]
    public float dragon_matkreduce;
    [SerializeField]
    [Header("�ر�2�]�k���")]
    public float race_matkreduce;
    [SerializeField]
    [Header("�ر�3�]�k���")]
    public float race3_matkreduce;

    [SerializeField]
    [Header("�s�ڪ��z���")]
    public float dragon_atkreduce;
    [SerializeField]
    [Header("�ر�1���z���")]
    public float race1_atkreduce;
    [SerializeField]
    [Header("�ر�1���z���")]
    public float race2_atkreduce;

    [SerializeField]
    [Header("�p�髬���z���")]
    public float littlesize_atkreduce;
    [SerializeField]
    [Header("���髬���z���")]
    public float middlesize_atkreduce;
    [SerializeField]
    [Header("�j�髬���z���")]
    public float largesize_atkreduce;

    [SerializeField]
    [Header("�p�髬�]�k���")]
    public float littlesize_matkreduce;
    [SerializeField]
    [Header("���髬�]�k���")]
    public float middlesize_matkreduce;
    [SerializeField]
    [Header("�j�髬�]�k���")]
    public float largesize_matkreduce;

    [SerializeField]
    [Header("���z�ܩ�")]
    public float resist;
    [SerializeField]
    [Header("�]�k�ܩ�")]
    public float magicresist;


    [Space(50)]
    [Header("�W�q���A")]
    [Header("================================")]

    [SerializeField]
    [Header("�]�k�ˮ`�W�[")]
    public bool aaaa = true;
    [SerializeField]
    [Header("���z�ˮ`�W�[")]
    public bool BBBB = true;
    [SerializeField]
    [Header("���z���m�W�[")]
    public bool cccc = true;

    [Space(50)]
    [Header("��q���A")]
    [Header("================================")]
    [SerializeField]
    [Header("�]�k�ˮ`���C")]
    public bool DDD = true;
    [SerializeField]
    [Header("���z�ˮ`���C")]
    public bool EEEE = true;
    [SerializeField]
    [Header("���z���m���C")]
    public bool FFFF = true;

    [Space(50)]
    [Header("���`���A")]
    [Header("================================")]
    [SerializeField]
    [Header("�A�G")]
    public bool GGG = true;
    [SerializeField]
    [Header("�B��")]
    public bool HHH = true;
    [SerializeField]
    [Header("�ۤ�")]
    public bool DED = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentHp += autoHprecover * Time.deltaTime;
    }
}
