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
	/// Representations the x86 Lgdt instruction.
	/// </summary>
	public sealed class Lgdt : IIntrinsicMethod
	{

		#region Methods

		/// <summary>
		/// Replaces the intrinsic call site
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="typeSystem">The type system.</param>
		void IIntrinsicMethod.ReplaceIntrinsicCall(Context context, ITypeSystem typeSystem, IList<RuntimeParameter> parameters)
		{
			MemoryOperand operand = new MemoryOperand(GeneralPurposeRegister.EAX, BuiltInSigType.Ptr, new System.IntPtr(0));
			context.SetInstruction(X86.Mov, new RegisterOperand(BuiltInSigType.Ptr, GeneralPurposeRegister.EAX), context.Operand1);
			context.AppendInstruction(X86.Lgdt, null, operand);

			RegisterOperand ax = new RegisterOperand(BuiltInSigType.Int16, GeneralPurposeRegister.EAX);
			RegisterOperand ds = new RegisterOperand(BuiltInSigType.Int16, SegmentRegister.DS);
			RegisterOperand es = new RegisterOperand(BuiltInSigType.Int16, SegmentRegister.ES);
			RegisterOperand fs = new RegisterOperand(BuiltInSigType.Int16, SegmentRegister.FS);
			RegisterOperand gs = new RegisterOperand(BuiltInSigType.Int16, SegmentRegister.GS);
			RegisterOperand ss = new RegisterOperand(BuiltInSigType.Int16, SegmentRegister.SS);

			context.AppendInstruction(X86.Mov, ax, new ConstantOperand(BuiltInSigType.Int32, (int)0x00000010));
			context.AppendInstruction(X86.Mov, ds, ax);
			context.AppendInstruction(X86.Mov, es, ax);
			context.AppendInstruction(X86.Mov, fs, ax);
			context.AppendInstruction(X86.Mov, gs, ax);
			context.AppendInstruction(X86.Mov, ss, ax);
			context.AppendInstruction(X86.FarJmp);
		}

		#endregion // Methods

	}
}
