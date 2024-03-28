using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemyManager : SingletonMono<EnemyManager>, IEnemyManager
{
    public Triangle triangle;
    public Circle circle;
    public Dot dot;
    public Square square;
    public Rhombus rhombus;
    public Star star;
    public Pentagon pentagon;
    public Hexagon hexagon;

    private BaseControl base_control;
    List<Enemy> enemies = new List<Enemy>();
    Vector3 rightUp;
    Vector3 leftDown;
    float right;
    float left;
    float up;
    float down;
    int generate_cnt = 0;
    void Start()
    {
        base_control = BaseControl.GetInstance();
        rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        right = rightUp.x+5;
        left = leftDown.x-5;
        up = rightUp.y+5;
        down = leftDown.y-5;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        if (generate_cnt-- == 0)
        {
            GenerateEnemy();
            generate_cnt = 1000;
        }

    }
    void GenerateEnemy()
    {
        int generateNum = (int)(Time.time % 10) + 1;
        float x, y;
        for (int i = 0; i < generateNum; i++)
        {
            x = Random.Range(left - 5, right + 5);
            if (x < left || x > right)
                y = Random.Range(down - 5, up + 5);
            else
            {
                if (Random.value < 0.5f)
                    y = Random.Range(down - 5, down);
                else
                    y = Random.Range(up, up + 5);
            }
            // should be random
            int randomValue = Random.Range(0, 8);
            Enemy newEnemy;
            switch (randomValue)
            {
                case 0:
                    newEnemy = Instantiate(triangle, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 1:
                    newEnemy = Instantiate(dot, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 2:
                    newEnemy = Instantiate(square, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 3:
                    newEnemy = Instantiate(circle, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 4:
                    newEnemy = Instantiate(rhombus, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 5:
                    newEnemy = Instantiate(pentagon, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 6:
                    newEnemy = Instantiate(hexagon, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                case 7:
                    newEnemy = Instantiate(star, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                default:
                    newEnemy = null;
                    break;
            }
            enemies.Add(newEnemy);
        }
    }
    void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            // Debug.Log("Enemy Dies");
            if (enemy.info.type == EnemyType.Circle)
            {
                Hatch(enemy.rb.position, EnemyType.Dot);
            }
            if (enemy.info.type == EnemyType.Rhombus)
            {
                SpeedUp(enemy.rb.position);
            }
            base_control.AddEnergy(enemy.energy);
            base_control.AddScore(enemy.score);
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);

        }
    }
    void CheckHp()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemies[i];
            if (enemy.info.hp <= 0)
            {
                RemoveEnemy(enemy);
            }
            else if (enemy.info.type == EnemyType.Hexagon && enemy.info.hp <= Constant.HpDic[EnemyType.Hexagon] / 2 && enemy.givenBirth == false)
            {
                enemy.givenBirth = true;
                Hatch(enemy.rb.position, EnemyType.Rhombus);
                Hatch(enemy.rb.position, EnemyType.Rhombus);
            }
        }
    }
    public List<EnemyInfo> GetEnemyList()
    {
        List<EnemyInfo> enemyInfos = new List<EnemyInfo>();
        foreach (Enemy enemy in enemies)
        {
            enemy.UpdateInfo();
            enemyInfos.Add(enemy.info);
        }
        return enemyInfos;
    }
    public void SpeedUp(Vector2 pos)
    {
        foreach (Enemy enemy in enemies)
        {
            if ((enemy.rb.position - pos).magnitude < Constant.speed_range)
            {
                enemy.rb.velocity *= Constant.speed_mul;
            }
        }
    }
    public void Hatch(Vector2 pos, EnemyType type)
    {
        if (type == EnemyType.Dot)
        {
            Dot newEnemy = Instantiate(dot, new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Dot>();
            Vector2 target = new Vector2(Random.Range(pos.x - 2f, pos.x + 2f), Random.Range(pos.y - 2f, pos.y + 2f));
            newEnemy.SetTarget(target);
            enemies.Add(newEnemy);
        }
        else if (type == EnemyType.Rhombus)
        {
            Rhombus newEnemy = Instantiate(rhombus, new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Rhombus>();
            Vector2 target = new Vector2(Random.Range(pos.x - 2f, pos.x + 2f), Random.Range(pos.y - 2f, pos.y + 2f));
            newEnemy.SetTarget(target);
            enemies.Add(newEnemy);
        }
    }
}
