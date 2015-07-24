﻿/*
    Copyright (C) 2014-2015 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;

namespace dnSpy.AsmEditor.DnlibDialogs {
	sealed class CustomAttributeOptions {
		public byte[] RawData;
		public ICustomAttributeType Constructor;
		public List<CAArgument> ConstructorArguments = new List<CAArgument>();
		public List<CANamedArgument> NamedArguments = new List<CANamedArgument>();

		public CustomAttributeOptions() {
		}

		public CustomAttributeOptions(CustomAttribute ca) {
			this.RawData = ca.RawData;
			this.Constructor = ca.Constructor;
			this.ConstructorArguments.AddRange(ca.ConstructorArguments.Select(a => a.Clone()));
			this.NamedArguments.AddRange(ca.NamedArguments.Select(a => a.Clone()));
		}

		public CustomAttribute Create() {
			if (RawData != null)
				return new CustomAttribute(Constructor, RawData);
			return new CustomAttribute(Constructor, ConstructorArguments, NamedArguments);
		}
	}
}
