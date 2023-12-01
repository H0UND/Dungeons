using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private EnemyCore[] _enemyPrefabs;

    [SerializeField]
    private List<EnemyCore> _enemyes;

    [SerializeField]
    private int _spawnCount = 0;

    [SerializeField]
    private TMP_Text _timerLabel;

    private float _activateTimer;

    public int SpawnCount { get { return _spawnCount; } }

    private void Start()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-5.5f, 5.5f),
                0,
                Random.Range(-4f, 10f));
            var enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], position, Quaternion.identity);
            _enemyes.Add(enemy);
        }

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_activateTimer <= 3f)
        {
            _activateTimer += Time.deltaTime;

            _timerLabel.text = ((int)_activateTimer).ToString();
            yield return new WaitForEndOfFrame();
        }

        foreach (var enemy in _enemyes)
        {
            enemy.Active = true;
        }
        _timerLabel.text = string.Empty;
    }


}