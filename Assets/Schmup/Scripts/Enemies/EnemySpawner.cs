using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Schmup
{
    public class EnemySpawner : MonoBehaviour
    {
        [System.Serializable]
        public struct RecurringEnemySpawn
        {
            public GameObject SpawnObject;
            public float SpawnInterval;
            public float SpawnIntervalOffset;
            public Transform SpawnPosition;
        }
        
        [SerializeField] private List<RecurringEnemySpawn> RecurringSpawns = new List<RecurringEnemySpawn>();
        
        private void Awake()
        {
            List<float> spawnDelays = SimulateRecurringSpawnsTimeline();
            StartCoroutine(SpawnReccuringEnemies(spawnDelays));
        }

        private IEnumerator SpawnReccuringEnemies(List<float> pSpawnDelays)
        {
            while (true)
            {
                for (int i = 0; i < RecurringSpawns.Count; i++)
                {
                    yield return new WaitForSeconds(pSpawnDelays[i]);
                    SpawnObject(RecurringSpawns[i].SpawnObject, RecurringSpawns[i].SpawnPosition.position);
                }
                yield return null;
            }
        }

        private List<float> SimulateRecurringSpawnsTimeline()
        {
            List<float> simulatedTimeline = new List<float>();
            float simulatedTimeSinceStart = 0.0f;
            
            foreach (RecurringEnemySpawn s in RecurringSpawns)
            {
                float nextTimeInterval = s.SpawnInterval;

                nextTimeInterval += s.SpawnIntervalOffset;
                nextTimeInterval -= simulatedTimeSinceStart;
                simulatedTimeline.Add(nextTimeInterval);
                simulatedTimeSinceStart += nextTimeInterval;
            }
            
            return simulatedTimeline;
        }
        

        private void SpawnObject(GameObject pObjectToSpawn, Vector3 pSpawnPosition)
        {
            Instantiate(pObjectToSpawn, pSpawnPosition, Quaternion.identity); //TODO: objectpool this shit :)
        }
    }
}

