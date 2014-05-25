using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Krisa
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Sets the error description string for the specified control from resource
        /// </summary>
        /// <param name="control">The control to set the error description string for</param>
        /// <param name="resource">The error description resource</param>
        public static void SetErrorResource(this ErrorProvider errorProvider, Control control, string resource)
        {
            errorProvider.SetError(control, Recursos.ResourceManager.GetString(resource));
        }
    }
}
