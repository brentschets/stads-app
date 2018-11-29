using System;
using System.Collections.Generic;

namespace Stads_App.Views
{
    public static class AnimatedViews
    {
        public static readonly IEnumerable<Type> All = new List<Type>
        {
            typeof(Home),
            typeof(Stores)
        };
    }
}