using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;

[RequireComponent(typeof(Seeker))]
[AddComponentMenu("Pathfinding/AI/AIPath (generic)")]
public class PathFollower:MonoBehaviour {
	public float endReachedDistance = 0.1f;
	public float pickNextWaypointDist = 1;
	public float slowdownDistance = 0.7f;
	public float turningSpeed = 4;
	public float forwardLook = 0.8f;
	public float speed = 5;
	
	public bool closestOnPathCheck = true;
	public bool canSearch = true;
	public bool canMove = true;

	public Transform target;

	protected int currentWaypointIndex = 0;

	protected float lastFoundWaypointTime = -9999;
	protected float minMoveScale = 0.05F;

	protected bool targetReached = false;
	protected bool canSearchAgain = true;

	protected Vector3 lastFoundWaypointPosition;
	protected Vector3 targetDirection;
	protected Vector3 targetPoint;
	protected Seeker seeker;
	protected Transform tr;
	protected Path path;

	private bool startHasRun = false;

	public bool TargetReached {
		get { return targetReached;	}
	}
	
	protected virtual void Awake() {
		seeker = GetComponent<Seeker>();

		tr = transform;
	}

	public virtual void Start () {
		startHasRun = true;
		OnEnable();
	}

	protected virtual void OnEnable() {
		canSearchAgain = true;
		
		lastFoundWaypointPosition = GetFeetPosition();
		
		if(startHasRun) {
			seeker.pathCallback += OnPathComplete;
			
			SearchPath();
		}
	}
	
	public void OnDisable() {
		if(seeker != null && !seeker.IsDone()) {
			seeker.GetCurrentPath().Error();
		}

		if(path != null) {
			path.Release (this);
		}

		path = null;

		seeker.pathCallback -= OnPathComplete;
	}

	public virtual void SearchPath() {
		if(target == null) {
			throw new System.InvalidOperationException("Target is null");
		}

		Vector3 targetPosition = target.position;

		canSearchAgain = false;

		seeker.StartPath(GetFeetPosition(), targetPosition);
	}
	
	public virtual void OnTargetReached() { }

	public virtual void OnPathComplete(Path _p) {
		ABPath p = _p as ABPath;

		if(p == null) {
			throw new System.Exception("This function only handles ABPaths, do not use special path types");
		}
		
		canSearchAgain = true;
		p.Claim(this);

		if(p.error) {
			p.Release(this);
			return;
		}

		if(path != null) {
			path.Release (this);
		}

		path = p;

		currentWaypointIndex = 0;
		targetReached = false;
		
		if(closestOnPathCheck) {
			Vector3 p1 = Time.time - lastFoundWaypointTime < 0.3f ? lastFoundWaypointPosition : p.originalStartPoint;
			Vector3 p2 = GetFeetPosition();
			Vector3 dir = p2 - p1;
			float magn = dir.magnitude;
			dir /= magn;
			int steps = (int)(magn / pickNextWaypointDist);
			
			
			for(int i = 0; i <= steps; i++) {
				CalculateVelocity (p1);
				p1 += dir;
			}
		}
	}
	
	public virtual Vector3 GetFeetPosition() {
		return tr.position;
	}
	
	public virtual void Update() {
		if(!canMove) { 
			return;
		}
		if(currentWaypointIndex != null && path != null){
			transform.position = Vector3.Lerp(transform.position,path.vectorPath[currentWaypointIndex],(Time.deltaTime / speed));
		}
		Vector3 dir = CalculateVelocity(GetFeetPosition());
		RotateTowards(targetDirection);
		
		transform.Translate(dir * Time.deltaTime, Space.World);
	}

	protected float XZSqrMagnitude (Vector3 a, Vector3 b) {
		float dx = b.x-a.x;
		float dz = b.z-a.z;
	
		return dx*dx + dz*dz;
	}

	protected Vector3 CalculateVelocity (Vector3 currentPosition) {
		if(path == null || path.vectorPath == null || path.vectorPath.Count == 0) {
			return Vector3.zero;
		}
		
		List<Vector3> vPath = path.vectorPath;
			
		if(vPath.Count == 1) {
			vPath.Insert (0, currentPosition);
		}
		
		if(currentWaypointIndex >= vPath.Count) { 
			currentWaypointIndex = vPath.Count - 1;
		}
		
		if(currentWaypointIndex <= 1) {
			currentWaypointIndex = 1;
		}
		
		while(true) {
			if(currentWaypointIndex < vPath.Count - 1) {
				float dist = XZSqrMagnitude (vPath[currentWaypointIndex], currentPosition);

				if(dist < pickNextWaypointDist * pickNextWaypointDist) {
					lastFoundWaypointPosition = currentPosition;
					lastFoundWaypointTime = Time.time;
					currentWaypointIndex++;
				} else {
					break;
				}
			} else {
				break;
			}
		}
		
		Vector3 dir = vPath[currentWaypointIndex] - vPath[currentWaypointIndex-1];
		Vector3 targetPosition = CalculateTargetPoint (currentPosition,vPath[currentWaypointIndex-1] , vPath[currentWaypointIndex]);		
		
		dir = targetPosition - currentPosition;
		float targetDist = dir.magnitude;
		
		float slowdown = Mathf.Clamp01 (targetDist / slowdownDistance);
		
		this.targetDirection = dir;
		this.targetPoint = targetPosition;
		
		if(currentWaypointIndex == vPath.Count-1 && targetDist <= endReachedDistance) {
			if(!targetReached) { 
				targetReached = true;
				OnTargetReached();
			}

			return Vector3.zero;
		}
		
		Vector3 forward = tr.forward;
		float dot = Vector3.Dot (dir.normalized, forward);
		float sp = speed * Mathf.Max (dot, minMoveScale) * slowdown;
		
		
		if (Time.deltaTime	> 0) {
			sp = Mathf.Clamp(sp, 0, targetDist / (Time.deltaTime * 2));
		}

		return forward * sp;
	}

	protected virtual void RotateTowards(Vector3 dir) {	
		if(dir == Vector3.zero) {
			return;
		}
		
		Quaternion rot = tr.rotation;
		Quaternion toTarget = Quaternion.LookRotation(dir);
		
		rot = Quaternion.Slerp(rot, toTarget,turningSpeed * Time.deltaTime);
		Vector3 euler = rot.eulerAngles;
		euler.z = 0;
		euler.x = 0;
		rot = Quaternion.Euler(euler);
		
		tr.rotation = rot;
	}

	protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b) {
		a.y = p.y;
		b.y = p.y;
		
		float magn = (a - b).magnitude;
		if (magn == 0) {
			return a;
		}
		
		float closest = AstarMath.Clamp01(AstarMath.NearestPointFactor(a, b, p));
		Vector3 point = (b - a) * closest + a;
		float distance = (point - p).magnitude;
		
		float lookAhead = Mathf.Clamp(forwardLook - distance, 0.0F, forwardLook);
		
		float offset = lookAhead / magn;
		offset = Mathf.Clamp(offset + closest, 0.0F, 1.0F);

		return (b - a) * offset + a;
	}
}