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
	/// An operand, which represents a label in the program data.
	/// </summary>
	public sealed class LabelOperand : MemoryOperand
	{
		#region Data members

		/// <summary>
		/// Holds the label.
		/// </summary>
		private int label;

		/// <summary>
		/// Holds the name
		/// </summary>
		private string name;

		#endregion // Data members

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="LabelOperand"/>.
		/// </summary>
		/// <param name="type">The signature type of the operand data.</param>
		/// <param name="baseRegister">The base register for label offsets.</param>
		/// <param name="offset">The default offset from the base register.</param>
		/// <param name="label">The additional offset as indicated by a label.</param>
		public LabelOperand(SigType type, Register baseRegister, int offset, int label) :
			base(baseRegister, type, new IntPtr(offset))
		{
			this.label = label;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LabelOperand"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="name">The name.</param>
		public LabelOperand(SigType type, string name) :
			base(null, type, IntPtr.Zero)
		{
			this.name = name;
		}

		#endregion // Construction

		#region Properties

		/// <summary>
		/// Gets the label of the operand.
		/// </summary>
		/// <value>The label.</value>
		public string Name
		{
			get { return name; }
		}

		/// <summary>
		/// Gets the label of the operand.
		/// </summary>
		/// <value>The label.</value>
		public int Label
		{
			get { return label; }
		}

		#endregion // Properties

		#region Object Overrides

		/// <summary>
		/// Returns a string representation of <see cref="Operand"/>.
		/// </summary>
		/// <returns>A string representation of the operand.</returns>
		public override string ToString()
		{
			if (name == null)
				return String.Format("[L_{0}, ", label) + " " + base.ToString();
			else
				return name + " " + base.ToString();
		}

		#endregion // Object Overrides
	}
}


