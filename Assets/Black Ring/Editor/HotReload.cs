using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

public static class HotReloadInvoker {
	[UnityEditor.Callbacks.DidReloadScripts]
	static void OnHotReload() {
		if (Application.isPlaying) {
			ExecuteHotReloadMethods();
		}
	}

	static void ExecuteHotReloadMethods() {
		bool IsInvokableType(Type t) {
			return t.IsClass
				&& !t.IsAbstract
				&& (t.IsSubclassOf(typeof(MonoBehaviour)) || t.IsSubclassOf(typeof(ScriptableObject)))
				&& Attribute.IsDefined(t, typeof(HotReloadInvokableAttribute), false);
		}

		MethodInfo GetOnHotReload(Type type) {
			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			return type.GetMethod("OnHotReload", flags, null, CallingConventions.HasThis, new Type[]{}, null);
		}

		System.Object[] hotReloadMethodParams = new System.Object[]{};

		foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(IsInvokableType)) {
			MethodInfo hotReloadMethod = GetOnHotReload(type);
			if (hotReloadMethod != null) {
				foreach (UnityEngine.Object component in UnityEngine.Object.FindObjectsOfType(type, false)) {
					hotReloadMethod.Invoke(component, hotReloadMethodParams);
				}
			} else {
				Debug.LogError("Class " + type.ToString() + " uses the HotReloadInvokable attribute but does not have an OnHotReload method");
			}
        }
	}
}
