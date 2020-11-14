using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Header("Settings")]

    public float AttackCooldown;

    public float DefenceCooldown;

    public float JammingCooldown;
}
