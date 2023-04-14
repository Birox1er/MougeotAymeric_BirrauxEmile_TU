using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject _prefab;
    [SerializeField] float _wait;

    Coroutine _routine;
    public Coroutine Routine
    {
        get => _routine;
        set
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
            }
            _routine = value;
        }
    }

    private void StartSpawning()
    {
        //if (_routine != null) return;
        _routine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(_prefab);
            yield return new WaitForSeconds(_wait);
        }

        yield return StartCoroutine(Wait());

        Debug.Log("orf");
        _routine = null;
        yield break;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
    }


#if false // Syntaxe Async / await
    private async Task<int> Start()
    {

        // await 

        return 12;
    }
#endif

#if false   // Méthode gestion du temps avec une méthode par "date" et une par "sablier"
    float _oldDate;
    float _remainingTime;
    private void Update()
    {
        Date précédente
        if (Time.time - _oldDate > _wait)        // 12 - 10.5  > 1f
        {
            Spawn();
            _oldDate = Time.time;
        }

        Sablier
       _remainingTime -= Time.deltaTime;
        if (_remainingTime < 0f)
        {
            Spawn();
            _remainingTime = 1f;
        }
    }
#endif

    void Spawn()
    {
        Instantiate(_prefab);
    }

}
