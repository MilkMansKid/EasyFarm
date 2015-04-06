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

using EasyFarm.UserSettings;
using EasyFarm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyFarm.Views.Settings
{
    public class BattleSettingsViewModel : ViewModelBase
    {
        public bool ShouldEngage 
        {
            get { return Config.Instance.IsEngageEnabled; }
            set { this.SetProperty(ref Config.Instance.IsEngageEnabled, value); }
        }

        public bool ShouldApproach
        {
            get { return Config.Instance.IsApproachEnabled; }
            set { this.SetProperty(ref Config.Instance.IsApproachEnabled, value); }
        }
    }    
}

namespace EasyFarm.UserSettings
{
    /// <summary>
    /// Battle Settings
    /// </summary>
    public partial class Config
    {
        /// <summary>
        /// Should we engage our target when in battle.
        /// </summary>
        public bool IsEngageEnabled = true;

        /// <summary>
        /// Should we move to the target once pulled. 
        /// </summary>
        public bool IsApproachEnabled = true;
    }
}
