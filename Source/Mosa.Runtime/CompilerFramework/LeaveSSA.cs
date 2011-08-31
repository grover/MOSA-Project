﻿/*
 * (c) 2011 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Simon Wollwage (rootnode) <rootnode@mosa-project.org>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mosa.Runtime.CompilerFramework.Operands;
using Mosa.Runtime.CompilerFramework.IR;
using System.Diagnostics;

namespace Mosa.Runtime.CompilerFramework
{
	/// <summary>
	/// 
	/// </summary>
	public class LeaveSSA : BaseMethodCompilerStage, IMethodCompilerStage, IPipelineStage
	{
		/// <summary>
		/// Performs stage specific processing on the compiler context.
		/// </summary>
		public void Run()
		{
			foreach (var block in this.basicBlocks)
			{
				if (block == this.FindBlock(int.MaxValue))
					continue;

				for (var context = new Context(this.instructionSet, block); !context.EndOfInstruction; context.GotoNext())
				{
					if (context.Instruction is PhiInstruction)
						this.ProcessPhiInstruction(block, context);

					for (var i = 0; i < context.OperandCount; ++i)
					{
						var op = context.GetOperand(i);
						if (op is SsaOperand)
							context.SetOperand(i, (op as SsaOperand).Operand);
					}

					for (var i = 0; i < context.ResultCount; ++i)
					{
						var op = context.GetResult(i);
						if (op is SsaOperand)
							context.SetResult(i, (op as SsaOperand).Operand);
					}
				}
			}
		}

		/// <summary>
		/// Processes the phi instruction.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="context">The context.</param>
		private void ProcessPhiInstruction(BasicBlock block, Context context)
		{
			for (var predecessorIndex = 0; predecessorIndex < block.PreviousBlocks.Count; ++predecessorIndex)
			{
				var predecessor = block.PreviousBlocks[predecessorIndex];
				var operand = context.GetOperand(predecessorIndex);

				this.InsertCopyStatement(predecessor, context.Result, operand);
			}
			context.Remove();
		}

		/// <summary>
		/// Inserts the copy statement.
		/// </summary>
		/// <param name="predecessor">The predecessor.</param>
		/// <param name="result">The result.</param>
		/// <param name="operand">The operand.</param>
		private void InsertCopyStatement(BasicBlock predecessor, Operand result, Operand operand)
		{
			var context = new Context(this.instructionSet, predecessor);
			while (!context.EndOfInstruction && IsBranchInstruction(context))
				context.GotoNext();

			if (context.Index != -1)
				context = context.InsertBefore();

			var source = operand is SsaOperand ? (operand as SsaOperand).Operand : operand;
			var destination = result is SsaOperand ? (result as SsaOperand).Operand : result;

			Debug.Assert(!(source is SsaOperand));
			Debug.Assert(!(destination is SsaOperand));

			context.SetInstruction(IR.Instruction.MoveInstruction, destination, source);
		}

		/// <summary>
		/// Determines whether [is branch instruction] [the specified context].
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		///   <c>true</c> if [is branch instruction] [the specified context]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsBranchInstruction(Context context)
		{
			return context.Instruction is BranchInstruction || context.Instruction is JmpInstruction;
		}

		/// <summary>
		/// Retrieves the name of the compilation stage.
		/// </summary>
		/// <value>
		/// The name of the compilation stage.
		/// </value>
		public string Name
		{
			get { return @"Leave Static Single Assignment Form"; }
		}
	}
}
