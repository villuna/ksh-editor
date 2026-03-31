using System.Numerics;
using Dear_ImGui_Sample.Backends;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace KshEditor;

public class App : GameWindow
{
    public App(int width, int height) : base(GameWindowSettings.Default, new NativeWindowSettings())
    {
        Size = new(width, height);
        VSync = VSyncMode.On;
        Title = "Ksh Editor";
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        SetupImgui();
    }

    private void SetupImgui()
    {
        ImGui.CreateContext();
        ImGui.StyleColorsDark();

        ImguiImplOpenTK4.Init(this);
        ImguiImplOpenGL3.Init();
    }

    // Tells imgui how to structure the UI. Called every frame.
    private void CreateGui()
    {
        ImGui.SetNextWindowPos(new Vector2(0, 0));
        ImGui.SetNextWindowSize(ImGui.GetIO().DisplaySize);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
        ImGui.Begin("root", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoResize);
        ImGui.PopStyleVar();

        ImGui.Text("Hi");
        if (ImGui.Button("Umazing!"))
        {
            Console.WriteLine("Our brains are umazing");
        }

        ImGui.End();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        // Create gui draw data
        ImguiImplOpenGL3.NewFrame();
        ImguiImplOpenTK4.NewFrame();
        ImGui.NewFrame();
        CreateGui();
        ImGui.Render();

        // Clear the screen
        GL.ClearColor(0, 0, 0, 1);
        GL.Clear(ClearBufferMask.None);

        // Draw the gui data to the screen
        ImguiImplOpenGL3.RenderDrawData(ImGui.GetDrawData());

        SwapBuffers();
    }
}
