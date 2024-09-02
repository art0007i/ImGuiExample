using ResoniteModLoader;
using ImGuiNET;
using FrooxEngine;
using Elements.Core;
using ResoniteImGuiLib;

namespace ImGuiExample;

public class ImGuiExample : ResoniteMod
{
    public override string Name => "ImGuiExample";
    public override string Author => "art0007i";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/art0007i/ImGuiExample/";
    public override void OnEngineInit()
    {
        string msg = "Hello World";
        bool show = true;
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
            ImGui.End();
        };
    }
}
