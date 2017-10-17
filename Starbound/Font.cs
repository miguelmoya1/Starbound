using System;
using Tao.Sdl;

public class Font {
    private IntPtr internalPointer;

    public Font(string fileName, short sizePoints) {
        Load(fileName, sizePoints);
    }

    public void Load(string fileName, short sizePoints) {
        internalPointer = SdlTtf.TTF_OpenFont(fileName, sizePoints);
        if (internalPointer == IntPtr.Zero)
            Hardware.FatalError("Font not found: " + fileName);
    }

    public IntPtr GetPointer() {
        return internalPointer;
    }
}
