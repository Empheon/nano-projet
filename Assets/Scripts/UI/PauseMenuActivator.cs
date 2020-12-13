using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Inputs;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class PauseMenuActivator : MonoBehaviour
    {
        [SerializeField] private CharacterSpawner spawner;
        [SerializeField] private UIPauseManager pauseMenu;

        private List<IGameController> _gcs;
        
        private IEnumerator Start()
        {
            // wait for character to spawn
            yield return new WaitForEndOfFrame();

            _gcs = spawner.Characters
                .Select(character => 
                    character
                        .GetComponent<CharacterController>()
                        .GetGamepad())
                .ToList();
        }

        private void Update()
        {
            if (_gcs != null && _gcs.Any(gc => gc.PauseThisFrame()))
            {
                pauseMenu.Open();
            }
        }
    }
}