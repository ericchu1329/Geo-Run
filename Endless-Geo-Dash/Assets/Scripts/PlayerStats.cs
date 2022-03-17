/*
NAME: GPEJ
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01

FILE DESCRIPTION:
This file contains the PlayerStats class, which contains
information about certain ingame stats
*/
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance { get; private set; }

    private float current_health;
    private int distance_traveled;

    private Ailments current_ailment;
    private float burning_multiplier;
    private float chilled_multiplier;
    private float grasped_multiplier;

    private void Awake()
    {
        instance = this;
    }

    public void ResetAllStats()
    {
        current_health = 100f;
        distance_traveled = 0;
        current_ailment = Ailments.None;
        burning_multiplier = chilled_multiplier = grasped_multiplier = 1f;
    }

    // Setters
    public void SetCurrentHealth(float val)
    {
        current_health = val;
    }

    public void SetDistanceTraveled(int val)
    {
        distance_traveled = val;
    }

    public void SetCurrentAilment(Ailments ailment)
    {
        current_ailment = ailment;
    }

    public void SetBurnMultiplier(float val)
    {
        burning_multiplier = val;
    }

    public void SetChillMultiplier(float val)
    {
        chilled_multiplier = val;
    }

    public void SetGraspMultiplier(float val)
    {
        grasped_multiplier = val;
    }

    // Getters

    public float GetCurrenHealth()
    {
        return current_health;
    }

    public int GetDistancedTraveled()
    {
        return distance_traveled;
    }

    public Ailments GetCurrentAilment()
    {
        return current_ailment;
    }

    public float GetBurnMultiplier()
    {
        return burning_multiplier;
    }

    public float GetChillMultiplier()
    {
        return chilled_multiplier;
    }

    public float GetGraspMultiplier()
    {
        return grasped_multiplier;
    }
}
