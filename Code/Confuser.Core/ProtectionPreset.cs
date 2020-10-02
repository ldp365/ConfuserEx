using System;

namespace Confuser.Core {
	/// <summary>
	///     Various presets of protections.
	/// </summary>
	public enum ProtectionPreset {
		/// <summary> The protection does not belong to any preset. </summary>
		None = 0,

		/// <summary> The protection provides basic security. </summary>
		Minimum = 1,

		/// <summary> The protection provides normal security for public release. </summary>
		Normal = 2,

		/// <summary> 该保护提供了更好的安全性和可观察的性能影响. </summary>
		Aggressive = 3,

		/// <summary> The protection provides strongest security with possible incompatibility. </summary>
		Maximum = 4
	}
}