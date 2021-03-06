﻿/*
 * (c) 2012 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 *  Simon Wollwage (rootnode) <kintaro@think-in-co.de>
 */

using System;

namespace Mosa.Platform.AVR32.Intrinsic
{
	public class DelegateStub
	{
		private object instance = null;
		private uint methodPointer = 0;

		public DelegateStub(object instance, uint methodPointer)
		{
			this.instance = instance;
			this.methodPointer = methodPointer;
		}

		public void Invoke()
		{
			if (instance == null)
			{
				Native.InvokeDelegate(instance, methodPointer);
			}
			else
			{
				Native.InvokeInstanceDelegate(instance, methodPointer);
			}
		}

		public object InvokeWithReturn()
		{
			if (instance == null)
			{
				return Native.InvokeDelegateWithReturn(instance, methodPointer);
			}
			else
			{
				return Native.InvokeInstanceDelegateWithReturn(instance, methodPointer);
			}
		}

		public IAsyncResult BeginInvoke(AsyncCallback callback, object arg)
		{
			return null;
		}

		public void EndInvoke(AsyncCallback result)
		{ }
	}
}