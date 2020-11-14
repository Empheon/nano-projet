using Global;
using UnityEngine;

namespace Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform teamLeftSpawn;
        [SerializeField] private Transform teamRightSpawn;
        [SerializeField] private GameObject characterPrefab;
        
        private void Start()
        {
            foreach (var player in PlayerManager.Instance.Players)
            {
                var spawn = player.team == PlayerManager.Team.Left ? teamLeftSpawn : teamRightSpawn;
                var character = Instantiate(characterPrefab, spawn.position, Quaternion.Euler(0, 0, 0));

                // send gamepad to all object who might need it.
                character.BroadcastMessage("OnSpawn", player.gamepad, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}