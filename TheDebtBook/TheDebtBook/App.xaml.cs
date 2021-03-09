using System;
using System.Windows;
using Prism.Unity;
using Prism.Ioc;
using TheDebtBook.Models;
using TheDebtBook.ViewModels;
using TheDebtBook.Views;

namespace TheDebtBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterDialog<AddView, AddViewModel>();
            containerRegistry.RegisterDialog<EditShowDetailView, EditShowDetailViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        private Debitors debitors = new Debitors();

        public Debitors Debitor
        {
            get { return debitors; }

            set => debitors = value;
        }
    }
}
