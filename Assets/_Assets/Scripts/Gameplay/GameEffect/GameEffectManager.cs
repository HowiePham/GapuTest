using System.Collections.Generic;
using UnityEngine;

public class GameEffectManager : TemporaryMonoSingleton<GameEffectManager>
{
    private Dictionary<GameEffectType, GameEffectHandler> _gameEffectHandlerList;

    protected override void Init()
    {
        InitSystem();
    }

    private void InitSystem()
    {
        _gameEffectHandlerList = new Dictionary<GameEffectType, GameEffectHandler>();
        foreach (Transform obj in transform)
        {
            var gameEffectHandler = obj.GetComponent<GameEffectHandler>();
            if (gameEffectHandler == null) continue;

            var gameEffectType = gameEffectHandler.GetGameEffectType();
            _gameEffectHandlerList.Add(gameEffectType, gameEffectHandler);
        }
    }

    public void CreateEffectAt(GameEffectType gameEffectType, Vector2 position)
    {
        var gameEffectHandler = _gameEffectHandlerList[gameEffectType];
        gameEffectHandler.CreateGameEffectAt(position);
    }
}