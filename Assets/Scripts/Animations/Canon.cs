using NeoMecha;
using PathCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class Canon : MonoBehaviour
    {
        [SerializeField]
        private Rocket rocketPrefab;

        [SerializeField]
        private PathCreator pathCreatorAtt;
        [SerializeField]
        private PathCreator pathCreatorDef;
        [SerializeField]
        private PathCreator pathCreatorJam;

        private const string ATTACK_KEY = "attack";
        private const string DEFENCE_KEY = "defence";
        private const string JAMMING_KEY = "jam";

        public void Shoot(Room room)
        {
            Rocket rocket = Instantiate(rocketPrefab);
            switch (room.RoomType)
            {
                case RoomType.ATTACK:
                    rocket.Init(pathCreatorAtt);
                    break;
                case RoomType.DEFENCE:
                    rocket.Init(pathCreatorDef);
                    break;
                case RoomType.JAMMING:
                    rocket.Init(pathCreatorJam);
                    break;
            }
        }
    }
}
