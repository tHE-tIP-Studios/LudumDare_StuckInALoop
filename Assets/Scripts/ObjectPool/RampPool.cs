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

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        if (!_firstSpawn)
        {
            print("First spawn");
            yield return new WaitForSeconds(_timeBeforeFirstSpawn);
            _firstSpawn = true;
        }
        print("Normal spawn");
        RampFunction wall = GetFromPool();
        wall.transform.SetParent(_spawnPoints[Random.Range(0, _spawnPoints.Length)]);
        wall.transform.localPosition = Vector3.zero;
        wall.gameObject.SetActive(true);
        yield return _spawnCooldown;
        StartCoroutine(SpawnCoroutine());
    }
}
