using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy‚Ìƒf[ƒ^‚ğŠi”[‚·‚éScriptableObject
/// </summary>
[CreateAssetMenu(fileName ="EnemyData", menuName ="EnemyData")]
public class EnemyData : ScriptableObject
{
    public Sprite EnemySprite => _enemySprite; 
    public int Level => _level;
    public string Name => _name;
    public int Attack => _attack;
    public int Defense => _defense;
    public int Speed => _speed;
    public int HP => _hP;
    public int MP => _mP;
    public int ExP => _exP;
    public int Gold => _gold;
    public SkillData[] Skills => _skills;

    [SerializeField]
    [Header("“G‚ÌŠG")]
    Sprite _enemySprite;

    [SerializeField]
    [Header("ƒŒƒxƒ‹")]
    int _level;

    [SerializeField]
    [Header("–¼‘O")]
    string _name;

    [SerializeField]
    [Header("UŒ‚—Í")]
    int _attack;

    [SerializeField]
    [Header("–hŒä—Í")]
    int _defense;

    [SerializeField]
    [Header("‘¬‚³")]
    int _speed;

    [SerializeField]
    [Header("HP")]
    int _hP;

    [SerializeField]
    [Header("MP")]
    int _mP;

    [SerializeField]
    [Header("ŒoŒ±’l")]
    int _exP;

    [SerializeField]
    [Header("‹à")]
    int _gold;

    [SerializeField]
    [Header("ƒXƒLƒ‹")]
    SkillData[] _skills;
}
