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

        private Animator m_animator;
        private Room m_roomToShoot;
        private Action m_callback;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Shoot(Room room, Action callback)
        {
            m_roomToShoot = room;
            m_callback = callback;
            m_animator.SetTrigger("Shoot");
        }

        public void OnShoot()
        {
            Rocket rocket = Instantiate(rocketPrefab);
            var pos = rocket.transform.position;
            pos.z = transform.position.z;
            rocket.transform.position = pos;

            switch (m_roomToShoot.RoomType)
            {
                case RoomType.ATTACK:
                    rocket.Init(pathCreatorAtt, m_callback);
                    break;
                case RoomType.DEFENCE:
                    rocket.Init(pathCreatorDef, m_callback);
                    break;
                case RoomType.JAMMING:
                    rocket.Init(pathCreatorJam, m_callback);
                    break;
            }
        }
    }
}
