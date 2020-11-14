using Character;
using UnityEngine;

namespace Ladder
{
    public class LadderStart : MonoBehaviour
    {
        private void OnCharacterInteract(GameObject character)
        {
            if (character.TryGetComponent(out CharacterLadderController ladderController))
            {
                ladderController.StartClimbing(transform.position);
            }
        }
    }
}