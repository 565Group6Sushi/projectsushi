using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
	[SerializeField]
	private CinemachineVirtualCamera activeCam; //the active virtual camera in the scene

	[SerializeField]
	private CinemachineVirtualCamera[] nonActiveCams; //all deactivated virtual cameras in the scene

	//Start is called before the first frame update
	void Start()
	{

	}

	//Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		//set array index 0 to the active camera being switched to
		activeCam.Priority = 1;

		//set all non-active cameras to any other 
		for (int i = 0; i < nonActiveCams.Length; i++)
		{
			nonActiveCams[i].Priority = 0;
		}
	}
}