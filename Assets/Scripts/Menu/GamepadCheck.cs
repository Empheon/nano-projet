using System.Collections;
using Animations;
using Global;
using Inputs;
using UnityEngine;

namespace Menu
{
    public class GamepadCheck : MonoBehaviour
    {
        [SerializeField] private ConnexionCircle circle;
        [SerializeField] private int watchIndex = -1;

        public bool IsConnected { get; private set; }
        public bool IsReady { get; private set; }
        
        private void Update()
        {
            if (watchIndex < PlayerManager.Instance.Players.Count)
            {
                var player = PlayerManager.Instance.Players[watchIndex];
                var gc = player.GameController;

                if (!IsConnected && gc.IsConnected())
                {
                    IsConnected = true;
                    circle.Appear();
                }
                else if(IsConnected && !gc.IsConnected())
                {
                    IsConnected = false;
                    circle.Disappear();
                }

                if (IsConnected)
                {
                    if (!IsReady && gc.IsReady())
                    {
                        IsReady = true;
                        circle.Validate();
                    }
                    else if (IsReady && !gc.IsReady())
                    {
                        IsReady = false;
                        circle.UnValidate();
                    }
                }
            }
            else
            {
                if (IsReady || IsConnected)
                {
                    IsReady = false;
                    IsConnected = false;
                    
                    circle.Disappear();
                }
            }
        }
    }
}