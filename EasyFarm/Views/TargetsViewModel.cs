
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
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace EasyFarm.ViewModels
{
    [ViewModelAttribute("Targets")]
    public class TargetsViewModel : ViewModelBase
    {
        public TargetsViewModel()
        {
            AddCommand = new DelegateCommand(AddTargetCommand);
            DeleteCommand = new DelegateCommand(DeleteTargetCommand);
            ClearCommand = new DelegateCommand(ClearTargetsCommand);
        }

        private void ClearTargetsCommand()
        {
            Targets.Clear();
        }

        private void DeleteTargetCommand()
        {
            if (Targets.Contains(TargetsName))
            {
                Targets.Remove(TargetsName);
            }
        }

        private void AddTargetCommand()
        {
            if (!Targets.Contains(TargetsName))
            {
                Targets.Add(TargetsName);
            }
        }

        public String TargetsName
        {
            get { return Config.Instance.TargetName; }
            set { SetProperty(ref Config.Instance.TargetName, value); }
        }

        public ObservableCollection<String> Targets
        {
            get { return Config.Instance.TargetedMobs; }
            set { SetProperty(ref Config.Instance.TargetedMobs, value); }
        }

        public bool Aggro
        {
            get { return Config.Instance.AggroFilter; }
            set { SetProperty(ref Config.Instance.AggroFilter, value); }
        }

        public bool Unclaimed
        {
            get { return Config.Instance.UnclaimedFilter; }
            set { SetProperty(ref Config.Instance.UnclaimedFilter, value); }
        }

        public bool PartyClaimed
        {
            get { return Config.Instance.PartyFilter; }
            set { SetProperty(ref Config.Instance.PartyFilter, value); }
        }

        public bool Claimed
        {
            get { return Config.Instance.ClaimedFilter; }
            set { SetProperty(ref Config.Instance.ClaimedFilter, value); }
        }

        public bool BitCheck
        {
            get { return Config.Instance.BitCheck; }
            set { SetProperty(ref Config.Instance.BitCheck, value); }
        }

        public ICommand AddCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ClearCommand { get; set; }
    }
}

namespace EasyFarm.UserSettings
{
    public partial class Config
    {
        /// <summary>
        /// Name of the mob to be attacked
        /// </summary>
        public string TargetName = String.Empty;

        /// <summary>
        /// Used to filter out aggroed mobs.
        /// </summary>
        public bool AggroFilter = true;

        /// <summary>
        /// Used to filter out party claimed mobs.
        /// </summary>
        public bool PartyFilter = true;

        /// <summary>
        /// Used to filter out unclaimed mobs.
        /// </summary>
        public bool UnclaimedFilter = true;

        /// <summary>
        /// Used to include claimed mobs in the filter. 
        /// </summary>
        public bool ClaimedFilter = false;

        /// <summary>
        /// Used to control whether we should use npc bit checking
        /// in mob validation. 
        /// </summary>
        public bool BitCheck = true;      

        /// <summary>
        /// A list of mobs that we should only kill.
        /// </summary>
        public ObservableCollection<String> TargetedMobs = new ObservableCollection<string>();
    }
}