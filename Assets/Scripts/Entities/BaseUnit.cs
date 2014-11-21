﻿//TODO WEAPON CURRENTLY COMMENTED OUT
//TODO methods are currently abstract, therefore you cannot specify a body as they are in this class.
//	   Also Cannot change move because all base classes are overriding the original method.
//	   Not necessarily difficult fixes but need to know what's expected
using UnityEngine;
using System.Collections;

public abstract class BaseUnit : MonoBehaviour
{

    #region Unit Properties
    public Sprite sprite;
    //public float health;
    public float moveSpeed;
    public EntityState state;
    #endregion

//	variable declarations
	protected int maxHealth;
	protected int curHealth;
	protected string myName;
	//protected Weapon myWeapon;
	protected GameObject myCharacter;

//  properties for accessing the variables publicly
	public int MaxHealth { 
		get { return maxHealth; } 
        set { maxHealth = value;} 
	}
    public int CurHealth {
		get { return curHealth; }
        set { 
			if(value > maxHealth)
				curHealth = maxHealth;			
			else
				curHealth = value;
		} 
	}
    public string MyName{
		get { return myName; }
		set { myName = value;} 
	}
/*	public Weapon MyWeapon {
		get { return myWeapon; }
		set { myWeapon = value;}
	}
*/
	public GameObject Character {
		get { return myCharacter; }
		set { myCharacter = value;}
	}

	#region Unit Methods
	protected abstract void Move();
	protected abstract void Attack();
	public abstract void Die();
    #endregion
}