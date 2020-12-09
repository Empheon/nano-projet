using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

namespace NeoMecha
{
    public class FixConsole : Console
    {
        [SerializeField] private float repairCooldown;
        [SerializeField] private float repairDelay;
        [SerializeField] private List<Room> rooms;

        [SerializeField] private UnityEvent OnRepairStart;
        [SerializeField] private UnityEvent OnRepairFinished;
        
        private float _timeSinceLastRepair;
        private bool _isFixing;
        
        private void Start()
        {
            _timeSinceLastRepair = repairCooldown;
        }

        private void Update()
        {
            _timeSinceLastRepair += Time.deltaTime;
        }

        public void OnCharacterInteract(GameObject character)
        {
            if (!CanInteract(character)) return;

            StartCoroutine(WaitAndFix());
        }

        private IEnumerator WaitAndFix()
        {
            OnRepairStart.Invoke();
            _isFixing = true;
            yield return new WaitForSeconds(repairDelay);
            OnRepairFinished.Invoke();
            _isFixing = false;
            
            foreach (var room in rooms)
            {
                if (room.IsDamaged) room.OnFixReceived();
            }
        }

        public override bool CanInteract(GameObject character)
        {
            var anyRoomDamaged = rooms.Any(room => room.IsDamaged);
            return anyRoomDamaged && !_isFixing && _timeSinceLastRepair > repairCooldown;
        }
    }
}
