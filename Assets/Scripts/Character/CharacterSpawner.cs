using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;

namespace Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform teamLeftSpawn;
        [SerializeField] private Transform teamRightSpawn;
        [SerializeField] private GameObject characterPrefab;
        [SerializeField] private int maxNbPlayer = 2;

        public List<GameObject> Characters { get; private set; } = new List<GameObject>();
        
        private void Start()
        {
            foreach (var player in PlayerManager.Instance.Players.Take(maxNbPlayer))
            {
                var spawn = player.Team == PlayerManager.Team.Left ? teamLeftSpawn : teamRightSpawn;
                var character = Instantiate(characterPrefab, spawn.position, Quaternion.Euler(0, 0, 0));

                Characters.Add(character);
                
                // send gamepad to all object who might need it.
                character.BroadcastMessage("OnSpawn", player.GameController, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}