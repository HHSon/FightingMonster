using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TSLibrary;
using Microsoft.Xna.Framework;
using TSLibrary.ui.Control;
using TSLibrary.ui.Control.Layout;
using TSLibrary.ui.Control.MarginType;


namespace TSLibraryTest
{
    [TestFixture]
    public class TSControlTest
    {
        TSControlConcreteClass control;
        TSLayout layout;

        [SetUp]
        public void ResetControl()
        {
            layout = new TSLayout();
            control = new TSControlConcreteClass();
            layout.ControlManager.Add(control);

            layout.Width = 800;
            layout.Height = 600;
            layout.Position = new Vector2(0, 0);

            control.MarginBottom = TSMarginType.NONE;
            control.MarginLeft = TSMarginType.NONE;
            control.MarginRight = TSMarginType.NONE;
            control.MarginTop = TSMarginType.NONE;

            control.Width = 100;
            control.Height = 20;

            control.Position = new Vector2(0, 0);
        }

        [Test]
        public void TestInitPosition()
        {
            Assert.AreEqual(control.PositionX, 0);
            Assert.AreEqual(control.PositionY, 0);

            Assert.AreEqual(control.Width, 100);    // giá trị mặc định
            Assert.AreEqual(control.Height, 20);    // giá trị mặc định

            Assert.IsTrue(control.MarginTop == TSMarginType.NONE);
            Assert.IsTrue(control.MarginBottom == TSMarginType.NONE);
            Assert.IsTrue(control.MarginLeft == TSMarginType.NONE);
            Assert.IsTrue(control.MarginRight == TSMarginType.NONE);
        }

        [Test]
        public void TestSetLeftMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Width = 100;
            control.MarginLeft = 2;

            Assert.IsTrue(control.PositionX == 2);
            Assert.IsTrue(control.Width == 100);
        }

        [Test]
        public void TestSetRightMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Width = 100;
            control.MarginRight = 2;

            Assert.IsTrue(control.PositionX == (layout.Width - 2 - 100));
            Assert.IsTrue(control.Width == 100);
        }

        [Test]
        public void TestSetBothLeftAndRightMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Width = 100;
            control.MarginLeft = 2;
            control.MarginRight = 2;

            Assert.IsTrue(control.PositionX == 2);
            Assert.IsTrue(control.Width == (800 - 2 - 2));
        }

        [Test]
        public void TestSetTopMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Height = 100;
            control.MarginTop = 2;

            Assert.IsTrue(control.PositionY == 2);
            Assert.IsTrue(control.Height == 100);
        }

        [Test]
        public void TestSetBottomMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Height = 100;
            control.MarginBottom = 2;

            Assert.IsTrue(control.PositionY == (layout.Height - 2 - 100));
            Assert.IsTrue(control.Height == 100);
        }

        [Test]
        public void TestSetBothTopAndBottomMargin()
        {
            control.Position = new Vector2(5, 5);
            control.Height = 20;
            control.MarginTop = 2;
            control.MarginBottom = 2;

            Assert.IsTrue(control.PositionY == 2);
            Assert.AreEqual(control.Height, (600 - 2 - 2));
        }

        [Test]
        public void TestSetLeftMarginCenter()
        {
            control.MarginLeft = TSMarginType.CENTER;
            control.MarginTop = 10;
            control.Width = 100;

            Assert.AreEqual(control.Position.X, layout.Width / 2 - control.Width / 2);
        }

        [Test]
        public void TestSetRightMarginCenter()
        {
            control.MarginRight = TSMarginType.CENTER;
            control.MarginTop = 10;
            control.Width = 100;

            Assert.AreEqual(control.Position.X, layout.Width / 2 - control.Width / 2);
        }

        [Test]
        public void TestSetTopMarginCenter()
        {
            control.MarginTop = TSMarginType.CENTER;
            control.MarginLeft = 10;
            control.Height = 100;

            Assert.AreEqual(control.Position.Y, (layout.Height / 2 - control.Height / 2));
        }

        [Test]
        public void TestSetBottomMarginCenter()
        {
            control.MarginBottom = TSMarginType.CENTER;
            control.MarginLeft = 10;
            control.Height = 100;

            Assert.AreEqual(control.Position.Y, (layout.Height / 2 - control.Height / 2));
        }

        [Test]
        public void TestIsMouseHover()
        {
            control.Position = new Vector2(5, 5);
            control.Width = 20;
            control.Height = 20;

            Assert.IsTrue(control.isMouseHover(5, 24));
            Assert.IsTrue(control.isMouseHover(24, 5));
            Assert.IsTrue(control.isMouseHover(25, 24));
            Assert.IsTrue(control.isMouseHover(24, 25));
            Assert.IsTrue(control.isMouseHover(10, 10));
            Assert.IsTrue(control.isMouseHover(4, 5) == false);
            Assert.IsTrue(control.isMouseHover(26, 5) == false);
            Assert.IsTrue(control.isMouseHover(5, 4) == false);
            Assert.IsTrue(control.isMouseHover(5, 26) == false);
        }

        [Test]
        public void TestPositionOnScreenWhenAddToLayout()
        {
            TSLayout layout = new TSLayout();
            layout.Position = new Vector2(1, 2);

            TSControlConcreteClass c = new TSControlConcreteClass();
            layout.ControlManager.Add(c);

            Assert.AreEqual(c.PositionOnScreenX, 1);
            Assert.AreEqual(c.PositionOnScreenY, 2);
        }

        [Test]
        public void TestControlPositionOnScreenWhenChangeParentPosition()
        {
            layout.Position = new Vector2(1, 2);
            Assert.AreEqual(control.PositionOnScreenX, 1);
            Assert.AreEqual(control.PositionOnScreenY, 2);
        }

        [Test]
        public void TestControlPositionOnScreenWhenChangePosition()
        {
            layout.Position = new Vector2(1, 2);
            control.Position = new Vector2(3, 4);
            Assert.AreEqual(control.PositionOnScreenX, 4);
            Assert.AreEqual(control.PositionOnScreenY, 6);
        }

        [Test]
        public void TestControlPositionOnScreenWhenChangeLeftMargin()
        {
            control.MarginLeft = 10;
            layout.Position = new Vector2(1, 2);
            Assert.AreEqual(control.PositionOnScreenX, 11);
        }

        [Test]
        public void TestControlPositionOnScreenWhenChangeTopMargin()
        {
            control.MarginTop = 10;
            layout.Position = new Vector2(1, 2);
            Assert.AreEqual(control.PositionOnScreenY, 12);
        }

        class TSControlConcreteClass : TSControl
        {
            public TSControlConcreteClass()
                : base()
            {
            }
        }
    }
}