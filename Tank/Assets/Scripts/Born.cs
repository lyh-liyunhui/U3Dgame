﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject plyerPrefab;
    public GameObject[] enemyPrefbList;

    public bool createPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank() {
        if (createPlayer)
        {
            Instantiate(plyerPrefab, transform.position, Quaternion.identity);
        }
        else {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefbList[num], transform.position, Quaternion.identity);
        }

        
    }
}
