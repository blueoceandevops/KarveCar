﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NavigationDrawerExercise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Bootstrapper bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
        
            base.OnStartup(e);
            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
