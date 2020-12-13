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
        
        bool PauseThisFrame();


        void UpdateState();
        bool IsConnected();
        bool IsReady();
    }
}