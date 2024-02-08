using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMono<EnemyManager>,IEnemyManager
{
    // Start is called before the first frame update
    List<EnemyBase> enemies = new List<EnemyBase>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        GenerateEnemies();
        
    }
    public void CheckHp()  //����Hp��յĵ���
    {

    }
    public void GenerateEnemies() //�����µĵ��ˣ�������Time.time��Ŀ������random��ʼ��
    {

    }
    public List<EnemyInfo> GetEnemyList()  //����info
    {
        List<EnemyInfo> enemyInfos = new List<EnemyInfo>();
        foreach(EnemyBase enemy in enemies)
            enemyInfos.Add(enemy.info);
        return enemyInfos;
    }//��ȡ���������ɵĵ�����Ϣ
}
