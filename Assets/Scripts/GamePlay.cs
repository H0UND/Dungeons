using TMPro;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField]
    private EnemySpawn _enemySpawn;

    private int _defeated;
    private int _score;

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private TMP_Text _scoreLabel;

    [SerializeField]
    private GameObject _winPanel;

    [SerializeField]
    private GameObject _losPanel;

    public void AddScore()
    {
        _score++;

        _scoreLabel.text = _score.ToString();
    }

    public void AddDefeated()
    {
        _defeated++;
        if (_defeated >= _enemySpawn.SpawnCount)
        {
            Debug.Log("Win");
            _door.SetActive(false);
        }
    }

    public void EndGameWin()
    {
        _winPanel.SetActive(true);
    }

    public void EndGameLos()
    {
        _losPanel.SetActive(true);
    }
}