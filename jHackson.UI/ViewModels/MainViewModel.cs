// <copyright file="MainViewModel.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.UI.ViewModels
{
    using System.Windows;
    using JHackson.Core.Projects;
    using JHackson.Core.Services;
    using Microsoft.Win32;
    using Prism.Commands;
    using Prism.Mvvm;

    public class MainViewModel : BindableBase
    {
        private const string TITLE_BASE = "jHackson";

        private readonly ISerializationService serializationService;

        private IProjectJson project;

        private string projectPath;

        private string title;

        public MainViewModel(ISerializationService serializationService)
        {
            this.serializationService = serializationService;

            this.NewCommand = new DelegateCommand(this.New);
            this.OpenCommand = new DelegateCommand(this.Open);
            this.QuitCommand = new DelegateCommand(this.Quit);
            this.SaveCommand = new DelegateCommand(this.Save, this.CanSave);
            this.SaveAsCommand = new DelegateCommand(this.SaveAs, this.CanSave);

            this.UpdateTitle();
        }

        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand OpenCommand { get; private set; }

        public DelegateCommand QuitCommand { get; private set; }

        public DelegateCommand SaveAsCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        private bool CanSave()
        {
            return this.project != null;
        }

        private void New()
        {
            if (this.project != null)
            {
                // TODO : Save the project ?

                this.project = null;

                // TODO : Close all UI elements
            }

            this.project = new ProjectJson();
            this.SaveCommand.RaiseCanExecuteChanged();
            this.SaveAsCommand.RaiseCanExecuteChanged();

            this.UpdateTitle();
        }

        private void Open()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "jHackson project (.json)|*.json",
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                this.projectPath = openFileDialog.FileName;
                this.project = this.serializationService.Deserialize(this.projectPath);

                this.UpdateTitle(this.project.Game);
            }
        }

        private void Quit()
        {
            Application.Current.MainWindow.Close();
        }

        private void Save()
        {
            bool saveConfirmed = true;

            if (string.IsNullOrWhiteSpace(this.projectPath))
            {
                saveConfirmed = false;

                var saveFileDialog = new SaveFileDialog
                {
                    FileName = this.project.Game,
                    DefaultExt = ".json",
                    Filter = "jHackson project (.json)|*.json"
                };

                bool? result = saveFileDialog.ShowDialog();

                if (result == true)
                {
                    this.projectPath = saveFileDialog.FileName;
                }
            }

            if (saveConfirmed)
            {
                this.project.Game = "test";
                this.serializationService.Serialize(this.project, this.projectPath);
            }
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog
            {
                FileName = this.project.Game,
                DefaultExt = ".json",
                Filter = "jHackson project (.json)|*.json"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                this.projectPath = saveFileDialog.FileName;
                this.project.Game = "test";
                this.serializationService.Serialize(this.project, this.projectPath);
            }
        }

        private void UpdateTitle(string game = null)
        {
            if (!string.IsNullOrWhiteSpace(game))
            {
                this.Title = $"{TITLE_BASE} - {game}";
            }
            else
            {
                this.Title = $"{TITLE_BASE}";
            }
        }
    }
}