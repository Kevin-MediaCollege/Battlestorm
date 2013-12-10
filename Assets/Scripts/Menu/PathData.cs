using UnityEngine;
using System.Collections;

public class PathData : MonoBehaviour {

	private Vector3 pointA;
	private Vector3 pointB;

	private Quaternion rotation;

	private

	void Start(Vector3 a,Vector3 b,Quaternion rot){
		pointA = a;
		pointB = b;
		rotation = rot;
	}
}
