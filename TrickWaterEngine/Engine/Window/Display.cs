using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrickWaterEngine {
	public class Display : GameWindow {

		public Display(int width, int height, string title) : base(width, height, GraphicsMode.Default, title, GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.ForwardCompatible) {
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			GL.ClearColor(Color.CornflowerBlue);
			Game.Instance.OnLoad();
		}

		protected override void OnUpdateFrame(FrameEventArgs e) {
			base.OnUpdateFrame(e);
			Game.Instance.OnUpdate(e.Time);
		}

		protected override void OnRenderFrame(FrameEventArgs e) {
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			Game.Instance.OnRender(e.Time);
			GL.Flush();
			SwapBuffers();
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			GL.Viewport(0, 0, Width, Height);
		}

	}
}