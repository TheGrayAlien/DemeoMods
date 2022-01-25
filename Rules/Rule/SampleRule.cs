﻿namespace Rules.Rule
{
    using HarmonyLib;

    /// <summary>
    /// Represents a modular gameplay modification.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///     It is recommended to use passive voice for naming rule classes, and to use the suffix <c>Rule</c>.
    ///     </para>
    ///     <para>
    ///     For example: Use <c>BallistaActionPointsAdjustedRule</c> instead of <c>AdjustBallistaActionPoints</c>.
    ///     </para>
    /// </remarks>
    public sealed class SampleRule : RulesAPI.Rule
    {
        /// <summary>
        /// Gets the description of the rule.
        /// </summary>
        public override string Description => "Sample rule.";

        /// <summary>
        /// Called when the rule is activated.
        /// </summary>
        protected override void OnActivate()
        {
        }

        /// <summary>
        /// Called when the rule is deactivated.
        /// </summary>
        /// <remarks>
        /// This method should undo any persistant changes made during <see cref="OnActivate"/>.
        /// </remarks>
        protected override void OnDeactivate()
        {
        }

        /// <summary>
        /// Called when the game is patched with this rule.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///     This method should perform any required patching using the given patcher.
        ///     </para>
        ///     <para>
        ///     Rules should use a static flag (e.g., <c>_isActivated</c>) in the patched method to ensure they make
        ///     modifications only while the rule is activated.
        ///     </para>
        /// </remarks>
        private static void OnPatch(Harmony harmony)
        {
        }
    }
}