using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Cysharp.Threading.Tasks;

public class BattlePresenter : MonoBehaviour
{
    [SerializeField]
    private BattleView _battleView;

    [SerializeField]
    [Header("最初の行動")]
    private string[] _firstActions;

    [SerializeField]
    private BattlePlayer _battlePlayer;

    private void Start()
    {
        _battleView.SetMaxHP(_battlePlayer.HP);

        _battlePlayer
            .ObserveEveryValueChanged(x => x.HP)
            .Subscribe(x => _battleView.SetHP(x));

        BattleManager
            .Instance
            .PhaseManagerInstance
            .ObserveEveryValueChanged(x => x.PhaseState)
            .Subscribe(state =>
            {
                if(state == BattlePhaseState.PlayerTurn)
                {
                    _battlePlayer.SelectAction();
                }
            });

        _battlePlayer
            .ObserveEveryValueChanged(x => x.SelectActionPhase)
            .Subscribe(async phase =>
            {
                switch (phase)
                {
                    case PlayerSelectActionPhase.SelectAction:

                        _battleView.SetButton(_firstActions.Length);
                        _battleView.ButtonTextChenge(_firstActions);
                        Action[] actions = new Action[_firstActions.Length];
                        actions[0] = () => _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectEnemy);
                        actions[1] = () => _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectSkill);
                        actions[2] = () => _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectMagic);
                        actions[3] = () => _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectItem);
                        actions[4] = () => _battlePlayer.SetActionPhase(PlayerSelectActionPhase.RunAway);
                        
                        _battleView.ButtonActionChange(actions);
                        break;

                    case PlayerSelectActionPhase.SelectEnemy:

                        _battleView.SetButton(BattleManager.Instance.Enemies.Count);

                        string[] enemyNames = new string[BattleManager.Instance.Enemies.Count];
                        for(int i = 0; i < BattleManager.Instance.Enemies.Count; i++)
                        {
                            enemyNames[i] = BattleManager.Instance.Enemies[i].Name;
                        }

                        _battleView.ButtonTextChenge(enemyNames);
                        Action[] selectActions = new Action[BattleManager.Instance.Enemies.Count];
                        for (int i = 0; i < selectActions.Length; i++)
                        {
                            var x = i;
                            selectActions[x] = () =>
                            {
                                _battlePlayer.SetTarget(BattleManager.Instance.Enemies[x]);
                                _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectNomalAttack);
                            };
                        }

                        _battleView.ButtonActionChange(selectActions);

                        break;

                    case PlayerSelectActionPhase.SelectItem:

                        if(_battlePlayer.Items.Count == 0)
                        {
                            Debug.Log("アイテムを持っていません");
                            break;
                        }

                        _battleView.SetButton(_battlePlayer.Items.Count);
                        string[] items = new string[_battlePlayer.Items.Count];
                        for(int i = 0; i < _battlePlayer.Items.Count; i++)
                        {
                            items[i] = _battlePlayer.Items[i].Name;
                        }
                        _battleView.ButtonTextChenge(items);
                        Action[] selectItems = new Action[_battlePlayer.Items.Count];
                        
                        break;

                    case PlayerSelectActionPhase.SelectNomalAttack:

                        _battleView.SetButton(1);// ただの攻撃なのでとくに選ぶものがない
                        string[] s = new string[1];
                        s[0] = "Attack";
                        _battleView.ButtonTextChenge(s);
                        Action[] nomalAttack = new Action[1];
                        nomalAttack[0] = () =>
                        {
                            _battlePlayer.Target.GetComponent<BattleEnemy>().ReciveDamage(_battlePlayer.Attack);
                            _battlePlayer.SetActionPhase(PlayerSelectActionPhase.None);
                        };
                        _battleView.ButtonActionChange(nomalAttack);

                        break;

                    case PlayerSelectActionPhase.SelectSkill:
       
                        if(_battlePlayer.Skills.Count == 0)
                        {
                            Debug.Log("スキルを習得していません");
                            break;
                        }
                        _battleView.SetButton(_battlePlayer.Skills.Count);
                        string[] skills = new string[_battlePlayer.Skills.Count];
                        for(int i = 0; i < _battlePlayer.Skills.Count; i++)
                        {
                            skills[i] = _battlePlayer.Skills[i].Name;
                        }
                        _battleView.ButtonTextChenge(skills);

                        break;

                    case PlayerSelectActionPhase.SelectMagic:
              
                        if(_battlePlayer.Magics.Count == 0)
                        {
                            Debug.Log("魔法を習得していません");
                            break;
                        }
                        _battleView.SetButton(_battlePlayer.Magics.Count);
                        string[] magics = new string[_battlePlayer.Magics.Count];
                        for(int i = 0; i < _battlePlayer.Magics.Count; i++)
                        {
                            magics[i] = _battlePlayer.Magics[i].Name;
                        }
                        _battleView.ButtonTextChenge(magics);

                        break;

                    case PlayerSelectActionPhase.RunAway:

                        SceneLoder.LoadBeforeScene();
                        Debug.Log("逃げた");

                        break;

                    case PlayerSelectActionPhase.None:

                        _battleView.SetButton(0);

                        await UniTask.Delay(1000);

                        
                        try
                        {
                            foreach (var enemy in BattleManager.Instance.Enemies)
                            {
                                _battlePlayer.ReciveDamage(enemy.Attack);
                                await UniTask.Delay(1000);
                            }
                        }
                        catch(NullReferenceException)
                        {
                            
                        }
                        

                        _battlePlayer.SetActionPhase(PlayerSelectActionPhase.SelectAction);

                        break;
                }
            });
    }
}
