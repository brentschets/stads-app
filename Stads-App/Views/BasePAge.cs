using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Stads_App.Views
{
    public class BasePage: Page
    {
        public virtual string Header { get; protected set; }

        protected static readonly IEnumerable<Type> AnimatedViews = new List<Type>
        {
            typeof(Home),
            typeof(Stores)
        };
    }
}
