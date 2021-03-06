/*
PROJECT: Geo Run
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01
LICENSE: MIT License. For more information, click here https://github.com/ericchu1329/Geo-Run
DATE: 2022 February 17

This file contains the CrystalDisplay class, which updates the glow image of the crystal 
whenever the player picks up a crystal.
*/

using UnityEngine;

namespace GPEJ.UI
{
    public class CrystalDisplay : MonoBehaviour
    {
        [SerializeField] private RuntimeDataContainer runtime_data;

        private Animator animator;

        public void StartCrystalPickupAnimation()
        {
            runtime_data.crystals++;
            animator.SetTrigger("StartGlow");
        }
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }     
    }
}
