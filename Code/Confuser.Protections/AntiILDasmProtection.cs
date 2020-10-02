﻿using System;
using System.Linq;
using Confuser.Core;
using dnlib.DotNet;

namespace Confuser.Protections {
	internal class AntiILDasmProtection : Protection {
		public const string _Id = "anti ildasm";
		public const string _FullId = "Ki.AntiILDasm";

		public override string Name {
			get { return "防ILDASM"; }
		}

		public override string Description {
			get { return "此保护使用阻止ILDasm反汇编它的属性标记模块。"; }
		}

		public override string Id {
			get { return _Id; }
		}

		public override string FullId {
			get { return _FullId; }
		}

		public override ProtectionPreset Preset {
			get { return ProtectionPreset.Minimum; }
		}

		protected override void Initialize(ConfuserContext context) {
			//
		}

		protected override void PopulatePipeline(ProtectionPipeline pipeline) {
			pipeline.InsertPreStage(PipelineStage.ProcessModule, new AntiILDasmPhase(this));
		}

		class AntiILDasmPhase : ProtectionPhase {
			public AntiILDasmPhase(AntiILDasmProtection parent)
				: base(parent) { }

			public override ProtectionTargets Targets {
				get { return ProtectionTargets.Modules; }
			}

			public override string Name {
				get { return "Anti-ILDasm marking"; }
			}

			protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
				foreach (ModuleDef module in parameters.Targets.OfType<ModuleDef>()) {
					TypeRef attrRef = module.CorLibTypes.GetTypeRef("System.Runtime.CompilerServices", "SuppressIldasmAttribute");
					var ctorRef = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRef);

					var attr = new CustomAttribute(ctorRef);
					module.CustomAttributes.Add(attr);
				}
			}
		}
	}
}