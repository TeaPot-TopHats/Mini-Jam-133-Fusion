using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCursor : MonoBehaviour
{
	[SerializeField] private GameObject Cursor;
	 private Transform CursorTransform;
	
	void Start()
	{
		CursorTransform = Cursor.GetComponent<Transform>();
	}
	
	void Update()
	{
		transform.position = CursorTransform.position;
	}
}
