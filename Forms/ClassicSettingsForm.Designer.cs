namespace Kombatant.Forms
{
	partial class ClassicSettingsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tbcSettings;
		private System.Windows.Forms.TabPage tbpConvenience;
		private System.Windows.Forms.TabPage tbpTargeting;
		private System.Windows.Forms.CheckBox chkIsPaused;
		private System.Windows.Forms.TabPage tbpMovement;
		private System.Windows.Forms.TabPage tbpCrBehavior;
		private System.Windows.Forms.TabPage tbpHotkeys;
		private System.Windows.Forms.CheckBox chkIsAutonomous;
		private System.Windows.Forms.CheckBox chkMechanicWarnings;
		private System.Windows.Forms.CheckBox chkAutoAcceptQuests;
		private System.Windows.Forms.CheckBox chkAutoAdvanceDialogue;
		private System.Windows.Forms.CheckBox chkAutoSkipCutscenes;
		private System.Windows.Forms.CheckBox chkAutoFaceTarget;
		private System.Windows.Forms.CheckBox chkAutoSprint;
		private System.Windows.Forms.CheckBox chkAutoSyncFate;
		private System.Windows.Forms.CheckBox chkAutoEndActEncounters;
		private System.Windows.Forms.CheckBox chkAutoAcceptDutyFinder;
		private System.Windows.Forms.CheckBox chkDutyFinderNotify;
		private System.Windows.Forms.Label lblDutyFinderWaitTime;
		private System.Windows.Forms.NumericUpDown numDutyFinderWaitTime;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkEnableSmartPull;
		private System.Windows.Forms.CheckBox chkEnableCombat;
		private System.Windows.Forms.CheckBox chkEnableCombatBuff;
		private System.Windows.Forms.CheckBox chkEnablePull;
		private System.Windows.Forms.CheckBox chkEnablePullBuff;
		private System.Windows.Forms.CheckBox chkEnablePreCombatBuff;
		private System.Windows.Forms.CheckBox chkEnableHeal;
		private System.Windows.Forms.CheckBox chkEnableRest;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassicSettingsForm));
			this.tbcSettings = new System.Windows.Forms.TabControl();
			this.tbpConvenience = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDutyFinderWaitTime = new System.Windows.Forms.Label();
			this.numDutyFinderWaitTime = new System.Windows.Forms.NumericUpDown();
			this.chkIsAutonomous = new System.Windows.Forms.CheckBox();
			this.chkIsPaused = new System.Windows.Forms.CheckBox();
			this.chkMechanicWarnings = new System.Windows.Forms.CheckBox();
			this.chkAutoEndActEncounters = new System.Windows.Forms.CheckBox();
			this.chkAutoAcceptQuests = new System.Windows.Forms.CheckBox();
			this.chkAutoAdvanceDialogue = new System.Windows.Forms.CheckBox();
			this.chkAutoSkipCutscenes = new System.Windows.Forms.CheckBox();
			this.chkAutoFaceTarget = new System.Windows.Forms.CheckBox();
			this.chkAutoSprint = new System.Windows.Forms.CheckBox();
			this.chkAutoSyncFate = new System.Windows.Forms.CheckBox();
			this.chkAutoAcceptDutyFinder = new System.Windows.Forms.CheckBox();
			this.chkDutyFinderNotify = new System.Windows.Forms.CheckBox();
			this.tbpTargeting = new System.Windows.Forms.TabPage();
			this.tbpMovement = new System.Windows.Forms.TabPage();
			this.tbpCrBehavior = new System.Windows.Forms.TabPage();
			this.chkEnableSmartPull = new System.Windows.Forms.CheckBox();
			this.tbpHotkeys = new System.Windows.Forms.TabPage();
			this.chkEnableRest = new System.Windows.Forms.CheckBox();
			this.chkEnableHeal = new System.Windows.Forms.CheckBox();
			this.chkEnablePreCombatBuff = new System.Windows.Forms.CheckBox();
			this.chkEnablePullBuff = new System.Windows.Forms.CheckBox();
			this.chkEnablePull = new System.Windows.Forms.CheckBox();
			this.chkEnableCombatBuff = new System.Windows.Forms.CheckBox();
			this.chkEnableCombat = new System.Windows.Forms.CheckBox();
			this.tbcSettings.SuspendLayout();
			this.tbpConvenience.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDutyFinderWaitTime)).BeginInit();
			this.tbpCrBehavior.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbcSettings
			// 
			this.tbcSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tbcSettings.Controls.Add(this.tbpConvenience);
			this.tbcSettings.Controls.Add(this.tbpTargeting);
			this.tbcSettings.Controls.Add(this.tbpMovement);
			this.tbcSettings.Controls.Add(this.tbpCrBehavior);
			this.tbcSettings.Controls.Add(this.tbpHotkeys);
			this.tbcSettings.Location = new System.Drawing.Point(3, 3);
			this.tbcSettings.Name = "tbcSettings";
			this.tbcSettings.SelectedIndex = 0;
			this.tbcSettings.Size = new System.Drawing.Size(381, 384);
			this.tbcSettings.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tbcSettings.TabIndex = 0;
			// 
			// tbpConvenience
			// 
			this.tbpConvenience.Controls.Add(this.panel2);
			this.tbpConvenience.Controls.Add(this.panel1);
			this.tbpConvenience.Controls.Add(this.lblDutyFinderWaitTime);
			this.tbpConvenience.Controls.Add(this.numDutyFinderWaitTime);
			this.tbpConvenience.Controls.Add(this.chkIsAutonomous);
			this.tbpConvenience.Controls.Add(this.chkIsPaused);
			this.tbpConvenience.Controls.Add(this.chkMechanicWarnings);
			this.tbpConvenience.Controls.Add(this.chkAutoEndActEncounters);
			this.tbpConvenience.Controls.Add(this.chkAutoAcceptQuests);
			this.tbpConvenience.Controls.Add(this.chkAutoAdvanceDialogue);
			this.tbpConvenience.Controls.Add(this.chkAutoSkipCutscenes);
			this.tbpConvenience.Controls.Add(this.chkAutoFaceTarget);
			this.tbpConvenience.Controls.Add(this.chkAutoSprint);
			this.tbpConvenience.Controls.Add(this.chkAutoSyncFate);
			this.tbpConvenience.Controls.Add(this.chkAutoAcceptDutyFinder);
			this.tbpConvenience.Controls.Add(this.chkDutyFinderNotify);
			this.tbpConvenience.Location = new System.Drawing.Point(4, 22);
			this.tbpConvenience.Name = "tbpConvenience";
			this.tbpConvenience.Padding = new System.Windows.Forms.Padding(3);
			this.tbpConvenience.Size = new System.Drawing.Size(373, 358);
			this.tbpConvenience.TabIndex = 0;
			this.tbpConvenience.Text = "Convenience";
			this.tbpConvenience.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.DarkGray;
			this.panel2.Location = new System.Drawing.Point(6, 168);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(361, 18);
			this.panel2.TabIndex = 43;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DarkGray;
			this.panel1.Location = new System.Drawing.Point(6, 75);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(361, 18);
			this.panel1.TabIndex = 42;
			// 
			// lblDutyFinderWaitTime
			// 
			this.lblDutyFinderWaitTime.AutoSize = true;
			this.lblDutyFinderWaitTime.Location = new System.Drawing.Point(82, 240);
			this.lblDutyFinderWaitTime.Name = "lblDutyFinderWaitTime";
			this.lblDutyFinderWaitTime.Size = new System.Drawing.Size(103, 13);
			this.lblDutyFinderWaitTime.TabIndex = 41;
			this.lblDutyFinderWaitTime.Text = "DutyFinderWaitTime";
			// 
			// numDutyFinderWaitTime
			// 
			this.numDutyFinderWaitTime.AutoSize = true;
			this.numDutyFinderWaitTime.Location = new System.Drawing.Point(6, 238);
			this.numDutyFinderWaitTime.Maximum = new decimal(new int[] {
			40,
			0,
			0,
			0});
			this.numDutyFinderWaitTime.Name = "numDutyFinderWaitTime";
			this.numDutyFinderWaitTime.Size = new System.Drawing.Size(70, 20);
			this.numDutyFinderWaitTime.TabIndex = 40;
			// 
			// chkIsAutonomous
			// 
			this.chkIsAutonomous.AutoSize = true;
			this.chkIsAutonomous.Location = new System.Drawing.Point(6, 29);
			this.chkIsAutonomous.Name = "chkIsAutonomous";
			this.chkIsAutonomous.Size = new System.Drawing.Size(93, 17);
			this.chkIsAutonomous.TabIndex = 39;
			this.chkIsAutonomous.Text = "IsAutonomous";
			this.chkIsAutonomous.UseVisualStyleBackColor = true;
			// 
			// chkIsPaused
			// 
			this.chkIsPaused.AutoSize = true;
			this.chkIsPaused.Location = new System.Drawing.Point(6, 6);
			this.chkIsPaused.Name = "chkIsPaused";
			this.chkIsPaused.Size = new System.Drawing.Size(70, 17);
			this.chkIsPaused.TabIndex = 38;
			this.chkIsPaused.Text = "IsPaused";
			this.chkIsPaused.UseVisualStyleBackColor = true;
			// 
			// chkMechanicWarnings
			// 
			this.chkMechanicWarnings.AutoSize = true;
			this.chkMechanicWarnings.Location = new System.Drawing.Point(6, 52);
			this.chkMechanicWarnings.Name = "chkMechanicWarnings";
			this.chkMechanicWarnings.Size = new System.Drawing.Size(118, 17);
			this.chkMechanicWarnings.TabIndex = 20;
			this.chkMechanicWarnings.Text = "MechanicWarnings";
			this.chkMechanicWarnings.UseVisualStyleBackColor = true;
			// 
			// chkAutoEndActEncounters
			// 
			this.chkAutoEndActEncounters.AutoSize = true;
			this.chkAutoEndActEncounters.Location = new System.Drawing.Point(189, 52);
			this.chkAutoEndActEncounters.Name = "chkAutoEndActEncounters";
			this.chkAutoEndActEncounters.Size = new System.Drawing.Size(140, 17);
			this.chkAutoEndActEncounters.TabIndex = 21;
			this.chkAutoEndActEncounters.Text = "AutoEndActEntcounters";
			this.chkAutoEndActEncounters.UseVisualStyleBackColor = true;
			// 
			// chkAutoAcceptQuests
			// 
			this.chkAutoAcceptQuests.AutoSize = true;
			this.chkAutoAcceptQuests.Location = new System.Drawing.Point(6, 99);
			this.chkAutoAcceptQuests.Name = "chkAutoAcceptQuests";
			this.chkAutoAcceptQuests.Size = new System.Drawing.Size(115, 17);
			this.chkAutoAcceptQuests.TabIndex = 22;
			this.chkAutoAcceptQuests.Text = "AutoAcceptQuests";
			this.chkAutoAcceptQuests.UseVisualStyleBackColor = true;
			// 
			// chkAutoAdvanceDialogue
			// 
			this.chkAutoAdvanceDialogue.AutoSize = true;
			this.chkAutoAdvanceDialogue.Location = new System.Drawing.Point(6, 122);
			this.chkAutoAdvanceDialogue.Name = "chkAutoAdvanceDialogue";
			this.chkAutoAdvanceDialogue.Size = new System.Drawing.Size(133, 17);
			this.chkAutoAdvanceDialogue.TabIndex = 23;
			this.chkAutoAdvanceDialogue.Text = "AutoAdvanceDialogue";
			this.chkAutoAdvanceDialogue.UseVisualStyleBackColor = true;
			// 
			// chkAutoSkipCutscenes
			// 
			this.chkAutoSkipCutscenes.AutoSize = true;
			this.chkAutoSkipCutscenes.Location = new System.Drawing.Point(6, 145);
			this.chkAutoSkipCutscenes.Name = "chkAutoSkipCutscenes";
			this.chkAutoSkipCutscenes.Size = new System.Drawing.Size(119, 17);
			this.chkAutoSkipCutscenes.TabIndex = 24;
			this.chkAutoSkipCutscenes.Text = "AutoSkipCutscenes";
			this.chkAutoSkipCutscenes.UseVisualStyleBackColor = true;
			// 
			// chkAutoFaceTarget
			// 
			this.chkAutoFaceTarget.AutoSize = true;
			this.chkAutoFaceTarget.Location = new System.Drawing.Point(190, 99);
			this.chkAutoFaceTarget.Name = "chkAutoFaceTarget";
			this.chkAutoFaceTarget.Size = new System.Drawing.Size(103, 17);
			this.chkAutoFaceTarget.TabIndex = 25;
			this.chkAutoFaceTarget.Text = "AutoFaceTarget";
			this.chkAutoFaceTarget.UseVisualStyleBackColor = true;
			// 
			// chkAutoSprint
			// 
			this.chkAutoSprint.AutoSize = true;
			this.chkAutoSprint.Location = new System.Drawing.Point(190, 122);
			this.chkAutoSprint.Name = "chkAutoSprint";
			this.chkAutoSprint.Size = new System.Drawing.Size(75, 17);
			this.chkAutoSprint.TabIndex = 26;
			this.chkAutoSprint.Text = "AutoSprint";
			this.chkAutoSprint.UseVisualStyleBackColor = true;
			// 
			// chkAutoSyncFate
			// 
			this.chkAutoSyncFate.AutoSize = true;
			this.chkAutoSyncFate.Location = new System.Drawing.Point(190, 145);
			this.chkAutoSyncFate.Name = "chkAutoSyncFate";
			this.chkAutoSyncFate.Size = new System.Drawing.Size(93, 17);
			this.chkAutoSyncFate.TabIndex = 27;
			this.chkAutoSyncFate.Text = "AutoSyncFate";
			this.chkAutoSyncFate.UseVisualStyleBackColor = true;
			// 
			// chkAutoAcceptDutyFinder
			// 
			this.chkAutoAcceptDutyFinder.AutoSize = true;
			this.chkAutoAcceptDutyFinder.Location = new System.Drawing.Point(6, 192);
			this.chkAutoAcceptDutyFinder.Name = "chkAutoAcceptDutyFinder";
			this.chkAutoAcceptDutyFinder.Size = new System.Drawing.Size(133, 17);
			this.chkAutoAcceptDutyFinder.TabIndex = 28;
			this.chkAutoAcceptDutyFinder.Text = "AutoAcceptDutyFinder";
			this.chkAutoAcceptDutyFinder.UseVisualStyleBackColor = true;
			// 
			// chkDutyFinderNotify
			// 
			this.chkDutyFinderNotify.AutoSize = true;
			this.chkDutyFinderNotify.Location = new System.Drawing.Point(6, 215);
			this.chkDutyFinderNotify.Name = "chkDutyFinderNotify";
			this.chkDutyFinderNotify.Size = new System.Drawing.Size(104, 17);
			this.chkDutyFinderNotify.TabIndex = 29;
			this.chkDutyFinderNotify.Text = "DutyFinderNotify";
			this.chkDutyFinderNotify.UseVisualStyleBackColor = true;
			// 
			// tbpTargeting
			// 
			this.tbpTargeting.Location = new System.Drawing.Point(4, 22);
			this.tbpTargeting.Name = "tbpTargeting";
			this.tbpTargeting.Padding = new System.Windows.Forms.Padding(3);
			this.tbpTargeting.Size = new System.Drawing.Size(373, 358);
			this.tbpTargeting.TabIndex = 1;
			this.tbpTargeting.Text = "Targeting";
			this.tbpTargeting.UseVisualStyleBackColor = true;
			// 
			// tbpMovement
			// 
			this.tbpMovement.Location = new System.Drawing.Point(4, 22);
			this.tbpMovement.Name = "tbpMovement";
			this.tbpMovement.Size = new System.Drawing.Size(373, 358);
			this.tbpMovement.TabIndex = 2;
			this.tbpMovement.Text = "Movement";
			this.tbpMovement.UseVisualStyleBackColor = true;
			// 
			// tbpCrBehavior
			// 
			this.tbpCrBehavior.Controls.Add(this.chkEnableCombat);
			this.tbpCrBehavior.Controls.Add(this.chkEnableCombatBuff);
			this.tbpCrBehavior.Controls.Add(this.chkEnablePull);
			this.tbpCrBehavior.Controls.Add(this.chkEnablePullBuff);
			this.tbpCrBehavior.Controls.Add(this.chkEnablePreCombatBuff);
			this.tbpCrBehavior.Controls.Add(this.chkEnableHeal);
			this.tbpCrBehavior.Controls.Add(this.chkEnableRest);
			this.tbpCrBehavior.Controls.Add(this.chkEnableSmartPull);
			this.tbpCrBehavior.Location = new System.Drawing.Point(4, 22);
			this.tbpCrBehavior.Name = "tbpCrBehavior";
			this.tbpCrBehavior.Size = new System.Drawing.Size(373, 358);
			this.tbpCrBehavior.TabIndex = 3;
			this.tbpCrBehavior.Text = "CR Behavior";
			this.tbpCrBehavior.UseVisualStyleBackColor = true;
			// 
			// chkEnableSmartPull
			// 
			this.chkEnableSmartPull.AutoSize = true;
			this.chkEnableSmartPull.Location = new System.Drawing.Point(6, 6);
			this.chkEnableSmartPull.Name = "chkEnableSmartPull";
			this.chkEnableSmartPull.Size = new System.Drawing.Size(103, 17);
			this.chkEnableSmartPull.TabIndex = 39;
			this.chkEnableSmartPull.Text = "EnableSmartPull";
			this.chkEnableSmartPull.UseVisualStyleBackColor = true;
			// 
			// tbpHotkeys
			// 
			this.tbpHotkeys.Location = new System.Drawing.Point(4, 22);
			this.tbpHotkeys.Name = "tbpHotkeys";
			this.tbpHotkeys.Size = new System.Drawing.Size(373, 358);
			this.tbpHotkeys.TabIndex = 4;
			this.tbpHotkeys.Text = "Hotkeys";
			this.tbpHotkeys.UseVisualStyleBackColor = true;
			// 
			// chkEnableRest
			// 
			this.chkEnableRest.AutoSize = true;
			this.chkEnableRest.Location = new System.Drawing.Point(6, 29);
			this.chkEnableRest.Name = "chkEnableRest";
			this.chkEnableRest.Size = new System.Drawing.Size(81, 17);
			this.chkEnableRest.TabIndex = 40;
			this.chkEnableRest.Text = "EnableRest";
			this.chkEnableRest.UseVisualStyleBackColor = true;
			// 
			// chkEnableHeal
			// 
			this.chkEnableHeal.AutoSize = true;
			this.chkEnableHeal.Location = new System.Drawing.Point(6, 52);
			this.chkEnableHeal.Name = "chkEnableHeal";
			this.chkEnableHeal.Size = new System.Drawing.Size(81, 17);
			this.chkEnableHeal.TabIndex = 41;
			this.chkEnableHeal.Text = "EnableHeal";
			this.chkEnableHeal.UseVisualStyleBackColor = true;
			// 
			// chkEnablePreCombatBuff
			// 
			this.chkEnablePreCombatBuff.AutoSize = true;
			this.chkEnablePreCombatBuff.Location = new System.Drawing.Point(6, 75);
			this.chkEnablePreCombatBuff.Name = "chkEnablePreCombatBuff";
			this.chkEnablePreCombatBuff.Size = new System.Drawing.Size(130, 17);
			this.chkEnablePreCombatBuff.TabIndex = 42;
			this.chkEnablePreCombatBuff.Text = "EnablePreCombatBuff";
			this.chkEnablePreCombatBuff.UseVisualStyleBackColor = true;
			// 
			// chkEnablePullBuff
			// 
			this.chkEnablePullBuff.AutoSize = true;
			this.chkEnablePullBuff.Location = new System.Drawing.Point(6, 98);
			this.chkEnablePullBuff.Name = "chkEnablePullBuff";
			this.chkEnablePullBuff.Size = new System.Drawing.Size(95, 17);
			this.chkEnablePullBuff.TabIndex = 43;
			this.chkEnablePullBuff.Text = "EnablePullBuff";
			this.chkEnablePullBuff.UseVisualStyleBackColor = true;
			// 
			// chkEnablePull
			// 
			this.chkEnablePull.AutoSize = true;
			this.chkEnablePull.Location = new System.Drawing.Point(6, 121);
			this.chkEnablePull.Name = "chkEnablePull";
			this.chkEnablePull.Size = new System.Drawing.Size(76, 17);
			this.chkEnablePull.TabIndex = 44;
			this.chkEnablePull.Text = "EnablePull";
			this.chkEnablePull.UseVisualStyleBackColor = true;
			// 
			// chkEnableCombatBuff
			// 
			this.chkEnableCombatBuff.AutoSize = true;
			this.chkEnableCombatBuff.Location = new System.Drawing.Point(6, 144);
			this.chkEnableCombatBuff.Name = "chkEnableCombatBuff";
			this.chkEnableCombatBuff.Size = new System.Drawing.Size(114, 17);
			this.chkEnableCombatBuff.TabIndex = 45;
			this.chkEnableCombatBuff.Text = "EnableCombatBuff";
			this.chkEnableCombatBuff.UseVisualStyleBackColor = true;
			// 
			// chkEnableCombat
			// 
			this.chkEnableCombat.AutoSize = true;
			this.chkEnableCombat.Location = new System.Drawing.Point(6, 167);
			this.chkEnableCombat.Name = "chkEnableCombat";
			this.chkEnableCombat.Size = new System.Drawing.Size(95, 17);
			this.chkEnableCombat.TabIndex = 46;
			this.chkEnableCombat.Text = "EnableCombat";
			this.chkEnableCombat.UseVisualStyleBackColor = true;
			// 
			// ClassicSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 390);
			this.Controls.Add(this.tbcSettings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ClassicSettingsForm";
			this.Text = "Kombatant";
			this.tbcSettings.ResumeLayout(false);
			this.tbpConvenience.ResumeLayout(false);
			this.tbpConvenience.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDutyFinderWaitTime)).EndInit();
			this.tbpCrBehavior.ResumeLayout(false);
			this.tbpCrBehavior.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
