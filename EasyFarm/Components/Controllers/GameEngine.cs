
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
using EasyFarm.FarmingTool;
using EasyFarm.ViewModels;
using FFACETools;
using System;
using System.Threading;

namespace EasyFarm.Components
{
    /// <summary>
    /// Controls whether or not the bot should run. Basically anything 
    /// that can pause or resume the bot's engine should be here. 
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Tells us whether the bot is working or not.
        /// </summary>
        public bool IsWorking = false;

        /// <summary>
        /// Monitors for zone changes and allows for pausing / resuming
        /// the program after zoning. 
        /// </summary>
        private ZoneMonitor _zoneMonitor;

        /// <summary>
        /// Monitors for players nearby and allows for pausing / resuming
        /// the program on detection. 
        /// </summary>
        private PlayerMonitor _playerMonitor;

        /// <summary>
        /// Monitors the player's status for dead and shuts down the 
        /// program when he is. 
        /// </summary>
        private DeadMonitor _statusMonitor;

        /// <summary>
        /// Monitors the player tos ee if he's stuck agains a wall and 
        /// if so stops the engine. 
        /// </summary>
        private StuckMonitor _stuckMonitor;

        /// <summary>
        /// Provides information about game data. 
        /// </summary>
        private FFACE _fface;

        /// <summary>
        /// The engine that controls player actions. 
        /// </summary>
        private FiniteStateEngine _stateMachine;

        public GameEngine(FFACE fface)
        {
            this._fface = fface;
            this._zoneMonitor = new ZoneMonitor(fface);
            this._playerMonitor = new PlayerMonitor(fface);
            this._statusMonitor = new DeadMonitor(fface);
            this._stuckMonitor = new StuckMonitor(fface);
            this._stateMachine = new FiniteStateEngine(fface);

            _zoneMonitor.Changed += ZoneMonitor_ZoneChanged;
            _zoneMonitor.Start();

            _statusMonitor.Changed += StatusMonitor_StatusChanged;
            _statusMonitor.Start();
        }

        /// <summary>
        /// Monitors engine status for player being stuck and 
        /// shuts it down when detected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StuckMonitor_StuckChanged(object sender, EventArgs e)
        {
            // Do nothing if the engine is not running already. 
            if (!IsWorking) { return; }

            // Stop program from running to next waypoint. 
            _fface.Navigator.Reset();

            // Tell the use we paused the program. 
            AppInformer.InformUser("Program Paused");

            // Stop the engine from running. 
            Stop();
        }

        /// <summary>
        /// Monitors engine stats for player dead status 
        /// and then shuts down the engine. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusMonitor_StatusChanged(object sender, EventArgs e)
        {
            // Do nothing if the engine is not running already. 
            if (!IsWorking) { return; }

            // Stop program from running to next waypoint. 
            _fface.Navigator.Reset();

            // Tell the use we paused the program. 
            AppInformer.InformUser("Program Paused");

            // Stop the engine from running. 
            Stop();
        }

        /// <summary>
        /// Stops the engine when another player is detected and 
        /// resumes afterwards. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerMonitor_DetectedChanged(object sender, EventArgs e)
        {
            // If the program is not running then bail out. 
            if (!IsWorking) { return; }

            var args = (e as MonitorArgs<bool>);
            if (args.Status)
            {
                AppInformer.InformUser("Program Paused");
                Stop();
            }
            else
            {
                AppInformer.InformUser("Program Resumed");
                Start();
            }
        }

        /// <summary>
        /// Pauses the engine when the player zones and
        /// resumes after. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoneMonitor_ZoneChanged(object sender, EventArgs e)
        {
            var args = (e as MonitorArgs<Zone>);

            // If the program is not running then bail out. 
            if (!IsWorking) { return; }

            AppInformer.InformUser("Program Paused");

            // Stop the state machine.
            Stop();

            // Wait until our player has zoned; 
            while (_fface.Player.Stats.Str == 0)  
            {
                Thread.Sleep(500);
            }

            // Start up the state machine again.
            Start();

            AppInformer.InformUser("Program Resumed");
        }

        /// <summary>
        /// Start the bot up
        /// </summary>
        public void Start()
        {
            _stateMachine.Start();
            IsWorking = true;
        }

        /// <summary>
        /// Stop the bot from going any further
        /// </summary>
        public void Stop()
        {
            _stateMachine.Stop();
            IsWorking = false;
        }
    }
}
