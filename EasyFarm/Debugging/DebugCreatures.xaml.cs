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

using EasyFarm.Classes;
using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EasyFarm.Debugging
{
    /// <summary>
    /// Interaction logic for DebugCreatures.xaml
    /// </summary>
    public partial class DebugCreatures : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private UnitService _units;
        private Zone _zone;
        private FFACE _fface;

        public DebugCreatures(FFACE fface)
        {
            InitializeComponent();
            this._fface = fface;
            this._units = new UnitService(fface);

            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            _timer.Start();
            this.Show();
        }

        public IEnumerable<Unit> Units
        {
            get { return _units.UnitArray; }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            var selectedItem = lstMobNames.SelectedItem;

            if (_fface.Player.Zone != _zone) this.lstMobNames.Items.Clear();

            foreach (var mob in Units)
            {
                if (!lstMobNames.Items.Contains(mob.Name + ":" + mob.ID) && mob.Name != "")
                {
                    lstMobNames.Items.Add("{0}:{1}"
                        .Replace("{0}", mob.Name)
                        .Replace("{1}", mob.ID.ToString()));
                }
            }

            lstMobNames.SelectedItem = selectedItem;

            _zone = _fface.Player.Zone;
        }

        private void lstMobNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstMobData.Items.Clear();
            Unit Mob = Unit.CreateUnit(0);

            var Query = from i in Units
                        where lstMobNames.SelectedItem != null
                        let SplitSelectedItem = lstMobNames.SelectedItem.ToString().Split(':')
                        where i.Name == SplitSelectedItem[0] && i.ID.ToString() == SplitSelectedItem[1]
                        select i;

            if (Query.Count() > 0) Mob = Query.First();

            lstMobData.Items.Add("Name: " + Mob.Name);
            lstMobData.Items.Add("IsActive: " + Mob.IsActive);
            lstMobData.Items.Add("ID: " + Mob.ID);
            lstMobData.Items.Add("Claimed ID: " + Mob.ClaimedID);
            lstMobData.Items.Add("NPCBit: " + Mob.NPCBit);
            lstMobData.Items.Add("NPCType: " + Mob.NPCType);
            lstMobData.Items.Add("Status: " + Mob.Status);
            lstMobData.Items.Add("HPPCurrent: " + Mob.HPPCurrent);
            lstMobData.Items.Add("Distance: " + Mob.Distance);
            lstMobData.Items.Add("IsDead: " + Mob.IsDead);
            lstMobData.Items.Add("IsRendered: " + Mob.IsRendered);
            lstMobData.Items.Add("HasAggroed: " + Mob.HasAggroed);
            lstMobData.Items.Add("MyClaim: " + Mob.MyClaim);
            lstMobData.Items.Add("PartyClaim: " + Mob.PartyClaim);
            lstMobData.Items.Add("IsClaimed: " + Mob.IsClaimed);
            lstMobData.Items.Add("PetID: " + Mob.PetID);
            lstMobData.Items.Add("Position: " + Mob.Position);            
            lstMobData.Items.Add("PosH: " + Mob.PosH);
            lstMobData.Items.Add("PosX: " + Mob.PosX);
            lstMobData.Items.Add("PosY: " + Mob.PosY);
            lstMobData.Items.Add("PosZ: " + Mob.PosZ);
            lstMobData.Items.Add("TPCurrent: " + Mob.TPCurrent);
            lstMobData.Items.Add("YDifference" + Mob.YDifference);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lstMobData.Items.Clear();
        }
    }
}
