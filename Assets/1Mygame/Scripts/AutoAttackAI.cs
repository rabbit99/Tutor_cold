using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackAI : MonoBehaviour
{
    [SerializeField] private GameObject bullet1;
    [SerializeField] private Transform gun1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("autoAtk", 0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void autoAtk()
    {
        Instantiate(bullet1, gun1.position, gun1.rotation);
    }
}
