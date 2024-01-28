using System;
using UnityEngine;

[Serializable]
public class EnemySpawnSequence
{
    [SerializeField] 
    private EnemyFactory _factory;

    [SerializeField] 
    private EnemyType _type;

    [SerializeField, Range(1, 100)] 
    private int _amount = 1;

    [SerializeField, Range(0.1f, 10f)]
    private float _cooldownS = 1f;
    

    [Serializable]
    public struct State
    {
        private EnemySpawnSequence _sequence;
        private int _count;
        private float _cooldown;

        public State(EnemySpawnSequence sequence)
        {
            _sequence = sequence;
            _count = 0;
            _cooldown = sequence._cooldownS;
        }
        

        public float ProgressD(float deltaTimeF)
        {
            _cooldown += deltaTimeF;
            Debug.Log(_sequence);
            while (_cooldown >= _sequence._cooldownS)
            {
                _cooldown -= _sequence._cooldownS;
                if (_count >= _sequence._amount)
                {
                    return _cooldown;
                }
                _count += 1;
                Game.SpawnEnemy(_sequence._factory, _sequence._type);
            }
            return -1f;
        }
    }
    public State Begin() => new(this);
}
