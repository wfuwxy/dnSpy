﻿
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using ICSharpCode.ILSpy.TreeNodes;

namespace ICSharpCode.ILSpy.Commands {
	[ExportContextMenuEntryAttribute(Header = "_Show in Explorer")]
	class OpenInExplorerCommand : IContextMenuEntry
	{
		public string GetMenuHeader(TextViewContext context)
		{
			return null;
		}

		public bool IsVisible(TextViewContext context)
		{
			return context.SelectedTreeNodes != null &&
				context.SelectedTreeNodes.Length == 1 &&
				context.SelectedTreeNodes[0] is AssemblyTreeNode;
		}

		public bool IsEnabled(TextViewContext context)
		{
			return IsVisible(context);
		}

		public void Execute(TextViewContext context)
		{
			// Known problem: explorer can't show files in the .NET 2.0 GAC.
			var asmNode = (AssemblyTreeNode)context.SelectedTreeNodes[0];
			var filename = asmNode.LoadedAssembly.FileName;
			var args = string.Format("/select,{0}", filename);
			try {
				Process.Start(new ProcessStartInfo("explorer.exe", args));
			}
			catch (IOException) {
			}
			catch (Win32Exception) {
			}
		}
	}
}