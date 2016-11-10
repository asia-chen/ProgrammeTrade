﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PubTools;


namespace MDProcess
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PubTools.GlobalVar.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMDProcess());
        }
    }
}