// <copyright file="MainViewModel.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using JHackson.Core.Projects;
    using JHackson.Core.Services;
    using JHackson.Core.Variables;
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

        private ObservableCollection<Variable> variableList;

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

        public string Console
        {
            get => this.project?.Console;
            set
            {
                if (this.project.Console != value)
                {
                    this.project.Console = value;
                }
                this.RaisePropertyChanged(nameof(this.Console));
            }
        }

        public string Description
        {
            get => this.project?.Description;
            set
            {
                if (this.project.Description != value)
                {
                    this.project.Description = value;
                }
                this.RaisePropertyChanged(nameof(this.Console));
            }
        }

        public string Game
        {
            get => this.project?.Game;
            set
            {
                if (this.project.Game != value)
                {
                    this.project.Game = value;
                }
                this.RaisePropertyChanged(nameof(this.Game));
            }
        }

        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand OpenCommand { get; private set; }

        public bool ProjectIsEditable => this.project != null;

        public DelegateCommand QuitCommand { get; private set; }

        public DelegateCommand SaveAsCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public ObservableCollection<Variable> VariablesList
        {
            get
            {
                if (this.variableList == null && this.project != null && this.project.Variables.Any())
                {
                    this.variableList = new ObservableCollection<Variable>(this.project.Variables);
                }

                return this.variableList;
            }
        }

        public string Version
        {
            get => this.project?.Version;
            set
            {
                if (this.project.Version != value)
                {
                    this.project.Version = value;
                }
                this.RaisePropertyChanged(nameof(this.Version));
            }
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
            }

            this.project = new ProjectJson();

            this.UpdateTitle();
            this.RaiseProjectChanged();
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
                this.project = null;

                this.projectPath = openFileDialog.FileName;
                this.project = this.serializationService.Deserialize(this.projectPath);

                this.UpdateTitle(Path.GetFileNameWithoutExtension(openFileDialog.FileName));
                this.RaiseProjectChanged();
            }
        }

        private void Quit()
        {
            Application.Current.MainWindow.Close();
        }

        private void RaiseProjectChanged()
        {
            this.RaisePropertyChanged(nameof(this.Console));
            this.RaisePropertyChanged(nameof(this.Description));
            this.RaisePropertyChanged(nameof(this.Game));
            this.RaisePropertyChanged(nameof(this.Version));
            this.RaisePropertyChanged(nameof(this.ProjectIsEditable));
            this.RaisePropertyChanged(nameof(this.VariablesList));
            this.SaveCommand.RaiseCanExecuteChanged();
            this.SaveAsCommand.RaiseCanExecuteChanged();
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

        private void UpdateTitle(string fileName = null)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                this.Title = $"{TITLE_BASE} - {fileName}";
            }
            else
            {
                this.Title = $"{TITLE_BASE}";
            }
        }
    }
}