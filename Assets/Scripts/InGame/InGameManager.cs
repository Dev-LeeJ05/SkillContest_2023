using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameManager : MonoBehaviour
{
    #region Variables
    private static InGameManager _instance = null;
    public static InGameManager Instance => _instance;

    [Header("UI")]
    public MainCanvas canvas;

    [Header("Bullet")]
    public BulletManager bulletManager;

    [Header("Enemy")]
    public EnemySpawn enemySpawn;

    [Header("Game")]
    public bool isGameStarted;
    public int currentScore;
    public float gameTimer;
    public Stage[] stages;
    public Stage stage;
    public int currentStage;
    public bool isWaving;
    public bool isCombo;
    public float maxComboDuration = 2.5f;
    public float curComboDelay;
    public int comboCount;
    public Transform ComboTrans;

    [Header("Skill")]
    public float maxHealDuration = 15f;
    public float maxBombDuration = 30f;
    public float curHealDelay;
    public float curBombDelay;
    public int healCount;
    public int bombCount;

    [Header("Enemy")]
    public BossBase curBoss;
    public Transform enemySpawnTrans;
    public Transform bulletSpawn;
    public Transform bossTarget;

    [Header("KeyCode")]
    public KeyCode keySkillHeal;
    public KeyCode keySkillBomb;

    [Header("Player")]
    public PlayerBase curPlayer;
    public Transform playerTarget;

    [Header("Prefab")]
    public GameObject getItemText;
    public GameObject comboText;
    public GameObject Bomb;
    public GameObject[] Meteors;
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InGameStartSettings();
    }

    private void Update()
    {
        if (currentScore > GameManager.Instance.bestScore)
        {
            canvas.scoreBox.setScore(currentScore, true);
        }
        else canvas.scoreBox.setScore(currentScore, false);

        canvas.healSkillBox.ActionSkill(curHealDelay, maxHealDuration, healCount);
        canvas.bombSkillBox.ActionSkill(curBombDelay, maxBombDuration, bombCount);

        if (!isGameStarted) return;
        else gameTimer += Time.deltaTime;

        if (canvas.bossBar.gameObject.activeSelf) canvas.bossBar.UpdateBossBar(curBoss.hp, curBoss.maxHp, curBoss.bossName);
        if (Input.GetKeyDown(KeyCode.Space)) GetItemText("+ 연료 증가");

        if (curHealDelay < maxHealDuration) curHealDelay += Time.deltaTime;
        if (curBombDelay < maxBombDuration) curBombDelay += Time.deltaTime;

        if (Input.GetKeyDown(keySkillHeal))
        {
            canvas.healSkillBox.Press.SetActive(true);
            SkillHeal();
        }
        if (Input.GetKeyUp(keySkillHeal)) canvas.healSkillBox.Press.SetActive(false);

        if (Input.GetKeyDown(keySkillBomb))
        {
            canvas.bombSkillBox.Press.SetActive(true);
            SkillBomb();
        }

        if (Input.GetKeyUp(keySkillBomb)) canvas.bombSkillBox.Press.SetActive(false);

        if (isCombo)
        {
            curComboDelay += Time.deltaTime;
            if (curComboDelay >= maxComboDuration)
                isCombo = false;
        }

        CheckWave();
    }

    public void InGameStartSettings()
    {
        GameIntro();
        InGamePlayerSetting();
        StageInit(1);
    }

    public void GameIntro()
    {
        // SetUI
        if (GameManager.Instance.ranks.Count > 0)
        {
            GameManager.Instance.SortRank();
            canvas.scoreBox.bestScore.text = GameManager.Instance.ranks[0].Score.ToString();
        }
        // CreatePlayer
        curPlayer = Instantiate(GameManager.Instance.selectedPlayer, new Vector3(0, -10, 0), Quaternion.Euler(0, 0, 0)).GetComponent<PlayerBase>();
        curPlayer.GetComponent<PlayerBase>().target = playerTarget;
        currentStage = 1;
    }

    public void InGamePlayerSetting()
    {
        currentScore += healCount * 300 + bombCount * 300;
        healCount = 3;
        bombCount = 3;
        curHealDelay = maxHealDuration;
        curBombDelay = maxBombDuration;
    }

    public void StageInit(int curStage)
    {
        Debug.Log($"curStage : {curStage}");
        foreach(Transform i in enemySpawnTrans)
        {
            Destroy(i.gameObject);
        }
        currentStage = curStage;
        canvas.info.SetStage(currentStage);
        switch (curStage)
        {
            case 1:
                stage = stages[0];
                break;
            case 2:
                stage = stages[1];
                break;
            case 3:
                stage = stages[2];
                break;
            default:
                break;
        }
        stage.currentWave = 0;
        stage.nextWave.Invoke();
        isWaving = false;
    }

    public void GameClear()
    {
        int score = 0;
        score += curPlayer.hp / curPlayer.maxHp * 1000;
        currentScore += score;
        isGameStarted = false;
        canvas.gameClear.Clear();
    }

    public void GetItemText(string value)
    {
        GameObject obj = Instantiate(getItemText, canvas.RightBox);
        obj.GetComponent<TextMeshProUGUI>().text = $"+ {value}";
        Destroy(obj, 1.5f);
    }

    public void RandomItem()
    {
        if (Random.Range(0,10) < 3) // 적 처치시 30% 확률로 아이템이 드랍 됨.
        {
            // 15% 무기 업그레이드 => "무기 강화"
            // 15% 무적 => "투명 모드"
            // 30% 체력 회복 => "내구도 수리"
            // 40% 연료 게이지 회복 => "주유"
            float probability = Random.value;
            if (probability < 0.4f) // 연료 게이지 회복 => "주유" 
            {
                curPlayer.fuel += (int)(curPlayer.maxFuel / 0.5);
                GetItemText("주유");
            }
            else if (probability < 0.7f) // 체력 회복 => "내구도 수리" - 30% 회복
            {
                curPlayer.hp += (int)(curPlayer.maxHp / 0.3);
                GetItemText("내구도 수리");
            }
            else if (probability < 0.85f) // 무적 => "투명 모드"
            {
                curPlayer.isgod = true;
                curPlayer.curGodDelay = 0f;
                GetItemText("투명 모드");
            }
            else // 무기 업그레이드 => "무기 강화"
            {
                curPlayer.WeaponLevelup();
                GetItemText("무기 강화");
            }
            currentScore += 200;
        }
    }

    public void EnemeyDie()
    {
        comboCount += 1;
        if (isCombo)
        {
            curComboDelay = 0f;
            GameObject ComboTXT = Instantiate(comboText, ComboTrans);
            ComboTXT.GetComponent<ComboText>().SetText(comboCount + 1, 100*currentStage + comboCount * 10);
            Destroy(ComboTXT, 0.7f);
        }
        else
        {
            isCombo = true;
            curComboDelay = 0f;
            comboCount = 0;
        }
        currentScore += 100*currentStage + comboCount*10;
        RandomItem();
    }

    public void BossDie()
    {
        // Score
        currentScore += 1500 * currentStage;
        
        //destroy
        Destroy(curBoss.gameObject);
        canvas.bossBar.ActiveBossBar(false);

        // if final boss => GameEnd, Rank register
        if (currentStage == 3)
        {
            GameClear();
            return;
        }

        // NextStage
        isGameStarted = false;
        isWaving = false;
        curPlayer.isIntro = true;
        StageInit(currentStage + 1);
        StartCoroutine(StageStart());
        // BossBar active fasle
    }

    public void PlayerDie(string reason)
    {
        isGameStarted = false;
        canvas.gameOver.EndGame(reason);
    }

    public IEnumerator StageStart()
    {
        Debug.Log(currentStage);
        yield return new WaitForSeconds(.6f);
        canvas.stageStart.StageClear();
        yield return new WaitForSeconds(2f);
        if (currentStage != 1)
        {
            InGamePlayerSetting();
            curPlayer.FuelManager();
            canvas.stageStart.InActive();
        }
        isWaving = true;
    }

    public void CheckWave()
    {
        if (isWaving && enemySpawnTrans.childCount <= 0 && !canvas.bossBar.gameObject.activeSelf)
        {
            isWaving = false;
            Debug.Log($"Stage : {currentStage}\n Phase : {stage.currentWave}");
            // Wave Clear
            stage.nextWave.Invoke();
        }
    }

    public void SkillHeal()
    {
        if (healCount <= 0)
        {
            // WarningMessage
            StartCoroutine(canvas.warningMessage.SetMessage("스킬 횟수 부족", .5f));
            return;
        }
        if (curHealDelay < maxHealDuration)
        {
            // WarningMessage
            StartCoroutine(canvas.warningMessage.SetMessage("스킬 준비 중", .5f));
            return;
        }
        healCount--;
        if (healCount <= 0) return;
        curHealDelay = 0f;
        curPlayer.HpRecover(curPlayer.maxHp/2);
        
    }

    public void SkillBomb()
    {
        if (bombCount <= 0)
        {
            // WarningMessage
            StartCoroutine(canvas.warningMessage.SetMessage("스킬 횟수 부족", .5f));
            return;
        }
        if (curBombDelay < maxBombDuration)
        {
            // WarningMessage
            StartCoroutine(canvas.warningMessage.SetMessage("스킬 준비 중", .5f));
            return;
        }
        bombCount--;
        if (bombCount <= 0) return;
        curBombDelay = 0f;
        GameObject particle = Instantiate(Bomb, Vector3.zero, Quaternion.identity);
        Destroy(particle, 1.5f);
        for(int i = enemySpawnTrans.childCount-1; i>=0; i--)
        {
            enemySpawnTrans.GetChild(i).TryGetComponent<EnemyBase>(out EnemyBase en);
            en.GetDamage(en.maxHp / 3 * 2);
        }
        foreach(Transform bullet in bulletSpawn)
        {
            Destroy(bullet.gameObject);
        }
    }

    public IEnumerator MeteorGenerator()
    {
        while (isGameStarted)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject meteor = Instantiate(Meteors[Random.Range(0,Meteors.Length)], new Vector2(-3.6f + i * 1.8f, 10f + Random.Range(-.5f,.5f)),Quaternion.Euler(0,0,Random.Range(0,360+1)));
                meteor.transform.parent = enemySpawnTrans;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
