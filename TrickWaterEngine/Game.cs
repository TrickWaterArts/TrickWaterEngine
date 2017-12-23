using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TrickWaterEngine {
	public class Game {

		public static Game Instance { private set; get; }

		private readonly Display display;

		static void Main(string[] args) {
			Debug.Print("TrickWaterEngine launching...");
			Instance = new Game(new Display(640, 480, "GameTest"));
			Instance.Init();
		}

		private Game(Display display) {
			this.display = display;
		}

		private void Init() {
			display.Run();
		}

		public void OnLoad() {
			TestStuff.OnLoad();
			Debug.Print("TrickWaterEngine loaded.");
		}

		public void OnUpdate(double deltaTime) {

		}

		public void OnRender(double deltaTime) {
			TestStuff.OnRender();
		}

	}

	public static class TestStuff {

		private static readonly string VERTEX_SHADER = "#version 330 core\n\nlayout (location = 0) in vec3 pos;\n\nvoid main() {\n\tgl_Position = vec4(pos, 1.0);\n}\n";
		private static readonly string FRAGMENT_SHADER = "#version 330 core\n\nout vec4 color;\n\nvoid main() {\n\tcolor = vec4(1.0, 1.0, 1.0, 1.0);\n}\n";

		private static readonly Vector3[] verts = {
			new Vector3(-0.5f, -0.5f, 0.0f),
			new Vector3(0.5f, -0.5f, 0.0f),
			new Vector3(0.0f, 0.5f, 0.0f)
		};

		private static uint vao;
		private static uint vbo;
		private static int program;

		public static void OnLoad() {
			InitShaders();
			InitVerts();
		}

		public static void OnRender() {
			GL.BindVertexArray(vao);
			GL.UseProgram(program);
			GL.DrawArrays(PrimitiveType.Triangles, 0, verts.Length);
			GL.UseProgram(0);
			GL.BindVertexArray(0);
		}

		private static void InitShaders() {
			program = GL.CreateProgram();
			int vertexShader = GL.CreateShader(ShaderType.VertexShader);
			int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

			GL.ShaderSource(vertexShader, VERTEX_SHADER);
			GL.CompileShader(vertexShader);
			string info = GL.GetShaderInfoLog(vertexShader);
			if (!string.IsNullOrEmpty(info)) {
				Debug.Print("Vertex shader failed to compile: " + info);
				return;
			}

			GL.ShaderSource(fragmentShader, FRAGMENT_SHADER);
			GL.CompileShader(fragmentShader);
			info = GL.GetShaderInfoLog(vertexShader);
			if (!string.IsNullOrEmpty(info)) {
				Debug.Print("Fragment shader failed to compile: " + info);
				return;
			}

			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragmentShader);
			GL.LinkProgram(program);
			info = GL.GetProgramInfoLog(program);
			if (!string.IsNullOrEmpty(info)) {
				Debug.Print("Program failed to link: " + info);
				return;
			}
			GL.DetachShader(program, vertexShader);
			GL.DetachShader(program, fragmentShader);
			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);
		}

		private static void InitVerts() {
			GL.GenVertexArrays(1, out vao);
			GL.BindVertexArray(vao);
			GL.GenBuffers(1, out vbo);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
			GL.BufferData(BufferTarget.ArrayBuffer, Vector3.SizeInBytes * verts.Length, verts, BufferUsageHint.StaticDraw);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindVertexArray(0);
		}

	}
}