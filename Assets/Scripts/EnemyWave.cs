using System;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave: ScriptableObject
{
    [SerializeField]
    private EnemySpawnSequence[] _spawnSequence;

    public State Begin() => new State(this);

    [Serializable]
    public struct State
    {
        private EnemyWave _wave;
        private int _index;
        private EnemySpawnSequence.State _sequenceD;

        public State(EnemyWave wave)
        {
            _wave = wave;
            _index = 0;
            _sequenceD = _wave._spawnSequence[0].Begin();
            Debug.Log(_sequenceD);
        }

        public float Progress(float deltaTimeD)
        {
            deltaTimeD = _sequenceD.ProgressD(deltaTimeD);
            while(deltaTimeD >= 0) 
            {
                if(++_index >= _wave._spawnSequence.Length) 
                {
                    return deltaTimeD;
                }

                _sequenceD  = _wave._spawnSequence[_index].Begin();
                deltaTimeD = _sequenceD.ProgressD(deltaTimeD);
            }
            return -1f;
        }
    }
}

