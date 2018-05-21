using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {

	private string playerId;

	private ShipMovement movement;

	private SkillAScript skillA;

	private PrimaryFire fire;

    private PlayerSetup setup;

	public string PlayerId
	{
		get
		{
			return playerId;
		}

		set
		{
			playerId = value;
		}
	}

	public ShipMovement Movement
	{
		get
		{
			return movement;
		}

		set
		{
			movement = value;
		}
	}

	public SkillAScript SkillA
	{
		get
		{
			return skillA;
		}

		set
		{
			skillA = value;
		}
	}

	public PrimaryFire Fire
	{
		get
		{
			return fire;
		}

		set
		{
			fire = value;
		}
	}

    public PlayerSetup Setup
    {
        get
        {
            return setup;
        }

        set
        {
            setup = value;
        }
    }

    // Use this for initialization
    void Start () {
		Movement = GetComponent<ShipMovement>();
		SkillA = GetComponent<SkillAScript>();
		Fire = GetComponent<PrimaryFire>();
        Setup = GetComponent<PlayerSetup>();
	}
}
