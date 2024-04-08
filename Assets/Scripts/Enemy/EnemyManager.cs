using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemyManager : SingletonMono<EnemyManager>, IEnemyManager
{
    List<Enemy> enemies = new List<Enemy>();
    public Triangle triangle;
    public Circle circle;
    public Dot dot;
    public Square square;
    public Rhombus rhombus;
    public Star star;
    public Pentagon pentagon;
    public Hexagon hexagon;
    Dictionary<int, Enemy> prefabDic;
    BaseControl base_control;

    int current_level;
    Batch[] current_batches;
    int batch_length;
    int batch_counter;
    bool ready;

    Vector3 rightUp;
    Vector3 leftDown;
    float right;
    float left;
    float up;
    float down;
    void Start()
    {
        ready = false;
        base_control = BaseControl.GetInstance();
        rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        right = rightUp.x + 5;
        left = leftDown.x - 5;
        up = rightUp.y + 5;
        down = leftDown.y - 5;
        ChangeLevel(1);
        prefabDic = new Dictionary<int, Enemy>
        {
            {0,triangle },
            {1,dot},
            {2,square},
            {3,circle},
            {4,rhombus },
            {5,pentagon },
            {6,hexagon},
            {7,star},
        };
        StartCoroutine(GenerateAlong());
        StartCoroutine(WaitTillReady(10f));
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        // Debug.Log(batch_counter);
        if (ready)
        {
            ready = false;
            StartCoroutine(GenerateBatch(current_batches[batch_counter]));
            batch_counter++;
            if (batch_counter < batch_length)
            {
                StartCoroutine(WaitTillReady(current_batches[batch_counter - 1].gap_time));
            }
        }
    }

    IEnumerator WaitTillReady(float time)
    {
        yield return new WaitForSeconds(time);
        ready = true;
    }
    IEnumerator GenerateAlong()
    {
        int num_line = 0;
        int count = 0;
        while (true)
        {
            count++;
            yield return new WaitForSeconds(Constant.generate_gap_time);
            for (int i = 0; i < num_line + count % 4; i++)
            {
                int randomValue = UnityEngine.Random.Range(0, 4);
                GenerateEnemy(randomValue, 1);
                yield return new WaitForSeconds(0.2f);
            }
            if (count % 10 == 0)
                num_line++;
        }
    }
    IEnumerator GenerateBatch(Batch batch)
    {
        GenerateEnemy(0, batch.triangle_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(1, batch.dot_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(2, batch.square_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(3, batch.circle_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(4, batch.rhombus_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(5, batch.pentagon_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(6, batch.hexagon_num);
        yield return new WaitForSeconds(0.5f);
        GenerateEnemy(7, batch.star_num);
    }

    void GenerateEnemy(int index, int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 position = RandomPosition();
            Enemy newEnemy = Instantiate(prefabDic[index], position, Quaternion.identity);
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
            enemy.ani.SetTrigger("Die");
            enemies.Remove(enemy);

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
            Vector2 target = new Vector2(UnityEngine.Random.Range(pos.x - 2f, pos.x + 2f), UnityEngine.Random.Range(pos.y - 2f, pos.y + 2f));
            newEnemy.SetTarget(target);
            enemies.Add(newEnemy);
        }
        else if (type == EnemyType.Rhombus)
        {
            Rhombus newEnemy = Instantiate(rhombus, new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Rhombus>();
            Vector2 target = new Vector2(UnityEngine.Random.Range(pos.x - 2f, pos.x + 2f), UnityEngine.Random.Range(pos.y - 2f, pos.y + 2f));
            newEnemy.SetTarget(target);
            enemies.Add(newEnemy);
        }
    }
    Vector3 RandomPosition()
    {
        float x, y;
        x = UnityEngine.Random.Range(left - 5, right + 5);
        if (x < left || x > right)
            y = UnityEngine.Random.Range(down - 5, up + 5);
        else
        {
            if (UnityEngine.Random.value < 0.5f)
                y = UnityEngine.Random.Range(down - 5, down);
            else
                y = UnityEngine.Random.Range(up, up + 5);
        }
        return new Vector3(x, y, 0);
    }
    public void ChangeLevel(int level)
    {
        current_level = level;
        current_batches = Constant.LevelDic[current_level];
        batch_length = current_batches.Length;
        batch_counter = 0;
    }
}
