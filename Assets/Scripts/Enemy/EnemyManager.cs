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
    Dictionary<EnemyType, int> type2intDic;
    BaseControl base_control;

    int current_level;
    Batch[] current_batches;
    float current_wait_time;
    float current_generate_gap_time;
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
        GameControl gameC = GameControl.GetInstance();
        ChangeLevel((int)gameC.gameLevel);
        base_control = BaseControl.GetInstance();
        rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        right = rightUp.x;
        left = leftDown.x;
        up = rightUp.y;
        down = leftDown.y;
        prefabDic = new Dictionary<int, Enemy>
        {
            {0,triangle },
            {1,circle},
            {2,square},
            {3,hexagon},
            {4,rhombus },
            {5,pentagon }, //swim
            {6,pentagon }, //rotate
            {7,star},
            {8,dot},
        };
        type2intDic = new Dictionary<EnemyType, int>
        {
            {EnemyType.Triangle,0},
            { EnemyType.Circle,1},
            { EnemyType.Square,2},
            { EnemyType.Hexagon,3},
            { EnemyType.Rhombus,4},
            { EnemyType.Pentagon,5},
            { EnemyType.Star,7},
            {EnemyType.Dot,8},
        };
        if ((int)gameC.gameLevel == 0)
        {
            StartCoroutine(WaitTillClick());
        }
        else
            StartCoroutine(WaitFor(current_wait_time));
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        if (ready)
        {
            ready = false;
            StartCoroutine(GenerateBatch(current_batches[batch_counter]));
            StartCoroutine(WaitFor(current_batches[batch_counter].exist_time));
            batch_counter++;
            if (batch_counter > batch_length)
                batch_counter = batch_length;
            if (batch_counter == batch_length && enemies.Count == 0)
            {
                LoadNextLevel();
            }
        }
    }

    IEnumerator WaitTillClick()
    {
        while (!ready)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
        ready = true;
    }
    IEnumerator GenerateBatch(Batch batch)
    {
        if (!batch.is_continuous)
        {
            float sec = 0.2f;
            GenerateEnemy(0, batch.triangle_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(1, batch.circle_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(2, batch.square_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(3, batch.hexagon_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(4, batch.rhombus_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(5, batch.swim_pentagon_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(6, batch.rotate_pentagon_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(7, batch.star_num);
            yield return new WaitForSeconds(sec);
            GenerateEnemy(8, batch.dot_num);
        }
        else
        {
            float gap_time = batch.exist_time/batch.sum();
            int triangle_num =batch.triangle_num;
            int circle_num =batch.circle_num;
            int square_num =batch.square_num;
            int hexagon_num=batch.hexagon_num;
            int rhombus_num=batch.rhombus_num;
            int swim_pentagon_num= batch.swim_pentagon_num;
            int rotate_pentagon_num=batch.rotate_pentagon_num ;
            int star_num=batch.star_num;
            int dot_num=batch.dot_num;

        }
    }

    void GenerateEnemy(int index, int num)
    {
        Vector3 position;
        for (int i = 0; i < num; i++)
        {
            if (index == 3 || index == 5) //hexagon and swim pentagon
                position = RandomPositionIn();
            else
                position = RandomPositionOut();
            Enemy newEnemy = Instantiate(prefabDic[index], position, Quaternion.identity);
            newEnemy.index = index;
            enemies.Add(newEnemy);
        }
    }

    void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            // enemy.ani.SetTrigger("Die");
            if (enemy.info.type == EnemyType.Rhombus)
            {
                SpeedUp(enemy.rb.position);
            }
            if (!enemy.is_hatched)
            {
                base_control.AddEnergy(enemy.energy);
                base_control.AddScore(enemy.score);
            }
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
                enemy.max_speed *= Constant.speed_mul;
            }
        }
    }
    public void Hatch(Vector2 pos, EnemyType type)
    {
        Enemy newEnemy = Instantiate(prefabDic[type2intDic[type]], new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Enemy>();
        newEnemy.is_hatched = true;
        enemies.Add(newEnemy);
    }
    public void HatchwithTarget(Vector2 pos, EnemyType type, Vector2 target)
    {
        Enemy newEnemy = Instantiate(prefabDic[type2intDic[type]], new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Enemy>();
        newEnemy.SetTarget(target);
        newEnemy.is_hatched=true;
        enemies.Add(newEnemy);
    }

    public void HatchwithCenterRotate(Vector2 pos, EnemyType type,Enemy enemy)
    {
        Enemy newEnemy = Instantiate(prefabDic[type2intDic[type]], new Vector3(pos.x, pos.y, 0), Quaternion.identity).GetComponent<Enemy>();
        newEnemy.SetCenterRotate(enemy);
        newEnemy.is_hatched = true;
        enemies.Add(newEnemy);
    }

    public Vector3 RandomPositionOut()
    {
        float x, y;
        int gap = 3;
        x = UnityEngine.Random.Range(left - gap, right + gap);
        if (x < left - 1 || x > right + 1)
            y = UnityEngine.Random.Range(down - gap, up + gap);
        else
        {
            if (UnityEngine.Random.value < 0.5f)
                y = UnityEngine.Random.Range(down - gap, down - 1);
            else
                y = UnityEngine.Random.Range(up + 1, up + gap);
        }
        return new Vector3(x, y, 0);
    }

    public Vector3 RandomPositionIn()
    {
        float x, y;
        float ratio = 2f / 3f;
        if (UnityEngine.Random.value < 0.5f)
            x = UnityEngine.Random.Range((left + 1), (left + 1) * ratio);
        else
            x = UnityEngine.Random.Range((right - 1) * ratio, (right - 1));
        if (UnityEngine.Random.value < 0.5f)
            y = UnityEngine.Random.Range((down + 1), (down + 1) * ratio);
        else
            y = UnityEngine.Random.Range((up - 1) * ratio, (up - 1));
        return new Vector3(x, y, 0);
    }
    public void ClickForReady()
    {
        ready = true;
    }
    private void ChangeLevel(int level)
    {
        current_level = level;
        current_batches = Constant.LevelDic[current_level];
        current_wait_time = Constant.waitDic[current_level];
        batch_length = current_batches.Length;
        batch_counter = 0;
    }
    private void LoadNextLevel()
    {
        GameControl.GetInstance().LoadNextLevel();
        Destroy(gameObject);
    }
}
