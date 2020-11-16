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
                // use character Y to make it not start from bottom each time
                var startPoint = new Vector2(transform.position.x, character.transform.position.y);
                
                ladderController.StartClimbing(startPoint);
            }
        }
    }
}