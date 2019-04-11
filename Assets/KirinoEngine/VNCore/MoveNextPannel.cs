using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine {

	public class MoveNextPannel : MonoBehaviour {

		public virtual void MoveNext()
		{
			if(VNController.textDisplayer.isTyping)
			{
				VNController.textDisplayer.SkipTypingLetter();
				return;
			}


//			VNController.parser.MoveNext();
		}
	}

}