using UnityEngine;
using System.Collections;
using Pathfinding;

public abstract class Enemy : BaseUnit
{
	protected BaseUnit target;
	protected float distFromTarget;
	protected float attackRange;
	protected float aggroRange;

	protected Seeker seeker;
	protected Path path;
	public float repathRate = 1f;
	public float lastRepath = 0;
	public int currentWP = 0;
	public float nextWaypointDistance = 3;

	protected GameObject marker;
	protected Transform minimap;

	protected BaseUnit FindTarget()
	{
		//finds all objects with tag Enemy and assigns them to a group
		GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");

		//Distance at which a minion will start attacking.
		float closestDist = aggroRange;
		GameObject closestEnemyObj = null;//tracks closest enemy object
		foreach(GameObject target in minions) {
			distFromTarget = Vector3.Distance(target.transform.position, transform.position);
			if (distFromTarget < closestDist) {
				closestDist = distFromTarget;
				closestEnemyObj = target;
			}
		}
		if(closestEnemyObj != null)
			return closestEnemyObj.GetComponent<BaseUnit>();
		else
			return null;
	}
	
	public void OnPathComplete (Path p) {
		p.Claim (this);
		if (!p.error) {
			if (path != null) path.Release (this);
			path = p;
		} else {
			p.Release (this);
			Debug.Log ("Oh noes, the target was not reachable: "+p.errorLog);
		}
	}

	protected override void Move() {
		if (path == null) {
			return;
		}
		
		if (currentWP >= path.vectorPath.Count)
			currentWP = 0;
		else {
			Vector3 dir = (path.vectorPath[currentWP]-transform.position).normalized;
			dir *= moveSpeed * Time.deltaTime;
			transform.Translate(dir);
		}
		currentWP ++;
	}
	
	public override void Die() {
		//ObjectPool.instance.PoolObject(this.gameObject); //We will switch to this once our prefabs are pooled.
		this.gameObject.SetActive (false);
		DestroyObject (this);
		DestroyObject (marker);
	}
}