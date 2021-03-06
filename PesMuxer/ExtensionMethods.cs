﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluraySharp.Common.BdStandardPart;
using System.IO;
using System.Diagnostics;

namespace PesMuxer
{
	static class ExtensionMethods
	{
		[Conditional("DEBUG")]
		public static void AssertNotNull(this object argument)
		{
			if (object.ReferenceEquals(argument, null))
			{
				throw new NullReferenceException();
			}
		}

		public static T AssertNotNull<T>(this T argument, string paramName)
		{
			if(object.ReferenceEquals(argument, null))
			{
				throw new ArgumentNullException(paramName);
			}

			return argument;
		}

		public static uint ToBdTimeValue(this TimeSpan span)
		{
			span.AssertNotNull();
			return BdTime.TicksToTimeValue(span.Ticks);
		}

		public static void SafeCreate(this DirectoryInfo dirInfo, string paramName)
		{
			dirInfo.AssertNotNull(paramName).Create();
			dirInfo.AssertExists();
		}

		public static void AssertExists(this FileSystemInfo dirInfo)
		{
			dirInfo.AssertNotNull();
			if (!dirInfo.Exists)
			{
				throw new FileNotFoundException(dirInfo.FullName);
			}
		}

		public static DirectoryInfo NavigateTo(this DirectoryInfo dir, string path)
		{
			dir.AssertNotNull();
			var tPath = Path.Combine(dir.FullName, path);
			return new DirectoryInfo(tPath);
		}

		public static FileInfo PickFile(this DirectoryInfo dir, string path)
		{
			dir.AssertNotNull();
			var tPath = Path.Combine(dir.FullName, path);
			return new FileInfo(tPath);
		}
	}
}
