using Configs;
using UnityEngine;

public class HostSettings : MonoBehaviour
{
    private int _timePerGame, _playerAmount;

    public void GameTime(int timeAmount)
    {
        _timePerGame = timeAmount;
    }

    public void SetPlayerAmount(int players)
    {
        _playerAmount = players;
    }

    public void SetDifficulty(int levelAmount)
    {
        StaticConfig.PublicConfig.levelSetting = levelAmount;
    }
}
