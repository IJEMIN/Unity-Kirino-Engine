using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
	public class VNMusicStop : VNCommand
	{
		public override void Invoke()
		{
            	VNController.audioManager.StopMusic();
		}
	}
}
