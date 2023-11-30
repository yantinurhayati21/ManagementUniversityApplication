using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementUniversityApplication.Controller
{
    internal class ValidationController
    {
        public bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name field is empty", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            char[] invalidChars = { '!', '@', '#', '$', '%', '^', '&', '*', ';', '_', ':', ',', '-', '/', '\\', '?', '+', '=' };

            if (invalidChars.Contains(name[0]) || invalidChars.Contains(name[name.Length - 1]))
            {
                MessageBox.Show("Invalid starting or ending character in Name field", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            foreach (char c in invalidChars)
            {
                if (name.Contains(c))
                {
                    MessageBox.Show("Invalid character in Name field", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        public bool ValidateAddress(string loc)
        {
            if (string.IsNullOrEmpty(loc))
            {
                MessageBox.Show("field is empty", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            char[] invalidChars = { ':', '?', '!', '#', '$', '%', '^', '&', '*', '(', ')' };

            if (loc[0] >= '0' && loc[0] <= '9')
            {
                MessageBox.Show("input should not start with a number", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (loc[0] == '-' || loc[0] == '/')
            {
                MessageBox.Show("Invalid starting character field", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (loc[loc.Length - 1] == ' ' || loc[loc.Length - 1] == '-' || loc[loc.Length - 1] == '/')
            {
                MessageBox.Show("Invalid ending character field", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            foreach (char c in invalidChars)
            {
                if (loc.Contains(c))
                {
                    MessageBox.Show("Invalid character field", "Add Training", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
