using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox.Examples;

public class UnlockTechtree : MonoBehaviour
{
	public void Unlock()
	{
		UIManager.Instance.UnlockTechtree();
	}
}
