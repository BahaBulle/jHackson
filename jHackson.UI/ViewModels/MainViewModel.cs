// <copyright file="MainViewModel.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.UI.ViewModels
{
    using System.Windows;
    using JHackson.Core.Projects;
    using Prism.Commands;

    public class MainViewModel
    {
        private IProjectJson project;

        public MainViewModel()
        {
            this.NewCommand = new DelegateCommand(this.New);
            this.QuitCommand = new DelegateCommand(this.Quit);
        }

        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand QuitCommand { get; private set; }

        private void New()
        {
            if (this.project != null)
            {
                // TODO : Save the project

                this.project = null;

                // TODO : Close all UI elements
            }

            this.project = new ProjectJson();
        }

        private void Quit()
        {
            Application.Current.MainWindow.Close();
        }
    }
}