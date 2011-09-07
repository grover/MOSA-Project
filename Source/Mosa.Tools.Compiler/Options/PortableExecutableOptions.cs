﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System;
using Mosa.Compiler.Framework;
using Mosa.Compiler.Options;
using Mosa.Tools.Compiler.Linker;
using NDesk.Options;

namespace Mosa.Tools.Compiler.Options
{
	/// <summary>
	/// </summary>
	public class PortableExecutableOptions : BaseCompilerOptions
	{

		public bool? SetChecksum { get; protected set; }
		public uint? FileAlignment { get; protected set; }
		public uint? SectionAlignment { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PortableExecutableOptions"/> class.
		/// </summary>
		/// <param name="optionSet">The option set.</param>
		public PortableExecutableOptions(OptionSet optionSet)
		{
			optionSet.Add(
				"pe-no-checksum",
				"Causes no checksum to be written in the generated PE file. MOSA requires the checksum to be set. It is on by default, use this switch to turn it off.",
				delegate(string value)
				{
					this.SetChecksum = false;
				}
			);

			optionSet.Add<uint>(
				"pe-file-alignment=",
				"Determines the alignment of sections within the PE file. Must be a multiple of 512 bytes.",
				delegate(uint alignment)
				{
					try
					{
						this.FileAlignment = alignment;
					}
					catch (Exception x)
					{
						throw new OptionException(@"The specified file alignment is invalid.", @"pe-file-alignment", x);
					}
				}
			);

			optionSet.Add<uint>(
				"pe-section-alignment=",
				"Determines the alignment of sections in memory. Must be a multiple of 4096 bytes.",
				delegate(uint alignment)
				{
					try
					{
						this.SectionAlignment = alignment;
					}
					catch (Exception x)
					{
						throw new OptionException(@"The specified section alignment is invalid.", @"pe-section-alignment", x);
					}
				}
			);

		}

		public void ApplyTo(PortableExecutableLinkerStage linker)
		{
			if (this.FileAlignment.HasValue)
				linker.FileAlignment = this.FileAlignment.Value;

			if (this.SectionAlignment.HasValue)
				linker.SectionAlignment = this.SectionAlignment.Value;

			if (this.SetChecksum.HasValue)
				linker.SetChecksum = this.SetChecksum.Value;
		}

		public override void Apply(IPipelineStage stage)
		{
			PortableExecutableLinkerStage linker = stage as PortableExecutableLinkerStage;
			if (linker != null)
				ApplyTo(linker);
		}

	}
}
