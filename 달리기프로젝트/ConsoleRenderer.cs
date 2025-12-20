using System;
using System.Runtime.InteropServices;
using System.Text;

public class ConsoleRenderer : IDisposable
{
    #region Win32 API Definitions
    [StructLayout(LayoutKind.Sequential)]
    public struct COORD { public short X; public short Y; }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_CURSOR_INFO { public uint dwSize; public bool bVisible; }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr CreateConsoleScreenBuffer(uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwFlags, IntPtr lpScreenBufferData);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    const uint GENERIC_READ = 0x80000000;
    const uint GENERIC_WRITE = 0x40000000;
    const uint CONSOLE_TEXTMODE_BUFFER = 1;
    #endregion

    private IntPtr[] _hScreen = new IntPtr[2];
    private int _screenIndex = 0;
    private readonly int _width;
    private readonly int _height;

    public ConsoleRenderer(int width = 80, int height = 50)
    {
        _width = width;
        _height = height;

        // 콘솔 창 크기 설정
        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width, height);

        for (int i = 0; i < 2; i++)
        {
            _hScreen[i] = CreateConsoleScreenBuffer(GENERIC_READ | GENERIC_WRITE, 0, IntPtr.Zero, CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);

            // 커서 숨기기
            var cci = new CONSOLE_CURSOR_INFO { dwSize = 1, bVisible = false };
            SetConsoleCursorInfo(_hScreen[i], ref cci);
        }
    }

    public void Flipping()
    {
        SetConsoleActiveScreenBuffer(_hScreen[_screenIndex]);
        _screenIndex = (_screenIndex == 0) ? 1 : 0;
    }

    public void Clear()
    {
        COORD coord = new COORD { X = 0, Y = 0 };
        uint written;
        FillConsoleOutputCharacter(_hScreen[_screenIndex], ' ', (uint)(_width * _height), coord, out written);
    }

    public void Print(int x, int y, string str)
    {
        COORD pos = new COORD { X = (short)x, Y = (short)y };
        SetConsoleCursorPosition(_hScreen[_screenIndex], pos);

        byte[] buffer = Encoding.Default.GetBytes(str);
        uint written;
        WriteFile(_hScreen[_screenIndex], buffer, (uint)buffer.Length, out written, IntPtr.Zero);
    }

    public void Dispose()
    {
        CloseHandle(_hScreen[0]);
        CloseHandle(_hScreen[1]);
    }
}
