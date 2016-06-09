using System;
using System.IO;
using System.Threading;
using Tao.Sdl;

public class Hardware
{
    static IntPtr hiddenScreen;
    static short width, height;
    static bool isThereJoystick;
    static IntPtr joystick;
    static short startX, startY; // For Scroll


    public static void Init(short w, short h, int colors, bool fullScreen)
    {
        width = w;
        height = h;

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags |= Sdl.SDL_FULLSCREEN;
        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        hiddenScreen = Sdl.SDL_SetVideoMode(
          width,
          height,
          colors,
          flags);

        Sdl.SDL_Rect rect2 =
          new Sdl.SDL_Rect(0, 0, (short)width, (short)height);
        Sdl.SDL_SetClipRect(hiddenScreen, ref rect2);

        SdlTtf.TTF_Init();

        // Joystick initialization
        isThereJoystick = true;
        if (Sdl.SDL_NumJoysticks() < 1)
            isThereJoystick = false;

        if (isThereJoystick)
        {
            joystick = Sdl.SDL_JoystickOpen(0);
            if (joystick == IntPtr.Zero)
                isThereJoystick = false;
        }

        if (SdlMixer.Mix_OpenAudio(22050,
            unchecked(Sdl.AUDIO_S16LSB), 2, 1024) == -1)
            FatalError("No se ha podido inicializar el Sonido");
    }

    public static void ClearScreen()
    {
        Sdl.SDL_Rect origin = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_FillRect(hiddenScreen, ref origin, 0);
    }

    public static void DrawHiddenImage(Image image, int x, int y)
    {
        drawHiddenImage(image.GetPointer(), x + startX, y + startY);
    }

    public static void WriteHiddenText(string txt,
        short x, short y, byte r, byte g, byte b, Font f)
    {
        Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
        IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(
            f.GetPointer(), txt, color);
        //if (textoComoImagen == IntPtr.Zero)
        //   Environment.Exit(5);

        Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(
            (short)(x + startX), (short)(y + startY),
            width, height);

        Sdl.SDL_BlitSurface(textoComoImagen, ref origen,
            hiddenScreen, ref dest);

        Sdl.SDL_FreeSurface(textoComoImagen);
    }

    public static void ShowHiddenScreen()
    {
        Sdl.SDL_Flip(hiddenScreen);
    }

    public static bool KeyPressed(int c)
    {
        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event myEvent;
        Sdl.SDL_PollEvent(out myEvent);
        int numkeys;
        byte[] keys = Sdl.SDL_GetKeyState(out numkeys);
        if (keys[c] == 1)
            pressed = true;
        return pressed;
    }

    public static void Pause(int milisegundos)
    {
        Thread.Sleep(milisegundos);
    }

    public static int GetWidth()
    {
        return width;
    }

    public static int GetHeight()
    {
        return height;
    }

    public static void FatalError(string text)
    {
        StreamWriter sw = File.AppendText("errors.log");
        sw.WriteLine(text);
        sw.Close();
        Console.WriteLine(text);
        Environment.Exit(1);
    }
    // Scroll Methods

    public static void ResetScroll()
    {
        startX = startY = 0;
    }

    public static void ScrollTo(short newStartX, short newStartY)
    {
        startX = newStartX;
        startY = newStartY;
    }

    public static void ScrollHorizontally(short xDespl)
    {
        startX += xDespl;
    }

    public static void ScrollVertically(short yDespl)
    {
        startY += yDespl;
    }

    // Private (auxiliar) methods

    private static void drawHiddenImage(IntPtr image, int x, int y)
    {
        Sdl.SDL_Rect origin = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect((short)x, (short)y,
          width, height);
        Sdl.SDL_BlitSurface(image, ref origin, hiddenScreen, ref dest);
    }

    // Alternate key definitions

    public static int KEY_ESC = Sdl.SDLK_ESCAPE;
    public static int KEY_SPC = Sdl.SDLK_SPACE;
    public static int KEY_A = Sdl.SDLK_a;
    public static int KEY_B = Sdl.SDLK_b;
    public static int KEY_C = Sdl.SDLK_c;
    public static int KEY_D = Sdl.SDLK_d;
    public static int KEY_E = Sdl.SDLK_e;
    public static int KEY_F = Sdl.SDLK_f;
    public static int KEY_G = Sdl.SDLK_g;
    public static int KEY_H = Sdl.SDLK_h;
    public static int KEY_I = Sdl.SDLK_i;
    public static int KEY_J = Sdl.SDLK_j;
    public static int KEY_K = Sdl.SDLK_k;
    public static int KEY_L = Sdl.SDLK_l;
    public static int KEY_M = Sdl.SDLK_m;
    public static int KEY_N = Sdl.SDLK_n;
    public static int KEY_O = Sdl.SDLK_o;
    public static int KEY_P = Sdl.SDLK_p;
    public static int KEY_Q = Sdl.SDLK_q;
    public static int KEY_R = Sdl.SDLK_r;
    public static int KEY_S = Sdl.SDLK_s;
    public static int KEY_T = Sdl.SDLK_t;
    public static int KEY_U = Sdl.SDLK_u;
    public static int KEY_V = Sdl.SDLK_v;
    public static int KEY_W = Sdl.SDLK_w;
    public static int KEY_X = Sdl.SDLK_x;
    public static int KEY_Y = Sdl.SDLK_y;
    public static int KEY_Z = Sdl.SDLK_z;
    public static int KEY_1 = Sdl.SDLK_1;
    public static int KEY_2 = Sdl.SDLK_2;
    public static int KEY_3 = Sdl.SDLK_3;
    public static int KEY_4 = Sdl.SDLK_4;
    public static int KEY_5 = Sdl.SDLK_5;
    public static int KEY_6 = Sdl.SDLK_6;
    public static int KEY_7 = Sdl.SDLK_7;
    public static int KEY_8 = Sdl.SDLK_8;
    public static int KEY_9 = Sdl.SDLK_9;
    public static int KEY_0 = Sdl.SDLK_0;
    public static int KEY_UP = Sdl.SDLK_UP;
    public static int KEY_DOWN = Sdl.SDLK_DOWN;
    public static int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public static int KEY_LEFT = Sdl.SDLK_LEFT;
    public static int KEY_RETURN = Sdl.SDLK_RETURN;

    // Mouse
    public static int GetMouse(out int x, out int y)
    {
        return Sdl.SDL_GetMouseState(out x, out y);
    }

} /* End of class SdlHardware */
