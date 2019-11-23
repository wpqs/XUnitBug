using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MxConsoleLib;

namespace MSUnitBugLibTest
{
    [TestClass]
    public class MxConsolePropertiesTest
    {
        [TestMethod]
        public void NoParamTest()
        {
            var props = new MxConsoleProperties();
            Assert.IsFalse(props.IsError());
        }

        [TestMethod]
        public void ValidateDefaultTest()
        {
            var props = new MxConsoleProperties();
            Assert.IsTrue(props.Validate());
        }

        [TestMethod]
        public void GetValidationErrorTest()
        {
            var props = new MxConsoleProperties();
            Assert.IsNull(props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorTitleTest()
        {
            var props = new MxConsoleProperties();
            props.Title = null;
            Assert.AreEqual($"Title is null", props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorBufferHeightTest()
        {
            var props = new MxConsoleProperties();
            props.BufferHeight = -1;
            Assert.AreEqual($"BufferHeight={props.BufferHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop}, WindowHeight={MxConsoleProperties.DefaultWindowHeight})", props.GetValidationError());
            props.BufferHeight = Int16.MaxValue;
            Assert.AreEqual($"BufferHeight={props.BufferHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop}, WindowHeight={MxConsoleProperties.DefaultWindowHeight})", props.GetValidationError());
            props.BufferHeight = props.WindowTop + props.WindowHeight - 1;
            Assert.AreEqual($"BufferHeight={props.BufferHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop}, WindowHeight={MxConsoleProperties.DefaultWindowHeight})", props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorBufferWidthTest()
        {
            var props = new MxConsoleProperties();
            props.BufferWidth = -1;
            Assert.AreEqual($"BufferWidth={ props.BufferWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft}, WindowWidth={MxConsoleProperties.DefaultWindowWidth})", props.GetValidationError());
            props.BufferWidth = Int16.MaxValue;
            Assert.AreEqual($"BufferWidth={ props.BufferWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft}, WindowWidth={MxConsoleProperties.DefaultWindowWidth})", props.GetValidationError());
            props.BufferWidth = props.WindowLeft + props.WindowWidth - 1;
            Assert.AreEqual($"BufferWidth={ props.BufferWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft}, WindowWidth={MxConsoleProperties.DefaultWindowWidth})", props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorWindowHeightTest()
        {
            var props = new MxConsoleProperties();
            props.WindowHeight = -1;
            Assert.AreEqual($"WindowHeight={props.WindowHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop})", props.GetValidationError());
            //caught by BufferHeight test
            //props.WindowHeight = Int16.MaxValue; 
            //Assert.Equal($"error WindowHeight={props.WindowHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop})", props.GetValidationError());
            //props.WindowHeight = Console.LargestWindowHeight + 1;
            //Assert.Equal($"error WindowHeight={props.WindowHeight} is out of range (WindowTop={MxConsoleProperties.DefaultWindowTop})", props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorWindowWidthTest()
        {
            var props = new MxConsoleProperties();
            props.WindowWidth = -1;
            Assert.AreEqual($"WindowWidth={props.WindowWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft})", props.GetValidationError());
            //caught by BufferWidth test 
            //props.WindowWidth = Int16.MaxValue;
            //Assert.Equal($"error WindowWidth={props.WindowWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft})", props.GetValidationError());
            //props.WindowWidth = Console.LargestWindowWidth;
            //Assert.Equal($"error WindowWidth={props.WindowWidth} is out of range (WindowLeft={MxConsoleProperties.DefaultWindowLeft})", props.GetValidationError());
        }

        [TestMethod]
        public void GetValidationErrorWindowTopTest()
        {
            var props = new MxConsoleProperties();
            props.WindowTop = -1;
            Assert.AreEqual($"WindowTop={props.WindowTop} is less than zero", props.GetValidationError());
            props.WindowHeight = 50;
            props.BufferHeight = props.WindowHeight;
            props.WindowTop = 0;    //ok to display line 50 of buffer in bottom line of window
            Assert.IsNull(props.GetValidationError());
            props.WindowTop = 1;    //attempting to display line 51 of buffer in bottom line of window - it doesn't exist!
         //   Assert.StartsWith($"BufferHeight={props.BufferHeight} is out of range (WindowTop={props.WindowTop}, WindowHeight={props.WindowHeight})", props.GetValidationError());
        }
        [TestMethod]
        public void GetValidationErrorWindowLeftTest()
        {
            var props = new MxConsoleProperties();
            props.WindowLeft = -1;
            Assert.AreEqual($"WindowLeft={props.WindowLeft} is less than zero", props.GetValidationError());

            props.WindowWidth = 120;
            props.BufferWidth = props.WindowWidth;
            props.WindowLeft = 0;  //ok to display column 120 of buffer in RHS of window
            Assert.IsNull(props.GetValidationError());
            props.WindowLeft = 1;  //attempting to display column 121 of buffer in right most column of window - it doesn't exist! 
            Assert.AreEqual($"BufferWidth={props.BufferWidth} is out of range (WindowLeft={props.WindowLeft}, WindowWidth={props.WindowWidth})", props.GetValidationError());

        }
        [TestMethod]
        public void GetValidationErrorCursorSizeTest()
        {
            var props = new MxConsoleProperties();
            props.CursorSize = 0;
            Assert.AreEqual($"CursorSize={props.CursorSize} is out of range 1-100", props.GetValidationError());
            props.CursorSize = 101;
            Assert.AreEqual($"CursorSize={props.CursorSize} is out of range 1-100", props.GetValidationError());

        }
        [TestMethod]
        public void GetValidationErrorCursorTopTest()
        {
            var props = new MxConsoleProperties();
            props.CursorTop = -1;
            Assert.AreEqual($"CursorTop={props.CursorTop} is out of range (BufferHeight={MxConsoleProperties.DefaultBufferHeight})", props.GetValidationError());
            props.CursorTop = props.BufferHeight;
            Assert.AreEqual($"CursorTop={props.CursorTop} is out of range (BufferHeight={MxConsoleProperties.DefaultBufferHeight})", props.GetValidationError());

        }
        [TestMethod]
        public void GetValidationErrorCursorLeftTest()
        {
            var props = new MxConsoleProperties();
            props.CursorLeft = -1;
            Assert.AreEqual($"CursorLeft={props.CursorLeft} is out of range (BufferWidth={MxConsoleProperties.DefaultBufferWidth})", props.GetValidationError());
            props.CursorLeft = props.BufferWidth;
            Assert.AreEqual($"CursorLeft={props.CursorLeft} is out of range (BufferWidth={MxConsoleProperties.DefaultBufferWidth})", props.GetValidationError());

        }
    }
}
