﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Simon Wollwage (rootnode) <kintaro@think-in-co.de>
 */

using System.Collections.Generic;
using Mosa.Compiler.Framework;
using Mosa.Compiler.Framework.Operands;
using Mosa.Compiler.Metadata.Signatures;
using Mosa.Compiler.TypeSystem;

namespace Mosa.Platform.x86.Intrinsic
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Get : IIntrinsicMethod
	{

		#region Methods

		/// <summary>
		/// Replaces the intrinsic call site
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="typeSystem">The type system.</param>
		void IIntrinsicMethod.ReplaceIntrinsicCall(Context context, ITypeSystem typeSystem, IList<RuntimeParameter> parameters)
		{
			Operand result = context.Result;

			RegisterOperand tmp = new RegisterOperand(BuiltInSigType.Ptr, GeneralPurposeRegister.EDX);
			MemoryOperand operand = new MemoryOperand(GeneralPurposeRegister.EDX, context.Operand1.Type, new System.IntPtr(0));

			context.SetInstruction(X86.Mov, tmp, context.Operand1);
			context.AppendInstruction(X86.Mov, result, operand);
		}

		#endregion // Methods

	}
}
