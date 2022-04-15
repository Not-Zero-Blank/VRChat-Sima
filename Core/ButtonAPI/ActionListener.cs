using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ProjectXoX.Utility
{
    public class ActionListener : MonoBehaviour
    {
        public ActionListener(IntPtr obj0) : base(obj0) { }
		[method: HideFromIl2Cpp]
		public event Action OnEnabled;
		[method: HideFromIl2Cpp]
		public event Action OnDisabled;

		private void OnEnable()
		{
			Action onEnabled = this.OnEnabled;
			if (onEnabled == null)
			{
				return;
			}
			onEnabled();
		}

		private void OnDisable()
		{
			Action onDisabled = this.OnDisabled;
			if (onDisabled == null)
			{
				return;
			}
			onDisabled();
		}
	}
}
