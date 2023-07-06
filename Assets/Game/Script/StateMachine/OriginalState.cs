using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalState : IState
{
    private float timer = 0;
    private float ranTime;
    public void OnEnter(BotController bot)
    {
        ranTime = 0.3f;
    }
    public void OnExecute(BotController bot)
    {
        timer += Time.deltaTime;
        if (timer > ranTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }
    public void OnExit(BotController bot)
    {

    }
}
