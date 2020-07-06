using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MapCreation : MonoBehaviour
{
    //实例化预制体
    public GameObject[] item;

    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }

    private void InitMap() {
        //实例化基地
        CreateItem(item[0], new Vector3(0, -8, -0), Quaternion.identity);

        //实例化围墙
        CreateItem(item[1], new Vector3(-1, -8, -0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, -0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, -0), Quaternion.identity);
        }
        //实例化外墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //产生敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(-0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("createEnemy", 4, 5);

        //实例化地图
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], createRandomPosition(), Quaternion.identity);

        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], createRandomPosition(), Quaternion.identity);
            CreateItem(item[4], createRandomPosition(), Quaternion.identity);
            CreateItem(item[5], createRandomPosition(), Quaternion.identity);

        }

        /* for (int i = 0; i < 20; i++)
         {
             CreateItem(item[4], createRandomPosition(), Quaternion.identity);

         }
         for (int i = 0; i < 20; i++)
         {
             CreateItem(item[5], createRandomPosition(), Quaternion.identity);

         }*/


    }
    private void CreateItem(GameObject createCameObject, Vector3 createPosition, Quaternion q) 
    {

        GameObject itemGo = Instantiate(createCameObject, createPosition, q);

        itemGo.transform.SetParent(gameObject.transform);

        itemPositionList.Add(createPosition);
    }


    /*产生随机位置的方法*/
    private Vector3 createRandomPosition() 
    {

        while (true) {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8),0);
            if (!HasThePosition(createPosition)) {
                return createPosition;
            }
        }

    }

    //用来判断位置是否存在
    private bool HasThePosition(Vector3 vector3) {
        for (int i = 0; i < itemPositionList.Count; i++) {

            if (vector3==itemPositionList[i]) {
                return true;
            }

        }
        return false;
    }
    //产生敌人的方法
    private void createEnemy() {

        int num = Random.Range(0,3);
        Vector3 v = new Vector3();
        if (num == 0) {
            v = new Vector3(-10, 8, 0);
        } else if (num==1){
            v = new Vector3(0, 8, 0);
        } else{
            v = new Vector3(-10, 8, 0);
        }
        CreateItem(item[3], v, Quaternion.identity);
    }
}
