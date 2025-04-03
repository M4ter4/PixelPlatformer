using System;
using System.Collections.Generic;
using Basic;
using UnityEngine;

namespace Level
{
    public class RoomReset : MonoBehaviour
    {
        [SerializeField] private GameObject trapHolder;
        private List<GameObject> _traps;
        private Vector3[] _spawnPointsTraps;
        
        [SerializeField] private GameObject enemyHolder;
        private List<GameObject> _enemies;
        private Vector3[] _spawnPointsEnemies;

        public Vector3 RoomCenterPosition;

        private void Start()
        {
            SaveTraps();
            SaveEnemies();
        }

        private void SaveTraps()
        {
            _traps = new List<GameObject>();
            if (trapHolder == null)
            {
                _spawnPointsTraps = Array.Empty<Vector3>();
                return;
            }
        
            foreach (Transform child in trapHolder.transform)
            {
                _traps.Add(child.gameObject);
            }
            _spawnPointsTraps = new Vector3[_traps.Count];
            for (int i = 0; i < _traps.Count; i++)
            {
                if(_traps[i] != null)
                    _spawnPointsTraps[i] = _traps[i].transform.position;
            }
        }

        private void SaveEnemies()
        {
            _enemies = new List<GameObject>();
            if (enemyHolder == null)
            {
                _spawnPointsEnemies = Array.Empty<Vector3>();
                return;
            }
        
            foreach (Transform child in enemyHolder.transform)
            {
                _enemies.Add(child.gameObject);
            }
            _spawnPointsEnemies = new Vector3[_enemies.Count];
            for (int i = 0; i < _enemies.Count; i++)
            {
                if(_enemies[i] != null)
                    _spawnPointsEnemies[i] = _enemies[i].transform.position;
            }
        }

        public void ActivateRoom(bool status)
        {
            for (int i = 0; i < _traps.Count; i++)
            {
                if(_traps[i] != null)
                {
                    _traps[i].SetActive(status);
                    _traps[i].transform.position = _spawnPointsTraps[i];
                }
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                {
                    _enemies[i].SetActive(status);
                    _enemies[i].transform.position = _spawnPointsEnemies[i];
                    _enemies[i].GetComponentInChildren<Health>().Revive();
                }
            }
        }
    }
}
