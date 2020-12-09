using UnityEngine;

namespace Inputs
{
    public interface IGameController
    {
        bool InteractThisFrame();
        bool ValidateThisFrame();
        bool CancelThisFrame();
        bool JumpThisFrame();
        bool KeepInAir();
        Vector2 GetMovement();


        void UpdateState();
        bool IsConnected();
        bool IsReady();
    }
}