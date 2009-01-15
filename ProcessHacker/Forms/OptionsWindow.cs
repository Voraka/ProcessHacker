﻿/*
 * Process Hacker - 
 *   options window
 * 
 * Copyright (C) 2008 Dean
 * Copyright (C) 2008-2009 wj32
 * 
 * This file is part of Process Hacker.
 * 
 * Process Hacker is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Process Hacker is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Process Hacker.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace ProcessHacker
{
    public partial class OptionsWindow : Form
    {
        public OptionsWindow()
        {
            InitializeComponent();

            textUpdateInterval.Value = Properties.Settings.Default.RefreshInterval;
            textSearchEngine.Text = Properties.Settings.Default.SearchEngine;
            comboSizeUnits.SelectedItem =
                Misc.SizeUnitNames[Properties.Settings.Default.UnitSpecifier];
            checkWarnDangerous.Checked = Properties.Settings.Default.WarnDangerous;
            checkShowProcessDomains.Checked = Properties.Settings.Default.ShowAccountDomains;
            checkShowTrayIcon.Checked = Properties.Settings.Default.ShowIcon;
            checkHideWhenMinimized.Checked = Properties.Settings.Default.HideWhenMinimized;
            checkVerifySignatures.Checked = Properties.Settings.Default.VerifySignatures;

            textHighlightingDuration.Value = Properties.Settings.Default.HighlightingDuration;
            colorNewProcesses.Color = Properties.Settings.Default.ColorNewProcesses;
            colorRemovedProcesses.Color = Properties.Settings.Default.ColorRemovedProcesses;
            colorOwnProcesses.Color = Properties.Settings.Default.ColorOwnProcesses;
            colorSystemProcesses.Color = Properties.Settings.Default.ColorSystemProcesses;
            colorServiceProcesses.Color = Properties.Settings.Default.ColorServiceProcesses;
            colorBeingDebugged.Color = Properties.Settings.Default.ColorBeingDebugged;
            colorElevatedProcesses.Color = Properties.Settings.Default.ColorElevatedProcesses;
            colorJobProcesses.Color = Properties.Settings.Default.ColorJobProcesses;
            colorDotNetProcesses.Color = Properties.Settings.Default.ColorDotNetProcesses;
            colorPackedProcesses.Color = Properties.Settings.Default.ColorPackedProcesses;

            checkPlotterAntialias.Checked = Properties.Settings.Default.PlotterAntialias;
            colorCPUKT.Color = Properties.Settings.Default.PlotterCPUKernelColor;
            colorCPUUT.Color = Properties.Settings.Default.PlotterCPUUserColor;
            colorMemoryPB.Color = Properties.Settings.Default.PlotterMemoryPrivateColor;
            colorMemoryWS.Color = Properties.Settings.Default.PlotterMemoryWSColor;
            colorIORO.Color = Properties.Settings.Default.PlotterIOROColor;
            colorIOW.Color = Properties.Settings.Default.PlotterIOWColor;
        }

        private void textUpdateInterval_Leave(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.RefreshInterval = Int32.Parse(textUpdateInterval.Value.ToString());
            }
            catch
            {
                MessageBox.Show("The entered value is not valid.", "Process Hacker", MessageBoxButtons.OK,
                 MessageBoxIcon.Error);

                textUpdateInterval.Focus();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SearchEngine = textSearchEngine.Text;
            Properties.Settings.Default.WarnDangerous = checkWarnDangerous.Checked;
            Properties.Settings.Default.ShowAccountDomains = checkShowProcessDomains.Checked;
            Properties.Settings.Default.ShowIcon = checkShowTrayIcon.Checked;
            Properties.Settings.Default.HideWhenMinimized = checkHideWhenMinimized.Checked;
            Properties.Settings.Default.UnitSpecifier =
                Array.IndexOf(Misc.SizeUnitNames, comboSizeUnits.SelectedItem);
            Properties.Settings.Default.VerifySignatures = checkVerifySignatures.Checked;

            Program.HackerWindow.NotifyIcon.Visible = Properties.Settings.Default.ShowIcon;
            Program.HackerWindow.ProcessProvider.Interval = Properties.Settings.Default.RefreshInterval;
            Program.HackerWindow.ServiceProvider.Interval = Properties.Settings.Default.RefreshInterval;

            Properties.Settings.Default.HighlightingDuration = (int)textHighlightingDuration.Value;
            Properties.Settings.Default.ColorNewProcesses = colorNewProcesses.Color;
            Properties.Settings.Default.ColorRemovedProcesses = colorRemovedProcesses.Color;
            Properties.Settings.Default.ColorOwnProcesses = colorOwnProcesses.Color;
            Properties.Settings.Default.ColorSystemProcesses = colorSystemProcesses.Color;
            Properties.Settings.Default.ColorServiceProcesses = colorServiceProcesses.Color;
            Properties.Settings.Default.ColorBeingDebugged = colorBeingDebugged.Color;
            Properties.Settings.Default.ColorElevatedProcesses = colorElevatedProcesses.Color;
            Properties.Settings.Default.ColorJobProcesses = colorJobProcesses.Color;
            Properties.Settings.Default.ColorDotNetProcesses = colorDotNetProcesses.Color;
            Properties.Settings.Default.ColorPackedProcesses = colorPackedProcesses.Color;

            Properties.Settings.Default.PlotterAntialias = checkPlotterAntialias.Checked;
            Properties.Settings.Default.PlotterCPUKernelColor = colorCPUKT.Color;
            Properties.Settings.Default.PlotterCPUUserColor = colorCPUUT.Color;
            Properties.Settings.Default.PlotterMemoryPrivateColor = colorMemoryPB.Color;
            Properties.Settings.Default.PlotterMemoryWSColor = colorMemoryWS.Color;
            Properties.Settings.Default.PlotterIOROColor = colorIORO.Color;
            Properties.Settings.Default.PlotterIOWColor = colorIOW.Color;

            // apply the settings immediately if we can
            HighlightedListViewItem.HighlightingDuration = Properties.Settings.Default.HighlightingDuration;
            HighlightedListViewItem.Colors[ListViewItemState.New] = Properties.Settings.Default.ColorNewProcesses;
            HighlightedListViewItem.Colors[ListViewItemState.Removed] = Properties.Settings.Default.ColorRemovedProcesses;
            TreeNodeAdv.StateColors[TreeNodeAdv.NodeState.New] = Properties.Settings.Default.ColorNewProcesses;
            TreeNodeAdv.StateColors[TreeNodeAdv.NodeState.Removed] = Properties.Settings.Default.ColorRemovedProcesses;

            Properties.Settings.Default.Save();
            Program.HackerWindow.ProcessList.RefreshItems();
            this.Close();
        }

        private void checkShowTrayIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowTrayIcon.Checked)
            {
                checkHideWhenMinimized.Enabled = true;
            }
            else
            {
                checkHideWhenMinimized.Enabled = false;
                checkHideWhenMinimized.Checked = false;
            }
        }
    }
}
