using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IEnemy
{
    public EnemyInfo info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Step2Center()  //�������н���λ�õȸ���
    {

    }
    public void TakeDamage(int damage)
    {
            info.hp -= damage;  //�����manager����У�
        //��Ҫ���ϰ׹���˸Ч��
    }
    
}
