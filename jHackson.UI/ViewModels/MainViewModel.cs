// <copyright file="MainViewModel.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.UI.ViewModels
{
    using System.Windows;
    using Prism.Commands;

    public class MainViewModel
    {
        public MainViewModel()
        {
            this.QuitCommand = new DelegateCommand(this.Quit);
        }

        public DelegateCommand QuitCommand { get; private set; }

        private void Quit()
        {
            Application.Current.MainWindow.Close();
        }
    }
}