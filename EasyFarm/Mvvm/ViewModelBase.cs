﻿
/*///////////////////////////////////////////////////////////////////
<EasyFarm, general farming utility for FFXI.>
Copyright (C) <2013>  <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using EasyFarm.Components;
using EasyFarm.UserSettings;
using FFACETools;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;

namespace EasyFarm.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        /// <summary>
        /// View Model name for header in tabcontrol item. 
        /// </summary>
        public String VMName { get; set; }

        /// <summary>
        /// Solo FFACE instance for current player. 
        /// </summary>
        public static FFACE FFACE { get; set; }      

        public static void SetSession(FFACE fface)
        {
            if (fface == null) return;

            // Save FFACE Instance
            FFACE = fface;

            // Create a new game engine to control our character. 
            App.GameEngine = new GameEngine(FFACE);
        }
    }
}
