/*
NAME: GPEJ
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01

FILE DESCRIPTION:
This file contains the CrystalDisplay class, which updates
the glow image of the crystal whenever the player
picks up a crystal
*/
using UnityEngine;

namespace GPEJ.UI
{
    public class CrystalDisplay : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameStateManager.instance.OnPlayerCrystalPickup += StartCrystalPickupAnimation;
        }

        private void StartCrystalPickupAnimation()
        {
            animator.SetTrigger("StartGlow");
        }

        private void OnDestroy()
        {
            GameStateManager.instance.OnPlayerCrystalPickup -= StartCrystalPickupAnimation;
        }
    }
}
