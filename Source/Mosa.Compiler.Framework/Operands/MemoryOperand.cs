/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using System;
using Mosa.Compiler.Metadata.Signatures;

namespace Mosa.Compiler.Framework.Operands
{
	/// <summary>
	/// Specifies a memory operand defined by an offset and an optional base register.
	/// </summary>
	public class MemoryOperand : Operand
	{
		#region Data members

		/// <summary>
		/// Holds the base register, if this is a relative memory access.
		/// </summary>
		private readonly Register _base;

		/// <summary>
		/// Holds the address offset if used together with a base register or the absolute address, if register is null.
		/// </summary>
		private IntPtr offset;

		#endregion // Data members

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="MemoryOperand"/>.
		/// </summary>
		/// <param name="base">The base register, if this is an indirect access.</param>
		/// <param name="type">The type of data held in the operand.</param>
		/// <param name="offset">The offset from the base register or absolute address to retrieve.</param>
		public MemoryOperand(Register @base, SigType type, IntPtr offset) :
			base(type)
		{
			this._base = @base;
			this.offset = offset;
		}

		#endregion // Construction

		#region Properties

		/// <summary>
		/// Retrieves the base register of this memory operand.
		/// </summary>
		public Register Base
		{
			get { return _base; }
		}

		/// <summary>
		/// Retrieves the offset from the base register, or the absolute address if base is null.
		/// </summary>
		public IntPtr Offset
		{
			get { return offset; }
			set { offset = value; }
		}

		#endregion // Properties

		#region Operand Overrides

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			if (_base == null)
			{
				if (offset.ToInt32() > 0)
					return String.Format("[{0:X}h] {1}", offset.ToInt32(), base.ToString());
				return String.Format("[-{0:X}h] {1}", -offset.ToInt32(), base.ToString());
			}
			{
				if (offset.ToInt32() > 0)
					return String.Format("[{0}+{1:X}h] {2}", _base, offset.ToInt32(), base.ToString());
				return String.Format("[{0}-{1:X}h] {2}", _base, -offset.ToInt32(), base.ToString());
			}
		}

		#endregion // Operand Overrides
	}
}


