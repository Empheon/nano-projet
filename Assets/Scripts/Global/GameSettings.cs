using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Global
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        private void Start()
        {
            Instance = this;
        }

        [Header("Settings")]

        public float AttackCooldown;

        public float DefenceCooldown;

        public float JammingCooldown;

        public float JammingDuration;
    }
}
