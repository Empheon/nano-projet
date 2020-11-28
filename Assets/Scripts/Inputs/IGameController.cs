using UnityEngine;

namespace Inputs
{
    public interface IGameController
    {
        bool InteractThisFrame();
        bool CancelThisFrame();
        bool JumpThisFrame();
        Vector2 GetMovement();


        void UpdateState();
        bool IsConnected();
        bool IsReady();
    }
}