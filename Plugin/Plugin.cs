using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using ResoniteImGuiLib;
using ImGuiNET;
using FrooxEngine;
using Elements.Core;

namespace ImGuiExample;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency(ResoniteImGuiLib.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log = null!;

    public override void Load()
    {
        Log = base.Log;
        
        string msg = "Hello World";
        bool show = true;
        bool demo = false;
        ImGuiLib.GetOrCreateInstance().Layout += () =>
        {
            ImGui.Begin("ImGuiExample Window");
            ImGui.InputText("Message Text", ref msg, 1000);
            ImGui.Checkbox("Show to Others", ref show);
            if (ImGui.Button("Show message"))
            {
                var world = show ? Engine.Current.WorldManager.FocusedWorld : Userspace.UserspaceWorld;
                var localUserRoot = world.LocalUser.Root;
                if (world != null && localUserRoot != null)
                    NotificationMessage.SpawnTextMessage(world,
                        localUserRoot.ViewPosition + (localUserRoot.ViewRotation * float3.Forward * 0.25f * localUserRoot.GlobalScale),
                        msg,
                        RandomX.Hue,
                        size: 0.35f*localUserRoot.GlobalScale,
                        floatUpDistance: 0.05f * localUserRoot.GlobalScale);

            }
            ImGui.Separator();
            ImGui.Checkbox("Show Demo Window", ref demo);
            ImGui.End();

            if (demo)
            {
                ImGui.ShowDemoWindow();
            }
        };
        
        ImGuiLib.GetOrCreateInstance("custom", (window, isNew) => {
            if(isNew) window.ClearColor = new color(0.13f, 0.86f, 0.53f, 1f);
        }).Layout += () =>
        {
            ImGui.Begin("ImGuiExample Window");
            ImGui.Text("This window has a different background to the other ones!");
            ImGui.End();
        };
    }
}
