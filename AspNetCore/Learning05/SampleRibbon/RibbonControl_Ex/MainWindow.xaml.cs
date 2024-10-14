using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Settings;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Core;
using WebApi;

namespace RibbonControl_Ex {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {

        private WebApi.WebApiInitializer _webApi;

        public MainWindow() {
            InitializeComponent();
            bFileName.Content = "Document 1";

            //HomePageInitialization();

            //Page1Initialization();

            //Page3Initialization();

            _webApi = new WebApi.WebApiInitializer();
            _webApi.InitWebApi();
        }

        public void AddRibbonPage(RibbonPage page, int index)
        {
            ribbonDefaultPageCategory.Pages.Insert(index, page);
        }

        public void HomePageInitialization()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("/RibbonControl_Ex;component/Home.xaml", UriKind.RelativeOrAbsolute)
            };
            
            var homePage = resourceDictionary["HomePage"] as RibbonPage;

            AddRibbonPage(homePage, 0);
        }

        public void Page1Initialization()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("/RibbonControl_Ex;component/Page1.xaml", UriKind.RelativeOrAbsolute)
            };

            var page1 = resourceDictionary["Page1"] as RibbonPage;

            AddRibbonPage(page1, 1);
        }

        public void Page3Initialization()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("/RibbonControl_Ex;component/Page3.xaml", UriKind.RelativeOrAbsolute)
            };

            var page3 = resourceDictionary["Page3"] as RibbonPage;

            AddRibbonPage(page3, 2);
        }

    }
}
