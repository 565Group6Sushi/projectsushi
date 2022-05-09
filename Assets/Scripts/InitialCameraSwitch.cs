using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitialCameraSwitch : MonoBehaviour
{
	[SerializeField]
	private CinemachineVirtualCamera newActiveCam; //the new active virtual cam we are swapping to

	[SerializeField]
	private CinemachineVirtualCamera oldActiveCam; //the old active vitual cam we are swapping from
	void Start()
	{
		StartCoroutine(ChangeCameraWait());
	}

	IEnumerator ChangeCameraWait()
    {
		yield return new WaitForSeconds(1);
		newActiveCam.Priority = 1;
		oldActiveCam.Priority = 0;
	}

}