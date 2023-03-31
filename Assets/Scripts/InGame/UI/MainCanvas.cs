using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public HPContainer hpContainer;
    public FuelContainer fuelContainer;

    public ScoreBox scoreBox;
    public SkillBox healSkillBox;
    public SkillBox bombSkillBox;
    public GameOver gameOver;
    public GameClear gameClear;
    public StageStart stageStart;
    public RankRegister rankRegister;
    public BossBar bossBar;
    public WarningMessage warningMessage;
    public Info info;

    public Transform RightBox;
}
