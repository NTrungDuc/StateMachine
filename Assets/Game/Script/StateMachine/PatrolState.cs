using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer = 0;
    float duration;
    public void OnEnter(BotController bot)
    {
        bot.randomMove();
        duration = Random.Range(1f, 4f);
    }
    public void OnExecute(BotController bot)
    {
        timer += Time.deltaTime;
        bot.TargetObj();
        if (timer > duration)
        {
            bot.ChangeState(new OriginalState());
        }
        if(bot.isTargetInRange())
        {
            bot.ChangeState(new AttackState());
        }
    }
    public void OnExit(BotController bot)
    {

    }
}
