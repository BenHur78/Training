namespace Infrastructure
{
    #region Using Directives

    using System.Windows;
    using DevExpress.Xpf.Ribbon;

    #endregion

    public class RibbonPageEx : RibbonPage
    {
        public static readonly DependencyProperty CustomIsVisibleProperty;
        public static readonly DependencyProperty ContextNameProperty;

        static RibbonPageEx()
        {
            ActualIsVisiblePropertyKey.OverrideMetadata(typeof(RibbonPageEx),
                new FrameworkPropertyMetadata(true, OnActualIsVisbilePropertyChanged,
                    OnActualIsVisisbleCoerceValue));
            CustomIsVisibleProperty = DependencyProperty.Register("CustomIsVisible", typeof(bool), typeof(RibbonPageEx),
                new FrameworkPropertyMetadata(true,
                    OnCustomIsVisiblePropertyChanged));
            ContextNameProperty = DependencyProperty.Register("ContextName", typeof(string), typeof(RibbonPageEx),
                new FrameworkPropertyMetadata(null));
        }

        public bool CustomIsVisible
        {
            get => (bool) GetValue(CustomIsVisibleProperty);
            set => SetValue(CustomIsVisibleProperty, value);
        }

        public string ContextName
        {
            get => (string) GetValue(ContextNameProperty);
            set => SetValue(ContextNameProperty, value);
        }

        private static void OnActualIsVisbilePropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var ribbonPageEx = (RibbonPageEx) dependencyObject;

            ribbonPageEx.OnActualIsVisibleChanged((bool) e.OldValue, (bool) e.NewValue);
        }

        private static object OnActualIsVisisbleCoerceValue(DependencyObject dependencyObject, object baseValue)
        {
            var ribbonPageEx = (RibbonPageEx) dependencyObject;

            return ribbonPageEx.CoerceActualIsVisible((bool) baseValue);
        }

        private static void OnCustomIsVisiblePropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ribbonPageEx = (RibbonPageEx) dependencyObject;

            ribbonPageEx.UpdateActualIsVisible();
        }

        private object CoerceActualIsVisible(bool baseValue)
        {
            return baseValue && CustomIsVisible;
        }
    }
}