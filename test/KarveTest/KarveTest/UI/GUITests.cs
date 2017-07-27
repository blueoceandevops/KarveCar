﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DataAccessLayer;
using KarveCommon.Generic;
using NUnit.Framework;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.UIItems.WPFUIItems;
using Application = TestStack.White.Application;
using Window = TestStack.White.UIItems.WindowItems.Window;

namespace KarveTest.UI
{
    [TestFixture]
    public class GUITests
    {

        private string applicationPath = @"C:\Users\Usuario\Documents\KarveSnapshots\KarveCar\src\bin\Debug\";

        [OneTimeSetUp]
        public void ToolBarTest()
        {
            applicationPath = Path.Combine(applicationPath, "KarveCar.exe");

        }
        [OneTimeTearDown]
        public void GUITestsTearDown() 
        {
        }

        [Test]
        public void Should_Start_and_Close_Correctly()
        {

            Application application = Application.Launch(applicationPath);
            application.Close();
        }

        [Test]
        public void Should_Start_And_Show_Banks_Correctly()
        {
            Application application = Application.Launch(applicationPath);
            Window mainWindow = application.GetWindows()[0];
            MenuBar clients = mainWindow.MenuBars[3];
            clients.Click();
            // here we have to adjust the click;
            Point point = new Point(718,130);
            Mouse.Instance.Click(point);
            application.Close();
        }

        [Test]
        public void Should_Click_ToolBarExitButton_And_Exit()
        {
            Application application = Application.Launch(applicationPath);
            Window mainWindow = application.GetWindows()[0];
            SearchCriteria searchCriteria = SearchCriteria.ByAutomationId("tbAcciones");
            var toolBarTest= mainWindow.Get<ToolStrip>(searchCriteria);
            searchCriteria = SearchCriteria.ByAutomationId("tbbtnSalir");
            Button toolBarExitButton = toolBarTest.Get<Button>(searchCriteria);
            toolBarExitButton.Click();
            application.WaitWhileBusy();
            Assert.AreEqual(true, application.HasExited);
            application.Close();
        }

    }
}
