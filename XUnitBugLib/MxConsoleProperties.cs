using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using MxStdUtilsLib;

namespace MxConsoleLib
{
    [SuppressMessage("ReSharper", "SimplifyConditionalTernaryExpression")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public class MxConsoleProperties
    {
        public const string Black = "black";
        public const string Blue = "blue";
        public const string Cyan = "cyan";
        public const string DarkBlue = "darkblue";
        public const string DarkCyan = "darkcyan";
        public const string DarkGray = "darkgray";
        public const string DarkGreen = "darkgreen";
        public const string DarkMagenta = "darkmagenta";
        public const string DarkRed = "darkred";
        public const string DarkYellow = "darkyellow";
        public const string Gray = "gray";
        public const string Green = "green";
        public const string Magenta = "magenta";
        public const string Red = "red";
        public const string White = "white";
        public const string Yellow = "yellow";

        public enum Color
        {
            [EnumMember(Value = MxConsoleProperties.Black)] Black = ConsoleColor.Black,
            [EnumMember(Value = MxConsoleProperties.DarkBlue)] DarkBlue = ConsoleColor.DarkBlue,
            [EnumMember(Value = MxConsoleProperties.DarkGreen)] DarkGreen = ConsoleColor.DarkGreen,
            [EnumMember(Value = MxConsoleProperties.DarkCyan)] DarkCyan = ConsoleColor.DarkCyan,
            [EnumMember(Value = MxConsoleProperties.DarkRed)] DarkRed = ConsoleColor.DarkRed,
            [EnumMember(Value = MxConsoleProperties.DarkMagenta)] DarkMagenta = ConsoleColor.DarkMagenta,
            [EnumMember(Value = MxConsoleProperties.DarkYellow)] DarkYellow = ConsoleColor.DarkYellow,
            [EnumMember(Value = MxConsoleProperties.Gray)] Gray = ConsoleColor.Gray,
            [EnumMember(Value = MxConsoleProperties.DarkGray)] DarkGray = ConsoleColor.DarkGray,
            [EnumMember(Value = MxConsoleProperties.Blue)] Blue = ConsoleColor.Blue,
            [EnumMember(Value = MxConsoleProperties.Green)] Green = ConsoleColor.Green,
            [EnumMember(Value = MxConsoleProperties.Cyan)] Cyan = ConsoleColor.Cyan,
            [EnumMember(Value = MxConsoleProperties.Red)] Red = ConsoleColor.Red,
            [EnumMember(Value = MxConsoleProperties.Magenta)] Magenta = ConsoleColor.Magenta,
            [EnumMember(Value = MxConsoleProperties.Yellow)] Yellow = ConsoleColor.Yellow,
            [EnumMember(Value = MxConsoleProperties.White)] White = ConsoleColor.White,
            [EnumMember(Value = MxStdUtils.ValueNotSet)] NotSet = MxStdUtils.PosIntegerNotSet,
            [EnumMember(Value = MxStdUtils.ValueUnknown)] Unknown = MxStdUtils.PosIntegerError
        }

        public const string DefaultTitle = MxStdUtils.ValueNotSet;

        public const int DefaultBufferHeight = 100;
        public const int DefaultBufferWidth = 120;
        public const int DefaultWindowHeight = 30;
        public const int DefaultWindowWidth = 120;
        public const int DefaultCursorSize = 25;
        public const int DefaultWindowTop = 0;
        public const int DefaultWindowLeft = 0;
        public const int DefaultCursorTop = 0;
        public const int DefaultCursorLeft = 0;
        public const MxConsoleProperties.Color DefaultForegroundColor = MxConsoleProperties.Color.Gray;
        public const MxConsoleProperties.Color DefaultBackgroundColor = MxConsoleProperties.Color.Black;

        public string Title { set; get; }
        public int BufferHeight {set; get; }
        public int BufferWidth  { set; get; }
        public int WindowHeight  {set; get; }
        public int WindowWidth  {set; get; }
        public int CursorSize  { set; get; }
        public int WindowTop  {  set; get; }
        public int WindowLeft  { set; get; }
        public int CursorTop  {  set; get; }
        public int CursorLeft  {  set; get; }
        public MxConsoleProperties.Color ForegroundColor  { set; get; }
        public MxConsoleProperties.Color BackgroundColor  { set; get; }

        public bool TreatCtrlCAsInput { set; get; }

        private bool Error { set; get; }

        public bool IsError(){ return Error;}

        public MxConsoleProperties()
        {
            Error = SetDefaults() ? false : true;
        }

        public bool SetDefaults()
        {
            Title = DefaultTitle;
            BufferHeight = DefaultBufferHeight;
            BufferWidth = DefaultBufferWidth;
            WindowHeight = DefaultWindowHeight;
            WindowWidth = DefaultWindowWidth;
            CursorSize = DefaultCursorSize;
            WindowTop = DefaultWindowTop;
            WindowLeft = DefaultWindowLeft;
            CursorTop = DefaultCursorTop;
            CursorLeft = DefaultCursorLeft;
            ForegroundColor = DefaultForegroundColor;
            BackgroundColor = DefaultBackgroundColor;
            TreatCtrlCAsInput = true;

            return Validate();
        }

        public bool Validate()
        {
            Error = true;

            var result = GetValidationError();
            if (result == null)
                Error = false;

            return (Error) ? false : true;
        }

        public static string GetSettingsError(string argRowsName, int argRowsValue, int windowSpacingHeight, string argColsName, int argColsValue, int windowSpacingWidth)
        {
            string rc = null;

            if ((windowSpacingHeight+argRowsValue+1) > Console.LargestWindowHeight)
                rc = $"'{argRowsName}={argRowsValue}' is invalid on this machine; max value is {Console.LargestWindowHeight-windowSpacingHeight-1}";
            else if ((windowSpacingWidth+argColsValue+1) > Console.LargestWindowWidth)
                rc = $"'{argColsName}={argColsValue}' is invalid on this machine; max value is { Console.LargestWindowWidth-windowSpacingWidth-1}";
            else
                rc = null;

            return rc;
        }

        public string GetValidationError()
        {
            // ReSharper disable once RedundantAssignment
            var rc = MxStdUtils.ValueUnknown;

            if (Title == null)
                rc = $"Title is null";
            else
            {
                if ((BufferHeight < 0) || (BufferHeight >= Int16.MaxValue) || (BufferHeight < (WindowTop + WindowHeight)))
                    rc = $"BufferHeight={BufferHeight} is out of range (WindowTop={WindowTop}, WindowHeight={WindowHeight})";
                else
                {
                    if ((BufferWidth < 0) || (BufferWidth >= Int16.MaxValue) || (BufferWidth < (WindowLeft + WindowWidth)))
                        rc = $"BufferWidth={BufferWidth} is out of range (WindowLeft={WindowLeft}, WindowWidth={WindowWidth})";
                    else
                    {
                        if ((WindowHeight < 0) || ((WindowHeight + WindowTop) >= Int16.MaxValue) || (WindowHeight > Console.LargestWindowHeight))
                            rc = $"WindowHeight={WindowHeight} is out of range (WindowTop={WindowTop})";
                        else
                        {
                            if ((WindowWidth < 0) || ((WindowWidth + WindowLeft) >= Int16.MaxValue) || (WindowWidth > Console.LargestWindowWidth))
                                rc = $"WindowWidth={WindowWidth} is out of range (WindowLeft={WindowLeft})";
                            else
                            {
                                if (WindowTop < 0) // || ((WindowTop + WindowHeight) > BufferHeight)) - checked by prior BufferHeight test
                                    rc = $"WindowTop={WindowTop} is less than zero";
                                else
                                {
                                    if (WindowLeft < 0) // || ((WindowLeft + WindowWidth) > BufferWidth)) - checked by prior BufferWidth test
                                        rc = $"WindowLeft={WindowLeft} is less than zero";
                                    else
                                    {
                                        if ((CursorSize <= 0) || (CursorSize > 100))
                                            rc = $"CursorSize={CursorSize} is out of range 1-100";
                                        else
                                        {
                                            if ((CursorTop < 0) || (CursorTop >= BufferHeight))
                                                rc = $"CursorTop={CursorTop} is out of range (BufferHeight={BufferHeight})";
                                            else
                                            {
                                                if ((CursorLeft < 0) || (CursorLeft >= BufferWidth))
                                                    rc = $"CursorLeft={CursorLeft} is out of range (BufferWidth={BufferWidth})";
                                                else
                                                {
                                                    rc = null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return rc;
        }
    }
}
