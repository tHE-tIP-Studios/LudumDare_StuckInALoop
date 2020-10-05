using System.Collections;
using UnityEngine;

public class RampPool : GenericObjectPool<RampFunction>
{
    [SerializeField] private Transform[] _spawnPoints = default;
    [SerializeField] private float _timeBeforeFirstSpawn;
    [SerializeField] private float _cooldownTime;
    private WaitForSeconds _spawnCooldown;
    private bool _firstSpawn = default;

    private void Awake()
    {
        AddObjects(10);
        _spawnCooldown = new WaitForSeconds(_cooldownTime);
    }

    public void MatchBegins()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public void MatchEnded()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StopAllCoroutines();
        _firstSpawn = false;
    }

    private IEnumerator SpawnCoroutine()
    {
        if (!_firstSpawn)
        {
            yield return new WaitForSeconds(_timeBeforeFirstSpawn);
            _firstSpawn = true;
        }

        RampFunction wall = GetFromPool();
        wall.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        wall.gameObject.SetActive(true);
        yield return _spawnCooldown;
        StartCoroutine(SpawnCoroutine());
    }
}
