using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK_TextAdventure
{
    class Dialog
    {
        private Dialog parent;
        private string content;

        private Dialog aOption;
        private Dialog bOption;


        public Dialog(string c)
        {
            content = c;
        }

        public Dialog AddAOption(string cnt)
        {
            aOption = new Dialog(cnt);
            return aOption;
        }

        public Dialog AddBOption(string cnt)
        {
            bOption = new Dialog(cnt);
            return bOption;
        }

    }
}
