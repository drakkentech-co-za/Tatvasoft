using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

/// <summary>
/// PagerCommandEventArgs
/// </summary>
/// <remarks></remarks>
public class PagerCommandEventArgs : EventArgs
{
    /// <summary>
    /// PageSelectorCommandEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="MyycoHS.PagerCommandEventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    public delegate void PageSelectorCommandEventHandler(object sender, PagerCommandEventArgs e);
}
