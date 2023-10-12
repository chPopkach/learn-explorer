using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Explorer.Commands;
using Explorer.Core;
using Explorer.FileEntities;

namespace Explorer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor
        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(Open);

            foreach (var logicalDrive in Directory.GetLogicalDrives())
            {
                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive));
            }
        }

        #endregion

        #region Commands

        public ICommand OpenCommand { get; }

        #endregion

        #region Commands Methods

        private void Open(object parameter)
        {
            if (parameter is DirectoryViewModel directoryViewModel)
            {
                FilePath = directoryViewModel.FullName;

                DirectoriesAndFiles.Clear();

                var directoryInfo = new DirectoryInfo(FilePath);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    DirectoriesAndFiles.Add(new DirectoryViewModel(directory));
                }

                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    DirectoriesAndFiles.Add(new FileViewModel(fileInfo));
                }
            }
        }

        #endregion

        #region Events

        #endregion

        #region Protected Methods

        #endregion

        #region Public Properties
        public FileEntityViewModel SelectedFileEntity { get; set; }

        public string FilePath { get; set; }

        public ObservableCollection<FileEntityViewModel> DirectoriesAndFiles { get; set; } = new ObservableCollection<FileEntityViewModel>();
        #endregion
    }
}
